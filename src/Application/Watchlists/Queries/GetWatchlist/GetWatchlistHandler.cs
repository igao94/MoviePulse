using Application.Core;
using Application.Interfaces;
using Application.Watchlists.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Watchlists.Queries.GetWatchlist;

public class GetWatchlistHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetWatchlistQuery, Result<IEnumerable<WatchlistDto>>>
{
    public async Task<Result<IEnumerable<WatchlistDto>>> Handle(GetWatchlistQuery request,
        CancellationToken cancellationToken)
    {
        var watchlist = await unitOfWork.WatchlistRepository.GetUserWatchlistAsync(userContext.GetUserId());

        return Result<IEnumerable<WatchlistDto>>.Success(mapper.Map<IEnumerable<WatchlistDto>>(watchlist));
    }
}
