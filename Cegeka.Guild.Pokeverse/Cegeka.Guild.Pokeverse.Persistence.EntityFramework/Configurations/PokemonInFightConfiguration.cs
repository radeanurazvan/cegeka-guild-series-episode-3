using Cegeka.Guild.Pokeverse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Configurations
{
    public class PokemonInFightConfiguration : IEntityTypeConfiguration<PokemonInFight>
    {
        public void Configure(EntityTypeBuilder<PokemonInFight> builder)
        {
            builder.HasKey(p => p.PokemonId);
            builder.HasOne(x => x.Pokemon)
                .WithOne()
                .HasForeignKey<PokemonInFight>(x => x.PokemonId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}