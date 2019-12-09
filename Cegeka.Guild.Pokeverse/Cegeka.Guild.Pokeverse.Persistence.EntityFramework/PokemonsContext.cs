using Cegeka.Guild.Pokeverse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        }

        public static DbContextOptions GetOptions() => new DbContextOptionsBuilder()
            .UseSqlServer("")
            .Options;
    }
}