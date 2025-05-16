using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
