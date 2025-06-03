using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserMovieInteractionRepository(AppDbContext context) : IUserMovieInteractionRepository
{
    public async Task<IEnumerable<UserMovieInteraction>> GetUserMovieInteractionsAsync(string userId,
        string? sort,
        UserMovieInteractionFilter filter)
    {
        var query = context.UserMovieInteractions
            .Where(um => um.UserId == userId)
            .Include(um => um.Movie)
                .ThenInclude(m => m.Celebrities)
                    .ThenInclude(mr => mr.Celebrity)
            .AsQueryable();

        query = filter switch
        {
            UserMovieInteractionFilter.Watchlist => query.Where(um => um.IsInWatchlist),
            UserMovieInteractionFilter.Rated => query.Where(um => um.Rating.HasValue),
            _ => query
        };

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

    public void AddMovieInteraction(UserMovieInteraction userMovieInteraction)
    {
        context.UserMovieInteractions.Add(userMovieInteraction);
    }

    public async Task<UserMovieInteraction?> GetUserMovieInteractionAsync(string userId, string movieId)
    {
        return await context.UserMovieInteractions.FindAsync(userId, movieId);
    }

    public async Task<IEnumerable<(string MovieId, double? AverageRating)>> CalculateAverageMovieRatingAsync()
    {
        var list = await context.UserMovieInteractions
            .Where(um => um.Rating.HasValue)
            .GroupBy(um => um.MovieId)
            .Select(g => new
            {
                MovieId = g.Key,
                AverageRating = g.Average(um => um.Rating)
            })
            .ToListAsync();

        return list.Select(x => (x.MovieId, x.AverageRating));
    }

    public async Task DeleteUserInteractionsAsync(string userId)
    {
        var interactions = await context.UserMovieInteractions
            .Where(um => um.UserId == userId)
            .ToListAsync();

        context.UserMovieInteractions.RemoveRange(interactions);
    }    
    
    public async Task DeleteMovieInteractionsAsync(string movieId)
    {
        var interactions = await context.UserMovieInteractions
            .Where(um => um.MovieId == movieId)
            .ToListAsync();

        context.UserMovieInteractions.RemoveRange(interactions);
    }
}
