using System;
using Cegeka.Guild.Pokeverse.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddInMemoryPersistence();
        }
    }
}
