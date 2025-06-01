using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserMovieInteractionRepository
{
    Task<UserMovieInteraction?> GetWatchlistMovieAsync(string userId, string movieId);
    void AddMovieToWatchlist(UserMovieInteraction userMovieInteraction);
    void RemoveMovieFromWatchlist(UserMovieInteraction userMovieInteraction);
    Task<IEnumerable<UserMovieInteraction>> GetWatchlistAsync(string userId, string? sort);
}
