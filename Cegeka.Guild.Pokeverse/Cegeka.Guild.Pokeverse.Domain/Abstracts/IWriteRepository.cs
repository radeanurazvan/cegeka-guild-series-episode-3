namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IWriteRepository<in T> 
        where T : Entity
    {
        void Add(T entity);

        void Save();
    }
}