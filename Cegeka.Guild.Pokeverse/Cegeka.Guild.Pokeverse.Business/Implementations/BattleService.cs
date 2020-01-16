using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Business.Implementations
{
    internal class BattleService : IBattleService
    {
        private readonly IReadRepository<Pokemon> pokemonsReadRepository;
        private readonly IReadRepository<Battle> battlesReadRepository;
        private readonly IWriteRepository<Battle> battlesWriteRepository;

        public BattleService(IReadRepository<Pokemon> pokemonsReadRepository, IReadRepository<Battle> battlesReadRepository, IWriteRepository<Battle> battlesWriteRepository)
        {
            this.pokemonsReadRepository = pokemonsReadRepository;
            this.battlesReadRepository = battlesReadRepository;
            this.battlesWriteRepository = battlesWriteRepository;
        }

        public void StartBattle(Guid attackerId, Guid defenderId)
        {
            if (attackerId == defenderId)
            {
                throw new InvalidOperationException("A pokemon cannot fight itself!");
            }

            var participants = new List<Guid> { attackerId, defenderId };
            var pokemonsAlreadyInBattle = this.battlesReadRepository.GetAll()
                .Any(b => participants.Contains(b.AttackerId) || participants.Contains(b.DefenderId));
            if (pokemonsAlreadyInBattle)
            {
                throw new InvalidOperationException("Pokemons already in battle!");
            }

            var attacker = this.pokemonsReadRepository.GetById(attackerId);
            var defender = this.pokemonsReadRepository.GetById(defenderId);

            if (attacker.TrainerId == defender.TrainerId)
            {
                throw new InvalidOperationException("Two pokemons of the same trainer cannot fight!");
            }

            var battle = new Battle
            {
                AttackerId = attackerId,
                Attacker = new PokemonInFight(attacker),
                DefenderId = defenderId,
                Defender = new PokemonInFight(defender),
                ActivePlayer = attackerId
            };
            this.battlesWriteRepository.Add(battle);
            this.battlesWriteRepository.Save();
        }
    }
}