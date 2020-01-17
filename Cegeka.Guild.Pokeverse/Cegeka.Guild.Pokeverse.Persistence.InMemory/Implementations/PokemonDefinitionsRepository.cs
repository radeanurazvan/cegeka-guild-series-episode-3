using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory.Implementations
{
    internal class PokemonDefinitionsRepository : IReadRepository<PokemonDefinition>, IWriteRepository<PokemonDefinition>
    {
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

        public IEnumerable<PokemonDefinition> GetAll() => definitions;

        public PokemonDefinition GetById(Guid id) => definitions.FirstOrDefault(p => p.Id == id);

        public void Add(PokemonDefinition entity) => definitions.Add(entity);
        public void Save()
        {
        }
    }
}