using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class UseAbilityCommandHandler : IRequestHandler<UseAbilityCommand>
    {
        
        private readonly IReadRepository<Pokemon> pokemonsReadRepository;
        private readonly IReadRepository<Battle> battlesReadRepository;
        private readonly IWriteRepository<Battle> battlesWriteRepository;
        private readonly IMediator mediator;

        public UseAbilityCommandHandler(
            IReadRepository<Pokemon> pokemonsReadRepository, 
            IReadRepository<Battle> battlesReadRepository, 
            IWriteRepository<Battle> battlesWriteRepository, 
            IMediator mediator)
        {
            this.pokemonsReadRepository = pokemonsReadRepository;
            this.battlesReadRepository = battlesReadRepository;
            this.battlesWriteRepository = battlesWriteRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            var battle = (await this.battlesReadRepository.GetById(request.BattleId)).Value ;
            if (battle == null)
            {
                throw new InvalidOperationException("Battle not found!");
            }

            if (battle.Winner != null)
            {
                throw new InvalidOperationException("Battle has already ended!");
            }

            if (battle.ActivePlayer != request.ParticipantId)
            {
                throw new InvalidOperationException("You are not the active player, wait for your turn!");
            }

            var pokemonDealingDamage = (await this.pokemonsReadRepository.GetById(request.ParticipantId)).Value;
            var ability = pokemonDealingDamage.Abilities.FirstOrDefault(a => a.Id == request.AbilityId);
            if (ability == null)
            {
                throw new InvalidOperationException("Unknown ability!");
            }

            if (ability.RequiredLevel > pokemonDealingDamage.CurrentLevel)
            {
                throw new InvalidOperationException("You cannot use this ability yet!");
            }

            var pokemonTakingDamage = battle.Attacker;
            if (request.ParticipantId == battle.AttackerId)
            {
                pokemonTakingDamage = battle.Defender;
            }

            //pokemonTakingDamage.Health -= ability.Damage;
            //battle.ActivePlayer = pokemonTakingDamage.Pokemon.Id;
            //if (pokemonTakingDamage.Health <= 0)
            //{
            //    battle.Winner = pokemonDealingDamage;
            //    battle.Loser = pokemonTakingDamage.Pokemon;
            //    battle.FinishedAt = DateTime.Now;

            //    await mediator.Publish(new BattleEndedEvent(battle.Id), cancellationToken);
            //}

            await battlesWriteRepository.Save();

            return Unit.Value;
        }
    }
}