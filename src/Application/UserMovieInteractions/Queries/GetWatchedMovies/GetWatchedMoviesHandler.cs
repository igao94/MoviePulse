using Application.Core;
using Application.Interfaces;
using Application.UserMovieInteractions.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetWatchedMovies;

public class GetWatchedMoviesHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetWatchedMoviesQuery, Result<IEnumerable<UserMovieIntercationDto>>>
{
    public async Task<Result<IEnumerable<UserMovieIntercationDto>>> Handle(GetWatchedMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var watchedMovies = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionsAsync(userContext.GetUserId(),
            request.UserMovieIntercationParams.Sort,
            UserMovieInteractionFilter.Watched);

        return Result<IEnumerable<UserMovieIntercationDto>>
            .Success(mapper.Map<IEnumerable<UserMovieIntercationDto>>(watchedMovies));
    }
}
