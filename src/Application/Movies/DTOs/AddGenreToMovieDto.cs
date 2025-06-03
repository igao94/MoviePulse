namespace Application.Movies.DTOs;

public class AddGenreToMovieDto
{
    public string MovieId {  get; set; } = string.Empty;
    public string GenreId {  get; set; } = string.Empty;
}
