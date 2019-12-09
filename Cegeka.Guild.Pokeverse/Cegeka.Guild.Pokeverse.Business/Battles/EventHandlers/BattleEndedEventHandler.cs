using System;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battles.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battles.EventHandlers
{
    internal sealed class BattleEndedEventHandler : INotificationHandler<BattleEndedEvent>
    {
        private static int MinExperienceGainedValue = 40;
        private static int MaxExperienceGainedValue = 50;
        private static double WinnerBonusFactor = 0.5;
        private readonly IRepository<Battle> battlesRepository;

        public BattleEndedEventHandler(IRepository<Battle> battlesRepository)
        {
            this.battlesRepository = battlesRepository;
        }

        public Task Handle(BattleEndedEvent notification, CancellationToken cancellationToken)
        {
            var battle = battlesRepository.GetById(notification.BattleId);

            var random = new Random(DateTime.Now.Millisecond);
            var experienceGained = random.Next(MinExperienceGainedValue, MaxExperienceGainedValue);
            var winner = battle.Winner;

            winner.Experience += (int)Math.Round(experienceGained * WinnerBonusFactor)+ experienceGained;

            var loser = battle.Loser;
            loser.Experience += experienceGained;

            return Task.CompletedTask;
        }
    }
}