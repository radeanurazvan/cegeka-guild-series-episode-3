using Cegeka.Guild.Pokeverse.DAL.Abstracts;
using Cegeka.Guild.Pokeverse.DAL.Entities;
using Cegeka.Guild.Pokeverse.DAL.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddSingleton<IRepository<PokemonDefinition>, PokemonDefinitionsRepository>();
        }
    }
}