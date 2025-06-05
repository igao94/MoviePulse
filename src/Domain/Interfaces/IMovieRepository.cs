using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovieRepository
{
    Task<(IEnumerable<Movie>, DateTime?)> GetAllMoviesAsync(string? search, int pageSize, DateTime? cursor);
    Task<Movie?> GetMovieByIdAsync(string id);
    Task<Movie?> GetMovieWithCelebritiesAndGenresByIdAsync(string id);
    void RemoveMovie(Movie movie);
    void AddMovie(Movie movie);
    Task<bool> MovieExistAsync(string title);
    Task RemoveMovieRolesFromMovieAsync(string id);
}
