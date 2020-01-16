using Cegeka.Guild.Pokeverse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Configurations
{
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.Ignore(x => x.Abilities);

            builder.HasOne<Trainer>()
                .WithMany(x => x.Pokemons)
                .HasForeignKey(x => x.TrainerId);

            builder.HasOne(x => x.Definition)
                .WithMany()
                .HasForeignKey(x => x.DefinitionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}