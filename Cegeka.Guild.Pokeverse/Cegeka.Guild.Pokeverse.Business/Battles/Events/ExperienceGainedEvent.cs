using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
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