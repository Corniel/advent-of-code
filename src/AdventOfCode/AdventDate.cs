namespace Advent_of_Code;

public readonly struct AdventDate : IComparable<AdventDate>
{
    public static readonly AdventDate All;
    public AdventDate(int? year, int? day, int? part)
    {
        Year = year;
        Day = day;
        Part = part;
    }

    public int? Year { get; }
    public int? Day { get; }
    public int? Part { get; }

    public bool SpecifiesYearDay() => Year.HasValue && Day.HasValue && !Part.HasValue;

    public bool Matches(AdventDate other)
        => Matches(Year, other.Year)
        && Matches(Day, other.Day)
        && Matches(Part, other.Part);

    private static bool Matches(int? l, int? r) => !l.HasValue || !r.HasValue || l.Value == r.Value;

    public override string ToString()
    {
        var str = $"{Year}-{Day:00}-{Part}".Replace("--", "*");
        while (str[^1] == '-') { str = str[0..^1]; }
        return str;
    }

    public static bool TryParse(string str, out AdventDate adventDate)
    {
        adventDate = default;
        if (str?.ToUpperInvariant() == "ALL" || str == "*") { return true; }

        var parts = str.Separate('-');
        var length = parts.Length;

        if (length > 3) { return false; }
        else
        {
            int? year;
            int? day = default;
            int? part = default;

            if (!int.TryParse(parts[0], out int y) || y < 2015) { return false; }
            else { year = y; }
            if (length > 1)
            {
                if (!int.TryParse(parts[1], out int d) || d < 0 || d > 25) { return false; }
                else { day = d; }
            }

            if (length > 2)
            {
                switch (parts[2].ToUpperInvariant())
                {
                    case "1": case "ONE": part = 1; break;
                    case "2": case "TWO": part = 2; break;
                    default: return false;
                }
            }

            adventDate = new AdventDate(year, day, part);
            return true;
        }
    }

    public int CompareTo(AdventDate other)
        => Compares(Year, other.Year)
        ?? Compares(Day, other.Day)
        ?? Compares(Part, other.Part)
        ?? 0;

    private static int? Compares(int? l, int? r)
    {
        if (l.HasValue && r.HasValue)
        {
            var compare = l.Value.CompareTo(r.Value);
            return compare == 0 ? default : (int?)compare;
        }
        else if (l.HasValue) { return -1; }
        else if (r.HasValue) { return +1; }
        else { return default; }
    }

    public bool IsAvailable(DateTime? now = default)
        => new DateTime(Year ?? 1, month: 12, Day ?? 1, hour: 05, minute: 00, second: 00, DateTimeKind.Utc)
        <= (now ?? Clock.UtcNow());

    public static IEnumerable<AdventDate> AllAvailable(DateTime? now = default)
        => Range(2015, 1 + Clock.Today().Year - 2015)
        .SelectMany(year => Range(1, 25).Select(d => new AdventDate(year, d, default)))
        .SelectMany(date => new[]
        {
            new AdventDate(date.Year, date.Day, 1),
            new AdventDate(date.Year, date.Day, 2)
        })
        .Where(date => date.IsAvailable(now));
}
