using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ICelebrityRepository celebrityRepository,
    IUserMovieRatingRepository userMovieRatingRepository,
    IUserMovieInteractionRepository userMovieInteractionRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public IUserRepository UserRepository => userRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public ICelebrityRepository CelebrityRepository => celebrityRepository;

    public IUserMovieRatingRepository UserMovieRatingRepository => userMovieRatingRepository;

    public IUserMovieInteractionRepository UserMovieInteractionRepository => userMovieInteractionRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
