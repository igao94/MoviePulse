using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovieGenreRepository
{
    Task<Genre?> GetGenreByIdAsync(string genreId);
    Task<MovieGenre?> GetMovieGenreByIdAsync(string moveId, string genreId);
    void AddMovieGenre(MovieGenre movieGenre);
    Task RemoveMovieGenresAsync(string movieId);
}
