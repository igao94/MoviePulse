using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class MovieRepository(AppDbContext context) : IMovieRepository
{
    public void AddMovie(Movie movie) => context.Movies.Add(movie);

    public async Task<(IEnumerable<Movie>, DateTime?)> GetAllMoviesAsync(string? search,
        int pageSize,
        DateTime? cursor)
    {
        var query = context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m =>
                m.Title.Contains(search) ||
                m.Description.Contains(search) ||
                m.Genres.Any(g => g.Genre.Name.Contains(search)));
        }

        query = query.OrderBy(m => m.CreatedAt);

        if (cursor.HasValue)
        {
            query = query.Where(m => m.CreatedAt >= cursor);
        }

        var movies = await query.Take(pageSize + 1).ToListAsync();

        DateTime? nextCursor = null;

        if (movies.Count > pageSize)
        {
            nextCursor = movies[pageSize].CreatedAt;

            movies.RemoveAt(pageSize);
        }

        return (movies, nextCursor);
    }

    public async Task<Movie?> GetMovieByIdAsync(string id)
    {
        return await context.Movies.FindAsync(id);
    }

    public async Task<Movie?> GetMovieWithCelebritiesAndGenresByIdAsync(string id)
    {
        return await context.Movies
            .Include(m => m.Celebrities).ThenInclude(c => c.Celebrity)
            .Include(m => m.Celebrities).ThenInclude(c => c.RoleType)
            .Include(m => m.Genres).ThenInclude(g => g.Genre)
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

    public async Task<Movie?> GetMovieWithPostersAsync(string id)
    {
        return await context.Movies
            .Include(m => m.Posters)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MoviePoster>> GetMoviePostersAsync(string id)
    {
        return await context.MoviePosters
            .Where(mp => mp.MovieId == id)
            .ToListAsync();
    }
}
