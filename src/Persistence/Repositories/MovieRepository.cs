using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class MovieRepository(AppDbContext context) : IMovieRepository
{
    public void AddMovie(Movie movie) => context.Movies.Add(movie);

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await context.Movies.ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(string id)
    {
        return await context.Movies.FindAsync(id);
    }

    public async Task<bool> MovieExistAsync(string title) => await context.Movies.AnyAsync(u => u.Title == title);

    public void RemoveMovie(Movie movie) => context.Movies.Remove(movie);
}
