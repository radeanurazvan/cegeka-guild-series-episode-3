using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using Cegeka.Guild.Pokeverse.Persistence.InMemory.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
        {
            return services
                .AddSingleton(typeof(GenericReadRepository<>))
                .AddSingleton(typeof(IReadRepository<>), provider => provider.GetService(typeof(GenericReadRepository<>)))
                .AddSingleton(typeof(IWriteRepository<>), provider => provider.GetService(typeof(GenericReadRepository<>)))
                .AddSingleton<PokemonDefinitionsRepository>()
                .AddSingleton<IReadRepository<PokemonDefinition>>(p => p.GetService<PokemonDefinitionsRepository>())
                .AddSingleton<IWriteRepository<PokemonDefinition>>(p => p.GetService<PokemonDefinitionsRepository>());
        }
    }
}