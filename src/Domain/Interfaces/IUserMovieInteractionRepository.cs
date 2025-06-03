using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IUserMovieInteractionRepository
{
    void AddMovieInteraction(UserMovieInteraction userMovieInteraction);
    Task<IEnumerable<UserMovieInteraction>> GetUserMovieInteractionsAsync(string userId,
        string? sort,
        UserMovieInteractionFilter filter);
    Task<UserMovieInteraction?> GetUserMovieInteractionAsync(string userId, string movieId);
    Task<IEnumerable<(string MovieId, double? AverageRating)>> CalculateAverageMovieRatingAsync();
}
