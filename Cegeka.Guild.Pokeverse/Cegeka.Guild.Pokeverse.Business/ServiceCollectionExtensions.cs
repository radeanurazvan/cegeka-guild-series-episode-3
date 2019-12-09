using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Cegeka.Guild.Pokeverse.Business.Implementations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddScoped<IBattleService, BattleService>()
                .AddScoped<IArenaService, ArenaService>()
                .AddMediatR(BusinessAssembly.Value);
        }
    }
}