using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.DAL.Abstracts;
using Cegeka.Guild.Pokeverse.DAL.Entities;

namespace Cegeka.Guild.Pokeverse.DAL.Implementations
{
    internal class GenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly ICollection<T> entities = new List<T>();

        public IEnumerable<T> GetAll() => entities;

        public T GetById(Guid id) => entities.FirstOrDefault(e => e.Id == id);

        public void Add(T entity) => entities.Add(entity);
    }
}