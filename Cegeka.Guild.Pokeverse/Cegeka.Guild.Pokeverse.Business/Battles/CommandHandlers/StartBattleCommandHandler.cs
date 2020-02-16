using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class StartBattleCommandHandler : IRequestHandler<StartBattleCommand>
    {
        private readonly IReadRepository<Battle> battlesReadRepository;
        private readonly IReadRepository<Pokemon> pokemonsReadRepository;
        private readonly IWriteRepository<Battle> battlesWriteRepository;

        public StartBattleCommandHandler(
            IReadRepository<Battle>  battlesReadRepository, 
            IReadRepository<Pokemon> pokemonsReadRepository,
            IWriteRepository<Battle> battlesWriteRepository)
        {
            this.battlesReadRepository = battlesReadRepository;
            this.pokemonsReadRepository = pokemonsReadRepository;
            this.battlesWriteRepository = battlesWriteRepository;
        }

        public Task<Unit> Handle(StartBattleCommand request, CancellationToken cancellationToken)
        {
            if (request.AttackerId== request.DefenderId)
            {
                throw new InvalidOperationException("A pokemon cannot fight itself!");
            }

            var participants = new List<Guid> {request.AttackerId, request.DefenderId};
            var pokemonsAlreadyInBattle = this.battlesReadRepository.GetAll()
                .Any(b => participants.Contains(b.AttackerId) || participants.Contains(b.DefenderId));
            if (pokemonsAlreadyInBattle)
            {
                throw new InvalidOperationException("Pokemons already in battle!");
            }

            var attacker = this.pokemonsReadRepository.GetById(request.AttackerId);
            var defender = this.pokemonsReadRepository.GetById(request.DefenderId);

            if (attacker.TrainerId == defender.TrainerId)
            {
                throw new InvalidOperationException("Two pokemons of the same trainer cannot fight!");
            }

            var battle = new Battle
            {
                AttackerId = request.AttackerId,
                Attacker = new PokemonInFight(attacker),
                DefenderId = request.DefenderId,
                Defender = new PokemonInFight(defender),
                ActivePlayer = request.AttackerId
            };
            this.battlesWriteRepository.Add(battle);
            this.battlesWriteRepository.Save();

            return Task.FromResult(Unit.Value);
        }
    }
}