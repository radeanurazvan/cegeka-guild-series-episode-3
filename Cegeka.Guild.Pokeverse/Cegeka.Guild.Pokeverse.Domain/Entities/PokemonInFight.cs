using System;

namespace Cegeka.Guild.Pokeverse.Domain.Entities
{
    public class PokemonInFight : Entity
    {
        private PokemonInFight() {}

        public PokemonInFight(Pokemon pokemon)
        {
            Pokemon = pokemon;
            Health = pokemon.HealthPoints * 15;
        }

        public Pokemon Pokemon { get; set; }

        public Guid PokemonId { get; set; }

        public Guid? AttackBattleId { get; set; }

        public Guid? DefendBattleId { get; set; }

        public int Health { get; set; }
    }
}