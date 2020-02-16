using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class EntityFrameworkGenericWriteRepository<T> : IWriteRepository<T>
        where T : Entity
    {
        private readonly PokemonsContext context;

        public EntityFrameworkGenericWriteRepository(PokemonsContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }
    }
}