using Cegeka.Guild.Pokeverse.Domain;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class EntityFrameworkGenericReadRepository<T> : BaseEntityFrameworkGenericReadRepository<T> where T : Entity
    {
        public EntityFrameworkGenericReadRepository(PokemonsContext context) : base(context)
        {
        }
    }
}