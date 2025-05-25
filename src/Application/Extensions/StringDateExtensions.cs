using System.Globalization;

namespace Application.Extensions;

public static class StringDateExtensions
{
    public static bool BeValidDate(this string dateStr)
    {
        return DateOnly
            .TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public static DateOnly ParseDateOnlyFromString(this string str)
    {
        return DateOnly.ParseExact(str, "yyyy-MM-dd");
    }
}
