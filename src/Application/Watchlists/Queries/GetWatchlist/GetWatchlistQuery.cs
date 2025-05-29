using Application.Core;
using Application.Watchlists.DTOs;
using MediatR;

namespace Application.Watchlists.Queries.GetWatchlist;

public class GetWatchlistQuery(WatchlistParams watchlistParams) : IRequest<Result<IEnumerable<WatchlistDto>>>
{
    public WatchlistParams WatchlistParams { get; set; } = watchlistParams;
}
