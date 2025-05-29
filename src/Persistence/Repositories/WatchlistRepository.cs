using Domain.Entities;
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

    public async Task<IEnumerable<Watchlist>> GetUserWatchlistAsync(string userId)
    {
        return await context.Watchlist
            .Include(wl => wl.Movie)
                .ThenInclude(m => m.Celebrities)
                    .ThenInclude(c => c.Celebrity)
            .Where(wl => wl.UserId == userId)
            .ToListAsync();
    }
}
