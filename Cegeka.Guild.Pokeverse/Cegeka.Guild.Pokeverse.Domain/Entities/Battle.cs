using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Battle : AggregateRoot
    {
        private Battle()
        {
            StartedAt = DateTime.Now;
        }

        internal static Battle Create(Pokemon attacker, Pokemon defender)
        {
            return new Battle
            {
                ActivePlayer = attacker.Id,
                AttackerId =  attacker.Id,
                Attacker = new PokemonInFight(attacker),
                DefenderId = defender.Id,
                Defender = new PokemonInFight(defender)
            };
        }

        public Guid AttackerId { get; private set; }

        public PokemonInFight Attacker { get; private set; }

        public Guid DefenderId { get; private set; }

        public PokemonInFight Defender { get; private set; }

        public Guid ActivePlayer { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime FinishedAt { get; private set; }

        public Pokemon Winner { get; private set; }

        public Guid? WinnerId { get; private set; }

        public Pokemon Loser { get; private set; }

        public Guid? LoserId { get; private set; }

        public bool IsOnGoing => Winner == null;
    }
}