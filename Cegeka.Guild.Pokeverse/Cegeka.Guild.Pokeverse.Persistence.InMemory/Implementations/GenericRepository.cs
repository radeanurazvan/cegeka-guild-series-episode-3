using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory.Implementations
{
    internal class GenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly ICollection<T> entities = new List<T>();

        public IEnumerable<T> GetAll() => entities;

        public T GetById(Guid id) => entities.FirstOrDefault(e => e.Id == id);

        public void Add(T entity) => entities.Add(entity);
        public void Save()
        {
        }
    }
}