﻿using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.GetMovieById;

public class GetMovieByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetMovieByIdQuery, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.Id);

        if (movie is null)
        {
            return Result<MovieDto>.Failure("Movie not found.", 404);
        }

        return Result<MovieDto>.Success(mapper.Map<MovieDto>(movie));
    }
}
