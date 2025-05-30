using Application.Core;
using Application.UserMovieRatings.DTOs;
using MediatR;

namespace Application.UserMovieRatings.Commands.RateMovie;

public class RateMovieCommand(RateMovieDto rateMovieDto) : IRequest<Result<Unit>>
{
    public RateMovieDto RateMovieDto { get; set; } = rateMovieDto;
}
