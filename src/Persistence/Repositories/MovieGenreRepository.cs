using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class MovieGenreRepository(AppDbContext context) : IMovieGenreRepository
{
    public void AddMovieGenre(MovieGenre movieGenre)
    {
        context.MovieGenres.Add(movieGenre);
    }

    public async Task<Genre?> GetGenreByIdAsync(string genreId)
    {
        return await context.Genres.FindAsync(genreId);
    }

    public async Task<MovieGenre?> GetMovieGenreByIdAsync(string movieId, string genreId)
    {
        return await context.MovieGenres.FindAsync(movieId, genreId);
    }
}
