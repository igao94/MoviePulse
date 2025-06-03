using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Commands.AddGenreToMovie;

public class AddGenreToMovieCommand(AddGenreToMovieDto addGenreToMovieDto) : IRequest<Result<Unit>>
{
    public AddGenreToMovieDto AddGenreToMovieDto { get; set; } = addGenreToMovieDto;
}
