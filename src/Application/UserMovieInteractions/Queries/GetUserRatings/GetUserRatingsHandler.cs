using Application.Core;
using Application.Interfaces;
using Application.UserMovieInteractions.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetUserRatings;

public class GetUserRatingsHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetUserRatingsQuery, Result<IEnumerable<UserMovieIntercationDto>>>
{
    public async Task<Result<IEnumerable<UserMovieIntercationDto>>> Handle(GetUserRatingsQuery request,
        CancellationToken cancellationToken)
    {
        var userRatings = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionsAsync(userContext.GetUserId(),
                request.UserMovieIntercationParams.Sort,
                UserMovieInteractionFilter.Rated);

        return Result<IEnumerable<UserMovieIntercationDto>>
            .Success(mapper.Map<IEnumerable<UserMovieIntercationDto>>(userRatings));
    }
}
