using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Pokemon : AggregateRoot
    {
        private Pokemon()
        {
            HealthPoints = 1;
            CriticalStrikeChancePoints = 0;
            DamagePoints = 2;
            CurrentLevel = 1;
            Experience = 0;
        }

        private Pokemon(Trainer trainer, PokemonDefinition definition)
        {
            DefinitionId = definition.Id;
            TrainerId = trainer.Id;
        }

        public static Result<Pokemon> Create(Trainer trainer, PokemonDefinition definition)
        {
            var trainerResult = Result.FailureIf(trainer == null, Messages.InvalidTrainer);
            var definitionResult = Result.FailureIf(definition == null, Messages.InvalidPokemonDefinition);

            return Result.FirstFailureOrSuccess(trainerResult, definitionResult)
                .Map(() => new Pokemon(trainer, definition));
        }

        public Guid TrainerId { get; private set; }

        public PokemonDefinition Definition { get; private set; }

        public Guid DefinitionId { get; private set; }

        public int HealthPoints { get; private set; }

        public int CriticalStrikeChancePoints { get; private set; }

        public int DamagePoints { get; private set; }

        public int CurrentLevel { get; private set; }

        public int Experience { get; private set; }

        public string Name => this.Definition.Name;

        public ICollection<Ability> Abilities => this.Definition.Abilities;
    }
}