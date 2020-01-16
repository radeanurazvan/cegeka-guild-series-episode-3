using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Repositories
{
    internal sealed class EntityFrameworkGenericReadRepository<T> : BaseEntityFrameworkGenericReadRepository<T> where T : Entity
    {
        public EntityFrameworkGenericReadRepository(PokemonsContext context) : base(context)
        {
        }
    }
}