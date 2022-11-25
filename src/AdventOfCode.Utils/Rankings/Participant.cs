namespace Advent_of_Code.Rankings;

public sealed record Participant(
    long Id,
    string Name,
    string Alias = "")
{
    public Dictionary<AdventDate, DateTime> Solutions { get; } = new();
    public HashSet<Board> Boards { get; } = new();

    public bool Matches(string name)
    {
        var trimmed = Trim(name);
        return Trim(Name) == trimmed
            || Trim(Alias) == trimmed
            || Id.ToString() == trimmed;

        static string Trim(string s) => s?.Trim().ToUpperInvariant() ?? string.Empty;
    }


    public override string ToString()
    {
        var sb = new StringBuilder(!string.IsNullOrEmpty(Alias) ? Alias : Name);
        if(Boards.Any())
        {
            sb.Append($" [{string.Join(",", Boards.Select(b => b.Name))}]");
        }
        return sb.ToString();
    }
}
