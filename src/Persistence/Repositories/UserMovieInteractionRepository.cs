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

    public async Task<IEnumerable<UserMovieInteraction>> GetWatchlistAsync(string userId, string? sort)
    {
        var query = context.UserMovieInteractions
            .Where(um => um.UserId == userId)
            .Include(um => um.Movie)
                .ThenInclude(m => m.Celebrities)
                    .ThenInclude(mr => mr.Celebrity)
            .AsQueryable();

        query = sort?.ToLower() switch
        {
            "releasedateasc" => query.OrderBy(um => um.Movie.ReleaseDate),
            "releasedatedesc" => query.OrderByDescending(um => um.Movie.ReleaseDate),
            "titleasc" => query.OrderBy(um => um.Movie.Title),
            "titledesc" => query.OrderByDescending(um => um.Movie.Title),
            "ratingasc" => query.OrderBy(um => um.Movie.Rating),
            "ratingdesc" => query.OrderByDescending(um => um.Movie.Rating),
            "durationasc" => query.OrderBy(um => um.Movie.DurationInMinutes),
            "durationdesc" => query.OrderByDescending(um => um.Movie.DurationInMinutes),
            _ => query.OrderByDescending(um => um.AddedToWatchlistAt)
        };

        return await query.ToListAsync();
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
