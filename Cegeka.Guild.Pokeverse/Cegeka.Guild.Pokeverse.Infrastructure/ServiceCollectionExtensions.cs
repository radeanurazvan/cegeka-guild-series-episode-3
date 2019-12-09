using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
using Cegeka.Guild.Pokeverse.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddEntityFrameworkPersistence();
        }
    }
}
