using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory
{
    internal class GenericReadRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : Entity
    {
        private readonly ICollection<T> entities = new List<T>();

        public Task<IEnumerable<T>> GetAll() => Task.FromResult<IEnumerable<T>>(entities);

        public Task<T> GetById(Guid id) => Task.FromResult(entities.FirstOrDefault(e => e.Id == id));

        public Task Add(T entity)
        {
            entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task Save() => Task.CompletedTask;
    }
}