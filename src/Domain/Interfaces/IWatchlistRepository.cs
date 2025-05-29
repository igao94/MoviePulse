using Domain.Entities;

namespace Domain.Interfaces;

public interface IWatchlistRepository
{
    Task<Watchlist?> GetWatchlistEntryAsync(string userId, string movieId);
    void AddToWatchlist(Watchlist watchlist);
    void RemoveFromWatchlist(Watchlist watchlist);
    Task<IEnumerable<Watchlist>> GetUserWatchlistAsync(string userId);
}
