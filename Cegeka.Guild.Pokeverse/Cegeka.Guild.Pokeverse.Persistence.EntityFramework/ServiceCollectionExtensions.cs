using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<PokemonsContext>(options => options.UseSqlServer(configuration.GetConnectionString("PokeverseDatabase")))
                .AddScoped(typeof(IRepository<>), typeof(EntityFrameworkGenericRepository<>))
                .AddScoped<ISeedService, SeedService>();
        }

    }
}