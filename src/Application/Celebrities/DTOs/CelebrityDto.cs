namespace Application.Celebrities.DTOs;

public class CelebrityDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
}
