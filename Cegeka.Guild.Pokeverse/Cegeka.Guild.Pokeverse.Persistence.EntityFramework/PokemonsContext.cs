using Cegeka.Guild.Pokeverse.Domain.Entities;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonsContext : DbContext
    {
        public PokemonsContext()
            : base(GetOptions())
        {
            Database.Migrate();   
        }

        public PokemonsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<PokemonDefinition> PokemonDefinitions { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Battle> Battles { get; set; }

        public static DbContextOptions GetOptions() => new DbContextOptionsBuilder()
            .UseSqlServer("Data Source=.; Initial Catalog=Pokeverse;Trusted_Connection=True;")
            .Options;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PokemonInFightConfiguration().Configure(modelBuilder.Entity<PokemonInFight>());
            new BattleConfiguration().Configure(modelBuilder.Entity<Battle>());
        }
    }
}