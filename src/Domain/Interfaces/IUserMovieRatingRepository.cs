using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserMovieRatingRepository
{
    Task<UserMovieRating?> GetUserMovieRatingAsync(string userId, string movieId);
    void AddUserMovieRating(UserMovieRating userMovieRating);
}
