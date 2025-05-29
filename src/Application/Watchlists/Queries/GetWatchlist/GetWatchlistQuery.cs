using Application.Core;
using Application.Watchlists.DTOs;
using MediatR;

namespace Application.Watchlists.Queries.GetWatchlist;

public class GetWatchlistQuery : IRequest<Result<IEnumerable<WatchlistDto>>>
{
}
