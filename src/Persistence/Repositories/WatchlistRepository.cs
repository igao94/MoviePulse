﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class WatchlistRepository(AppDbContext context) : IWatchlistRepository
{
    public async Task<Watchlist?> GetWatchlistEntryAsync(string userId, string movieId)
    {
        return await context.Watchlist.FindAsync(userId, movieId);
    }

    public void AddToWatchlist(Watchlist watchlist) => context.Watchlist.Add(watchlist);

    public void RemoveFromWatchlist(Watchlist watchlist) => context.Watchlist.Remove(watchlist);

    public async Task<IEnumerable<Watchlist>> GetUserWatchlistAsync(string userId, string? sort)
    {
        var query = context.Watchlist
            .Include(wl => wl.Movie)
                .ThenInclude(m => m.Celebrities)
                    .ThenInclude(c => c.Celebrity)
            .Where(wl => wl.UserId == userId)
            .AsQueryable();

        query = sort?.ToLower() switch
        {
            "dateAsc" => query.OrderBy(wl => wl.Movie.ReleaseDate),
            "dateDesc" => query.OrderByDescending(wl => wl.Movie.ReleaseDate),
            "titleAsc" => query.OrderBy(wl => wl.Movie.Title),
            "titleDesc" => query.OrderByDescending(wl => wl.Movie.Title),
            "ratingAsc" => query.OrderBy(wl => wl.Movie.Rating),
            "ratingDesc" => query.OrderByDescending(wl => wl.Movie.Rating),
            "durationAsc" => query.OrderBy(wl => wl.Movie.DurationInMinutes),
            "durationDesc" => query.OrderByDescending(wl => wl.Movie.DurationInMinutes),
            _ => query.OrderByDescending(wl => wl.CreatedAt)
        };

        return await query.ToListAsync();
    }
}
