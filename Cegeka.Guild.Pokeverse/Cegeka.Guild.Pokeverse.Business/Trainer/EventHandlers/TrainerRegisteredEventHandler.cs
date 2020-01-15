using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Trainer.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.EventHandlers
{
    internal sealed class TrainerRegisteredEventHandler : INotificationHandler<TrainerRegisteredEvent>
    {
        private const int RandomPokemonsOnRegister = 2;

        private readonly IRepository<PokemonDefinition> definitionsRepository;
        private readonly IRepository<Pokemon> pokemonsRepository;
        private readonly IRepository<Domain.Entities.Trainer> trainersRepository;

        public TrainerRegisteredEventHandler(IRepository<PokemonDefinition> definitionsRepository, IRepository<Pokemon> pokemonsRepository, IRepository<Domain.Entities.Trainer> trainersRepository)
        {
            this.definitionsRepository = definitionsRepository;
            this.pokemonsRepository = pokemonsRepository;
            this.trainersRepository = trainersRepository;
        }

        public Task Handle(TrainerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var trainer = this.trainersRepository.GetById(notification.Id);

            var random = new Random(DateTime.Now.Millisecond);
            var pokemons = this.definitionsRepository.GetAll();
            Enumerable.Range(1, RandomPokemonsOnRegister)
                .Select(_ => random.Next(0, pokemons.Count()))
                .Select(randomIndex => pokemons.ElementAt(randomIndex))
                .Select(definition => new Pokemon(trainer, definition))
                .ToList()
                .ForEach(p =>
                {
                    this.pokemonsRepository.Add(p);
                });

            pokemonsRepository.Save();
            return Task.CompletedTask;
        }
    }
}