using Application.Core;

namespace Application.Movies;

public class MovieSpecParams : PaginationParams<DateTime?>
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value?.ToLower();
    }
}
