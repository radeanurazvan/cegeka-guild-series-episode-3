using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonBattle : Entity
    {
        private PokemonBattle()
        {
        }

        public Guid BattleId { get; private set; }

        public Battle Battle { get; private set; }

        public Guid PokemonId { get; private set; }
    }
}