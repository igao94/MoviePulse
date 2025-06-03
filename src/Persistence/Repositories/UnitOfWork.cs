using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IMovieRepository movieRepository,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ICelebrityRepository celebrityRepository,
    IUserMovieInteractionRepository userMovieInteractionRepository,
    IMovieGenreRepository movieGenreRepository) : IUnitOfWork
{
    public IMovieRepository MovieRepository => movieRepository;

    public IUserRepository UserRepository => userRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public ICelebrityRepository CelebrityRepository => celebrityRepository;

    public IUserMovieInteractionRepository UserMovieInteractionRepository => userMovieInteractionRepository;

    public IMovieGenreRepository MovieGenreRepository => movieGenreRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
