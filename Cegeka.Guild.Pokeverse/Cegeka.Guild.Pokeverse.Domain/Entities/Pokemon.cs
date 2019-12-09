using System;
using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.Domain.Entities
{
    public class Pokemon : Entity
    {
        public Pokemon(Trainer trainer, PokemonDefinition definition)
        {
            Definition = definition;

            HealthPoints = 1;
            CriticalStrikeChancePoints = 0;
            DamagePoints = 2;
            CurrentLevel = 1;
            Experience = 0;

            TrainerId = trainer.Id;
        }

        public Guid TrainerId { get; set; }

        public string Name => this.Definition.Name;

        public PokemonDefinition Definition { get; set; }

        public int HealthPoints { get; set; }

        public int CriticalStrikeChancePoints { get; set; }

        public int DamagePoints { get; set; }

        public int CurrentLevel { get; set; }

        public int Experience { get; set; }

        public ICollection<Ability> Abilities => this.Definition.Abilities;
    }
}