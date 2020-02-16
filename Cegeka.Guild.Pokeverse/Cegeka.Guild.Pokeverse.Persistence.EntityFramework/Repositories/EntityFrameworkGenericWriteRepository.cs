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

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}