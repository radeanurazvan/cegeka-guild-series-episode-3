using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Repositories
{
    internal sealed class TrainerReadRepository : BaseEntityFrameworkGenericReadRepository<Trainer>
    {
        public TrainerReadRepository(PokemonsContext context) : base(context)
        {
        }

        protected override IQueryable<Trainer> DecorateEntities(IQueryable<Trainer> entities)
        {
            return entities
                .Include(x => x.Pokemons)
                    .ThenInclude(x => x.Definition)
                        .ThenInclude(x => x.Abilities);
        }
    }
}