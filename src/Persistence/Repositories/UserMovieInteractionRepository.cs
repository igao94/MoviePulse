using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserMovieInteractionRepository(AppDbContext context) : IUserMovieInteractionRepository
{
    public void AddMovieToWatchlist(UserMovieInteraction userMovieInteraction)
    {
        context.UserMovieInteractions.Add(userMovieInteraction);
    }

    public async Task<UserMovieInteraction?> GetWatchlistMovieAsync(string userId, string movieId)
    {
        return await context.UserMovieInteractions
            .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId && um.IsInWatchlist);
    }

    public void RemoveMovieFromWatchlist(UserMovieInteraction userMovieInteraction)
    {
        context.UserMovieInteractions.Remove(userMovieInteraction);
    }
}
