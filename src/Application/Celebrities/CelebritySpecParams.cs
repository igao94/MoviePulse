namespace Application.Celebrities;

public class CelebritySpecParams
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value?.ToLower();
    }

    public string? Sort { get; set; }
}
