using System;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class BattleEndedEventHandler : INotificationHandler<BattleEndedEvent>
    {
        private static int MinExperienceGainedValue = 40;
        private static int MaxExperienceGainedValue = 50;
        private static double WinnerBonusFactor = 0.5;
        
        private readonly IReadRepository<Battle> battlesReadRepository;
        private readonly IWriteRepository<Battle> battlesWriteRepository;
        private readonly IMediator mediator;

        public BattleEndedEventHandler(IReadRepository<Battle> battlesReadRepository, IWriteRepository<Battle> battlesWriteRepository, IMediator mediator)
        {
            this.battlesReadRepository = battlesReadRepository;
            this.battlesWriteRepository = battlesWriteRepository;
            this.mediator = mediator;
        }

        public async Task Handle(BattleEndedEvent notification, CancellationToken cancellationToken)
        {
            var battle = (await battlesReadRepository.GetById(notification.BattleId)).Value;

            var random = new Random(DateTime.Now.Millisecond);
            var experienceGained = random.Next(MinExperienceGainedValue, MaxExperienceGainedValue);
            var winner = battle.Winner;


            //winner.Experience += (int)Math.Round(experienceGained * WinnerBonusFactor)+ experienceGained;

            var loser = battle.Loser;
            //loser.Experience += experienceGained;
            // TODO
            await this.battlesWriteRepository.Save();

            await this.mediator.Publish(new ExperienceGainedEvent(winner.Id), cancellationToken);
            await this.mediator.Publish(new ExperienceGainedEvent(loser.Id), cancellationToken);
        }
    }
}