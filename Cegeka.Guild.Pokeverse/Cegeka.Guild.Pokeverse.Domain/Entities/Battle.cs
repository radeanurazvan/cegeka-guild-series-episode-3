using System;

namespace Cegeka.Guild.Pokeverse.Domain.Entities
{
    public class Battle : Entity
    {
        public Battle()
        {
            StartedAt = DateTime.Now;
        }

        public Guid AttackerId { get; set; }

        public PokemonInFight Attacker { get; set; }

        public Guid DefenderId { get; set; }

        public PokemonInFight Defender { get; set; }

        public Guid ActivePlayer { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public Pokemon Winner { get; set; }

        public Pokemon Loser { get; set; }
    }
}