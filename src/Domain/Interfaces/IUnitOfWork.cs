namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ICelebrityRepository CelebrityRepository { get; }
    public IWatchlistRepository WatchlistRepository { get; }
    Task<bool> SaveChangesAsync();
}
