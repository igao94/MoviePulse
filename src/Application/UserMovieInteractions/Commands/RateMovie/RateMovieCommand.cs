using Application.Core;
using Application.UserMovieInteractions.DTOs;
using MediatR;

namespace Application.UserMovieInteractions.Commands.RateMovie;

public class RateMovieCommand(RateMovieDto rateMovieDto) : IRequest<Result<Unit>>
{
    public RateMovieDto RateMovieDto { get; set; } = rateMovieDto;
}
