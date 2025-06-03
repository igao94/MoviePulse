using Application.Core;
using Application.Interfaces;
using Application.UserMovieInteractions.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetWatchlist;

public class GetWatchlistHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetWatchlistQuery, Result<IEnumerable<UserMovieIntercationDto>>>
{
    public async Task<Result<IEnumerable<UserMovieIntercationDto>>> Handle(GetWatchlistQuery request,
        CancellationToken cancellationToken)
    {
        var watchlist = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionsAsync(userContext.GetUserId(),
                request.UserMovieIntercationParams.Sort,
                UserMovieInteractionFilter.Watchlist);

        return Result<IEnumerable<UserMovieIntercationDto>>
            .Success(mapper.Map<IEnumerable<UserMovieIntercationDto>>(watchlist));
    }
}
