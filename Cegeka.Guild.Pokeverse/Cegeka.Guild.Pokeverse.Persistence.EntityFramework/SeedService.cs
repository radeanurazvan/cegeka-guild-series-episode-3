using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class SeedService : ISeedService
    {
        private readonly IRepository<PokemonDefinition> definitionsRepository;

        private readonly ICollection<PokemonDefinition> definitions = new List<PokemonDefinition>
        {
            new PokemonDefinition
            {
                Name = "Pikachu",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Lightning Strike",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            },
            new PokemonDefinition
            {
                Name = "Squirtle",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Water Jet",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            },
            new PokemonDefinition
            {
                Name = "Bulbasaur",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Leaves strike",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            }
        };

        public SeedService(IRepository<PokemonDefinition> definitionsRepository)
        {
            this.definitionsRepository = definitionsRepository;
        }

        public void Seed()
        {
            var existingDefinitions = definitionsRepository.GetAll();
            if (existingDefinitions.Any())
            {
                return;
            }

            definitions.ToList().ForEach(definitionsRepository.Add);
            definitionsRepository.Save();
        }
    }
}