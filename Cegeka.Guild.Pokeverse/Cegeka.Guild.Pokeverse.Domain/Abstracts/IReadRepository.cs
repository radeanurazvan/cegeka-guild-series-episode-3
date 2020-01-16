using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Domain.Abstracts
{
    public interface IReadRepository<out T>
        where T : Entity
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);
    }
}