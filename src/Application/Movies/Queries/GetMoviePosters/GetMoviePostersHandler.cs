using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Queries.GetMoviePosters;

public class GetMoviePostersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetMoviePostersQuery, Result<IEnumerable<PosterDto>>>
{
    public async Task<Result<IEnumerable<PosterDto>>> Handle(GetMoviePostersQuery request,
        CancellationToken cancellationToken)
    {
        var posters = await unitOfWork.MovieRepository.GetMoviePostersAsync(request.Id);

        return Result<IEnumerable<PosterDto>>.Success(mapper.Map<IEnumerable<PosterDto>>(posters));
    }
}
