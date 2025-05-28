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

    public async Task<Movie?> GetMovieWithCelebritiesByIdAsync(string id)
    {
        return await context.Movies
            .Include(m => m.Celebrities).ThenInclude(c => c.Celebrity)
            .Include(m => m.Celebrities).ThenInclude(c => c.RoleType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> MovieExistAsync(string title)
    {
        return await context.Movies.AnyAsync(u => u.Title.ToLower() == title.ToLower());
    }

    public void RemoveMovie(Movie movie) => context.Movies.Remove(movie);

    public async Task RemoveMovieRolesFromMovieAsync(string id)
    {
        var movieRoles = await context.MovieRoles
            .Where(mr => mr.MovieId == id)
            .ToListAsync();

        context.MovieRoles.RemoveRange(movieRoles);
    }

    public async Task RemoveMovieWatchlistAsync(string id)
    {
        var watchlist = await context.Watchlist
            .Where(wl => wl.MovieId == id)
            .ToListAsync();

        context.Watchlist.RemoveRange(watchlist);
    }
}
