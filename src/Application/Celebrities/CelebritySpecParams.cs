using Application.Core;

namespace Application.Celebrities;

public class CelebritySpecParams : PaginationParams<DateTime?>
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value?.ToLower();
    }
}
