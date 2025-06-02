using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserMovieInteractionRepository
{
    void AddMovieInteraction(UserMovieInteraction userMovieInteraction);
    Task<IEnumerable<UserMovieInteraction>> GetWatchlistAsync(string userId, string? sort);
    Task<UserMovieInteraction?> GetUserMovieInteractionAsync(string userId, string movieId);
    Task<IEnumerable<(string MovieId, double? AverageRating)>> CalculateAverageMovieRatingAsync();
}
