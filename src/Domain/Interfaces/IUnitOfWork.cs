namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ICelebrityRepository CelebrityRepository { get; }
    public IUserMovieInteractionRepository UserMovieInteractionRepository { get; }
    public IMovieGenreRepository MovieGenreRepository { get; }
    Task<bool> SaveChangesAsync();
}
