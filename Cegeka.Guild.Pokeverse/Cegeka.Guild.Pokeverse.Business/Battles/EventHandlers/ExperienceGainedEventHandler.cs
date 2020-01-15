using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battles.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battles.EventHandlers
{
    internal sealed class ExperienceGainedEventHandler : INotificationHandler<ExperienceGainedEvent>
    {
        private const int ExperienceThreshold = 100;
        private readonly IRepository<Pokemon> pokemonRepository;

        public ExperienceGainedEventHandler(IRepository<Pokemon> pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        public Task Handle(ExperienceGainedEvent notification, CancellationToken cancellationToken)
        {
            var pokemon = this.pokemonRepository.GetById(notification.PokemonId);
            if(pokemon.Experience > pokemon.CurrentLevel * ExperienceThreshold)
            {
                pokemon.CurrentLevel++;
                pokemon.Experience = 0;
            }

            this.pokemonRepository.Save();
            return Task.CompletedTask;
        }
    }
}