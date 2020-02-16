using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class SeedService : ISeedService
    {
        private readonly IReadRepository<PokemonDefinition> definitionsReadRepository;
        private readonly IWriteRepository<PokemonDefinition> definitionsWriteRepository;

        private readonly ICollection<PokemonDefinition> defaultPokemonDefinitions = new List<PokemonDefinition>
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

        public SeedService(IReadRepository<PokemonDefinition> definitionsReadRepository, IWriteRepository<PokemonDefinition> definitionsWriteRepository)
        {
            this.definitionsReadRepository = definitionsReadRepository;
            this.definitionsWriteRepository = definitionsWriteRepository;
        }

        public void Seed()
        {
            var existingDefinitions = definitionsReadRepository.GetAll();
            if (existingDefinitions.Any())
            {
                return;
            }

            defaultPokemonDefinitions.ToList().ForEach(definitionsWriteRepository.Add);
            definitionsWriteRepository.Save();
        }
    }
}