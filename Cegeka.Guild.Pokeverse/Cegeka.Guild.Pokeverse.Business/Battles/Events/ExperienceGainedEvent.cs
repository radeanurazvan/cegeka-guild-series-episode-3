using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battles.Events
{
    public class ExperienceGainedEvent : INotification
    {
        public ExperienceGainedEvent(Guid pokemonId)
        {
            PokemonId = pokemonId;
        }

        public Guid PokemonId { get; private set; }
    }
}