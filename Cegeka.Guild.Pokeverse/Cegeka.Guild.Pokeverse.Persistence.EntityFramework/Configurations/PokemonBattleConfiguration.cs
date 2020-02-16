using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonBattleConfiguration : IEntityTypeConfiguration<PokemonBattle>
    {
        public void Configure(EntityTypeBuilder<PokemonBattle> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(typeof(Pokemon))
                .WithMany(Pokemon.Expressions.Battles)
                .HasPrincipalKey(nameof(Pokemon.Id))
                .HasForeignKey(nameof(PokemonBattle.BattleId));

            builder.HasOne(b => b.Battle)
                .WithOne()
                .HasPrincipalKey(nameof(Battle.Id))
                .HasForeignKey(nameof(PokemonBattle.BattleId));
        }
    }
}