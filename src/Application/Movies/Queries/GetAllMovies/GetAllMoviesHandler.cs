using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllMoviesQuery, Result<PagedList<MovieDto, DateTime?>>>
{
    public async Task<Result<PagedList<MovieDto, DateTime?>>> Handle(GetAllMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var (movies, nextCursor) = await unitOfWork.MovieRepository
            .GetAllMoviesAsync(request.MovieSpecParams.Search,
                request.MovieSpecParams.PageSize,
                request.MovieSpecParams.Cursor);

        return Result<PagedList<MovieDto, DateTime?>>.Success(new PagedList<MovieDto, DateTime?>
        {
            Items = mapper.Map<IEnumerable<MovieDto>>(movies),
            NextCursor = nextCursor
        });
    }
}
