using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkPersistence(this IServiceCollection services)
        {
            return services.AddScoped(typeof(EntityFrameworkGenericRepository<>), typeof(IRepository<>));
        }
    }
}