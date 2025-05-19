using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository,
    IAccountRepository accountRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public IAccountRepository AccountRepository => accountRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
