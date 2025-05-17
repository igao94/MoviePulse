using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommand(CreateMovieDto createMovieDto) : IRequest<Result<MovieDto>>
{
    public CreateMovieDto CreateMovieDto { get; set; } = createMovieDto;
}
