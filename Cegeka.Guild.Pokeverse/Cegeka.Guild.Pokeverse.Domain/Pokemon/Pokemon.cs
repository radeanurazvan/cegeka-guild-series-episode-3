using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Pokemon : AggregateRoot
    {
        private readonly ICollection<PokemonBattle> battles = new List<PokemonBattle>();

        private Pokemon()
        {
            HealthPoints = 1;
            CriticalStrikeChancePoints = 0;
            DamagePoints = 2;
            CurrentLevel = 1;
            Experience = 0;
        }

        private Pokemon(Trainer trainer, PokemonDefinition definition) : this()
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

        public IEnumerable<Ability> Abilities => this.Definition.Abilities.Where(a => a.RequiredLevel <= CurrentLevel);

        public IEnumerable<PokemonBattle> Battles => this.battles;

        public Result<Battle> Attack(Pokemon other)
        {
            return Result.FailureIf(other == null, Messages.InvalidPokemon)
                .Ensure(() => this != other, Messages.CannotFightItself)
                .Ensure(() => this.TrainerId != other.TrainerId, Messages.SiblingsCannotFight)
                .Ensure(() => !this.IsInBattle, Messages.PokemonAlreadyInBattle)
                .Ensure(() => !other.IsInBattle, Messages.PokemonAlreadyInBattle)
                .Map(() => Battle.Create(this, other));
        }

        public Result UseAbility(Guid battleId, Guid abilityId)
        {
            var abilityResult = this.Definition.Abilities.FirstOrNothing(a => a.Id == abilityId).ToResult(Messages.UnknownAbility)
                .Ensure(a => a.RequiredLevel <= CurrentLevel, Messages.CannotUseAbility);
            var battleResult = this.battles.FirstOrNothing(b => b.BattleId == battleId).Select(b => b.Battle).ToResult(Messages.BattleDoesNotExist)
                .Ensure(b => b.IsOnGoing, Messages.BattleHasEnded)
                .Ensure(b => b.ActivePlayer == this.Id, Messages.NotYourTurn);

            return Result.FirstFailureOrSuccess(abilityResult, battleResult)
                .Bind(() => battleResult.Value.TakeTurn(this.Id, abilityResult.Value));
        }

        private bool IsInBattle => this.battles.Any(b => b.Battle.IsOnGoing);

        public static class Expressions
        {
            public static string Battles = $"{nameof(battles)}.{nameof(PokemonBattle.Battle)}";
        }
    }
}