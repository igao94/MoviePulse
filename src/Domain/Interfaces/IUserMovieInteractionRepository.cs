using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserMovieInteractionRepository
{
    Task<UserMovieInteraction?> GetWatchlistMovieAsync(string userId, string movieId);
    void AddMovieInteraction(UserMovieInteraction userMovieInteraction);
    void RemoveMovieInteraction(UserMovieInteraction userMovieInteraction);
    Task<IEnumerable<UserMovieInteraction>> GetWatchlistAsync(string userId, string? sort);
    Task<UserMovieInteraction?> GetUserMovieInteractionAsync(string userId, string movieId);
}
