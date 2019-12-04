using Cegeka.Guild.Pokeverse.BLL.Abstracts;
using Cegeka.Guild.Pokeverse.BLL.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ITrainerService, TrainerService>()
                .AddScoped<IBattleService, BattleService>()
                .AddScoped<IArenaService, ArenaService>();
        }
    }
}