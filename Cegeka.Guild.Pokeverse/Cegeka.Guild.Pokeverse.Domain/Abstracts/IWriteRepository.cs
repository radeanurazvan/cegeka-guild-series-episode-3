using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Domain.Abstracts
{
    public interface IWriteRepository<in T> 
        where T : Entity
    {
        void Add(T entity);

        void Save();
    }
}