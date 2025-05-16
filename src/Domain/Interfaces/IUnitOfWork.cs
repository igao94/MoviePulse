namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    Task<bool> SaveChangesAsync();
}
