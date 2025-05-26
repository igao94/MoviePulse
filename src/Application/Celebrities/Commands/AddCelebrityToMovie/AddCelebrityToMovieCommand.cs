using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Commands.AddCelebrityToMovie;

public class AddCelebrityToMovieCommand(AddCelebrityToMovieDto addCelebrityToMovieDto) : IRequest<Result<Unit>>
{
    public AddCelebrityToMovieDto AddCelebrityToMovieDto { get; set; } = addCelebrityToMovieDto;
}
