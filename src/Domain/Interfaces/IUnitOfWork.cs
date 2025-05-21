namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IMovieRepository MovieRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public IRoleRepository RoleRepository { get; }
    Task<bool> SaveChangesAsync();
}
