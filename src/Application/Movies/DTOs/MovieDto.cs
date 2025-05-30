﻿using Shared.Constants;

namespace Application.Movies.DTOs;

public class MovieDto : BaseMovieDto
{
    public string Id { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public double Rating { get; set; }
    public double MaxRating { get; set; } = MovieRating.MaxRating;
    public ICollection<MovieRoleDto> Celebrities { get; set; } = [];
}
