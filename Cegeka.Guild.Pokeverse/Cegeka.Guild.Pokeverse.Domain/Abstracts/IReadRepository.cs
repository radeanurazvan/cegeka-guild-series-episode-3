using System;
using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IReadRepository<out T>
        where T : Entity
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);
    }
}