using Shared.Constants;

namespace Application.Movies.DTOs;

public class MovieDto : BaseMovieDto
{
    public string Id { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public double Rating { get; set; }
    public double MaxRating { get; set; } = MovieRating.MaxRating;
    public IEnumerable<MovieRoleDto> Celebrities { get; set; } = [];
    public IEnumerable<string> Genres { get; set; } = [];
}
