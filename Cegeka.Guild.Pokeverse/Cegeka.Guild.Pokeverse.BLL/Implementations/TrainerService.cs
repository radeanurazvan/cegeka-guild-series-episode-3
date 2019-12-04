﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.BLL.Abstracts;
using Cegeka.Guild.Pokeverse.BLL.Models;
using Cegeka.Guild.Pokeverse.DAL.Abstracts;
using Cegeka.Guild.Pokeverse.DAL.Entities;

namespace Cegeka.Guild.Pokeverse.BLL.Implementations
{
    internal sealed class TrainerService : ITrainerService
    {
        private const int RandomPokemonsOnRegister = 2;

        private readonly IRepository<PokemonDefinition> definitionsRepository;
        private readonly IRepository<Pokemon> pokemonsRepository;
        private readonly IRepository<Trainer> trainerRepository;

        public TrainerService(IRepository<PokemonDefinition> definitionsRepository, IRepository<Pokemon> pokemonsRepository, IRepository<Trainer> trainerRepository)
        {
            this.definitionsRepository = definitionsRepository;
            this.pokemonsRepository = pokemonsRepository;
            this.trainerRepository = trainerRepository;
        }

        public IEnumerable<TrainerModel> GetAll()
        {
            return this.trainerRepository.GetAll().Select(t => new TrainerModel
            {
                Id = t.Id,
                Name = t.Name,
                Pokemons = t.Pokemons.Select(p => new PokemonModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList()
            });
        }

        public void Register(string name)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var trainer = new Trainer { Name = name };

            var pokemons = this.definitionsRepository.GetAll();
            Enumerable.Range(1, RandomPokemonsOnRegister)
                .Select(_ => random.Next(0, pokemons.Count()))
                .Select(randomIndex => pokemons.ElementAt(randomIndex))
                .Select(definition => new Pokemon(trainer, definition))
                .ToList()
                .ForEach(p =>
                {
                    this.pokemonsRepository.Add(p);
                    trainer.Pokemons.Add(p);
                });

            this.trainerRepository.Add(trainer);
        }
    }
}