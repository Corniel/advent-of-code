namespace Advent_of_Code.Rankings;

public sealed record Participant(
    long Id,
    string Name,
    string Alias = "") : IFormattable
{
    public Dictionary<AdventDate, DateTime> Solutions { get; } = [];
    public HashSet<Board> Boards { get; } = [];

    public bool Matches(string name)
    {
        var trimmed = Trim(name);
        return Trim(Name) == trimmed
            || Trim(Alias) == trimmed
            || Id.ToString() == trimmed;

        static string Trim(string s) => s?.Trim().ToUpperInvariant() ?? string.Empty;
    }


    public override string ToString() => ToString(null, null);

    public string ToString(string format, IFormatProvider formatProvider)
    {
        var sb = new StringBuilder(!string.IsNullOrEmpty(Alias) ? Alias : Name);
        if (format != "name-only" && Boards.NotEmpty())
        {
            sb.Append($" [{string.Join(",", Boards.Select(b => b.Name))}]");
        }
        return sb.ToString();
    }
}
