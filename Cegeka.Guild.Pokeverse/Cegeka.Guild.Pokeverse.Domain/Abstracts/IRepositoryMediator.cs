namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IRepositoryMediator
    {
        IReadRepository<T> Read<T>() where T : Entity;

        IWriteRepository<T> Write<T>() where T: Entity;
    }
}