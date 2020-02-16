using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class ExperienceGainedEventHandler : INotificationHandler<ExperienceGainedEvent>
    {
        private const int ExperienceThreshold = 100;
        private readonly IReadRepository<Pokemon> pokemonReadRepository;
        private readonly IWriteRepository<Pokemon> pokemonWriteRepository;

        public ExperienceGainedEventHandler(IReadRepository<Pokemon> pokemonReadRepository, IWriteRepository<Pokemon> pokemonWriteRepository)
        {
            this.pokemonReadRepository = pokemonReadRepository;
            this.pokemonWriteRepository = pokemonWriteRepository;
        }

        public Task Handle(ExperienceGainedEvent notification, CancellationToken cancellationToken)
        {
            var pokemon = this.pokemonReadRepository.GetById(notification.PokemonId);
            if(pokemon.Experience > pokemon.CurrentLevel * ExperienceThreshold)
            {
                pokemon.CurrentLevel++;
                pokemon.Experience = 0;
            }

            this.pokemonWriteRepository.Save();
            return Task.CompletedTask;
        }
    }
}