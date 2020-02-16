using System.Threading.Tasks;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IWriteRepository<in T> 
        where T : Entity
    {
        Task Add(T entity);

        Task Save();
    }
}