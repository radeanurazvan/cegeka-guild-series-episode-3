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
        private readonly IRepository<Pokemon> pokemonsRepository;
        private readonly IRepository<Battle> battlesRepository;

        public BattleService(IRepository<Pokemon> pokemonsRepository, IRepository<Battle> battlesRepository)
        {
            this.pokemonsRepository = pokemonsRepository;
            this.battlesRepository = battlesRepository;
        }

        public void StartBattle(Guid attackerId, Guid defenderId)
        {
            if (attackerId == defenderId)
            {
                throw new InvalidOperationException("A pokemon cannot fight itself!");
            }

            var participants = new List<Guid> { attackerId, defenderId };
            var pokemonsAlreadyInBattle = this.battlesRepository.GetAll()
                .Any(b => participants.Contains(b.AttackerId) || participants.Contains(b.DefenderId));
            if (pokemonsAlreadyInBattle)
            {
                throw new InvalidOperationException("Pokemons already in battle!");
            }

            var attacker = this.pokemonsRepository.GetById(attackerId);
            var defender = this.pokemonsRepository.GetById(defenderId);

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
            this.battlesRepository.Add(battle);
        }
    }
}