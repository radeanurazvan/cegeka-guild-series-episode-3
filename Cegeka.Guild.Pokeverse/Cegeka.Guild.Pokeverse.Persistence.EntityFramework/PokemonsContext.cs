using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonsContext : DbContext
    {
        public PokemonsContext()
            : base(GetOptions())
        {
        }

        public PokemonsContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();   
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
            new PokemonConfiguration().Configure(modelBuilder.Entity<Pokemon>());
        }
    }
}