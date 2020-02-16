using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IReadRepository<T>
        where T : Entity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);
    }
}