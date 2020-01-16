using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Repositories
{
    internal abstract class BaseEntityFrameworkGenericReadRepository<T> : IReadRepository<T>
        where T : Entity
    {
        private readonly IQueryable<T> entities;

        protected BaseEntityFrameworkGenericReadRepository(PokemonsContext context)
        {
            this.entities = this.DecorateEntities(context.Set<T>());
        }

        protected virtual IQueryable<T> DecorateEntities(IQueryable<T> entities) => entities;

        public IEnumerable<T> GetAll()
        {
            return entities.AsNoTracking().ToList();
        }

        public T GetById(Guid id)
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }
    }
}