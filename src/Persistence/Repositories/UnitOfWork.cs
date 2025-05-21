using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository,
    IAccountRepository accountRepository,
    IRoleRepository roleRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public IAccountRepository AccountRepository => accountRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
