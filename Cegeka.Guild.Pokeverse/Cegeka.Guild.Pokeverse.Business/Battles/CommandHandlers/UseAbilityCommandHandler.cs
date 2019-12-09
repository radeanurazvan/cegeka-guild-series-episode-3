using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battles.Commands;
using Cegeka.Guild.Pokeverse.Business.Battles.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battles.CommandHandlers
{
    internal sealed class UseAbilityCommandHandler : IRequestHandler<UseAbilityCommand>
    {
        
        private readonly IRepository<Pokemon> pokemonsRepository;
        private readonly IRepository<Battle> battlesRepository;
        private readonly IMediator mediator;

        public UseAbilityCommandHandler(IRepository<Pokemon> pokemonsRepository, IRepository<Battle> battlesRepository, IMediator mediator)
        {
            this.pokemonsRepository = pokemonsRepository;
            this.battlesRepository = battlesRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            var battle = this.battlesRepository.GetById(request.BattleId);
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

            var pokemonDealingDamage = this.pokemonsRepository.GetById(request.ParticipantId);
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

            pokemonTakingDamage.Health -= ability.Damage;
            battle.ActivePlayer = pokemonTakingDamage.Pokemon.Id;
            if (pokemonTakingDamage.Health <= 0)
            {
                battle.Winner = pokemonDealingDamage;
                battle.Loser = pokemonTakingDamage.Pokemon;
                battle.FinishedAt = DateTime.Now;

                await mediator.Publish(new BattleEndedEvent(battle.Id), cancellationToken);
            }

            battlesRepository.Save();

            return Unit.Value;
        }
    }
}