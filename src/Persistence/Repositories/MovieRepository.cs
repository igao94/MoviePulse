using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class MovieRepository(AppDbContext context) : IMovieRepository
{
    public void AddMovie(Movie movie) => context.Movies.Add(movie);

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(string? search, string? sort)
    {
        var query = context.Movies
            .OrderByDescending(m => m.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m =>
                m.Title.Contains(search) ||
                m.Description.Contains(search) ||
                m.Genres.Any(g => g.Genre.Name.Contains(search)));
        }

        query = sort switch
        {
            "titleAsc" => query.OrderBy(m => m.Title),
            "titleDesc" => query.OrderByDescending(m => m.Title),
            "releaseDateAsc" => query.OrderBy(m => m.ReleaseDate),
            "releaseDateDesc" => query.OrderByDescending(m => m.ReleaseDate),
            "durationAsc" => query.OrderBy(m => m.DurationInMinutes),
            "durationDesc" => query.OrderByDescending(m => m.DurationInMinutes),
            "ratingAsc" => query.OrderBy(m => m.Rating),
            "ratingDesc" => query.OrderByDescending(m => m.Rating),
            _ => query
        };

        return await query.ToListAsync();
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
}
