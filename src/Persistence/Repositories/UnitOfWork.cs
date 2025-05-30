using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ICelebrityRepository celebrityRepository,
    IWatchlistRepository watchlistRepository,
    IUserMovieRatingRepository userMovieRatingRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public IUserRepository UserRepository => userRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public ICelebrityRepository CelebrityRepository => celebrityRepository;

    public IWatchlistRepository WatchlistRepository => watchlistRepository;

    public IUserMovieRatingRepository UserMovieRatingRepository => userMovieRatingRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
