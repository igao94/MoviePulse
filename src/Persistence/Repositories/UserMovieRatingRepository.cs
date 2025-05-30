using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserMovieRatingRepository(AppDbContext context) : IUserMovieRatingRepository
{
    public async Task<UserMovieRating?> GetUserMovieRatingAsync(string userId, string movieId)
    {
        return await context.UserMovieRatings.FindAsync(userId, movieId);
    }

    public void AddUserMovieRating(UserMovieRating userMovieRating)
    {
        context.UserMovieRatings.Add(userMovieRating);
    }
}
