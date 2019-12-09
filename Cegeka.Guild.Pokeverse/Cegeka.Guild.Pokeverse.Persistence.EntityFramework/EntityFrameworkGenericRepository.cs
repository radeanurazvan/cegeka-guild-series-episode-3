using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class EntityFrameworkGenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly PokemonsContext context;

        public EntityFrameworkGenericRepository(PokemonsContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}