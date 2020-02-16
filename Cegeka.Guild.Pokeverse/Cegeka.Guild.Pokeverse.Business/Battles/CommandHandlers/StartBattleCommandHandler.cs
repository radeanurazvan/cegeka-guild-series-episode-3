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

        public async Task<Unit> Handle(StartBattleCommand request, CancellationToken cancellationToken)
        {
            if (request.AttackerId== request.DefenderId)
            {
                throw new InvalidOperationException("A pokemon cannot fight itself!");
            }

            var participants = new List<Guid> {request.AttackerId, request.DefenderId};
            var pokemonsAlreadyInBattle = (await this.battlesReadRepository.GetAll())
                .Any(b => participants.Contains(b.AttackerId) || participants.Contains(b.DefenderId));
            if (pokemonsAlreadyInBattle)
            {
                throw new InvalidOperationException("Pokemons already in battle!");
            }

            var attacker = await this.pokemonsReadRepository.GetById(request.AttackerId);
            var defender = await this.pokemonsReadRepository.GetById(request.DefenderId);

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

            await this.battlesWriteRepository.Add(battle);
            await this.battlesWriteRepository.Save();

            return Unit.Value;
        }
    }
}