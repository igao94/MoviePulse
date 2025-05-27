using System.Text.Json.Serialization;

namespace Application.Celebrities.DTOs;

public class UpdateCelebrityDto : BaseCreateCelebrityDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
}
