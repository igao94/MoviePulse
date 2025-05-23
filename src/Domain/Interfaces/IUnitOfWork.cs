namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    Task<bool> SaveChangesAsync();
}
