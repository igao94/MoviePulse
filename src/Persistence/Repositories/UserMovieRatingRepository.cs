using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<(string MovieId, double AverageRating)>> GetAverageRatingForMoviesAsync()
    {
        var list = await context.UserMovieRatings
            .GroupBy(umr => umr.MovieId)
            .Select(g => new
            {
                MovieId = g.Key,
                AverageRating = g.Average(umr => umr.Rating)
            })
            .ToListAsync();

        return list.Select(x => (x.MovieId, x.AverageRating));
    }
}
