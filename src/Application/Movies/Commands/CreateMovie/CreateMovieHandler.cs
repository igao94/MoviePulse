using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateMovieCommand, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = mapper.Map<Movie>(request.CreateMovieDto);

        unitOfWork.MovieRepository.AddMovie(movie);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<MovieDto>.Success(mapper.Map<MovieDto>(movie))
            : Result<MovieDto>.Failure("Failed to create movie.", 400);
    }
}
