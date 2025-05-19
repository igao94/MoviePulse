namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    public IAccountRepository AccountRepository { get; }
    Task<bool> SaveChangesAsync();
}
