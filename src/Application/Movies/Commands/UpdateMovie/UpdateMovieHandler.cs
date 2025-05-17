using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie;

public class UpdateMovieHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.UpdateMovieDto.Id);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        mapper.Map(request.UpdateMovieDto, movie);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update movie.", 400);
    }
}
