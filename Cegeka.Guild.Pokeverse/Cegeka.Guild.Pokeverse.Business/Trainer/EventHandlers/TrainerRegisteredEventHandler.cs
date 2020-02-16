using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class TrainerRegisteredEventHandler : INotificationHandler<TrainerRegisteredEvent>
    {
        private const int RandomPokemonsOnRegister = 2;

        private readonly IReadRepository<PokemonDefinition> definitionsReadRepository;
        private readonly IReadRepository<Trainer> trainersReadRepository;
        private readonly IWriteRepository<Pokemon> pokemonWriteRepository;

        public TrainerRegisteredEventHandler(
            IReadRepository<PokemonDefinition> definitionsReadRepository, 
            IReadRepository<Trainer> trainersReadRepository,
            IWriteRepository<Pokemon> pokemonWriteRepository)
        {
            this.definitionsReadRepository = definitionsReadRepository;
            this.trainersReadRepository = trainersReadRepository;
            this.pokemonWriteRepository = pokemonWriteRepository;
        }

        public Task Handle(TrainerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var trainer = this.trainersReadRepository.GetById(notification.Id);

            var random = new Random(DateTime.Now.Millisecond);
            var pokemons = this.definitionsReadRepository.GetAll();
            Enumerable.Range(1, RandomPokemonsOnRegister)
                .Select(_ => random.Next(0, pokemons.Count()))
                .Select(randomIndex => pokemons.ElementAt(randomIndex))
                .Select(definition => new Pokemon(trainer, definition))
                .ToList()
                .ForEach(p => pokemonWriteRepository.Add(p));

            pokemonWriteRepository.Save();
            return Task.CompletedTask;
        }
    }
}