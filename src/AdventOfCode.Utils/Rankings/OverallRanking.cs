namespace Advent_of_Code.Rankings;

public record OverallRanking(Participant Participant) : IComparable<OverallRanking>
{
    public int Position { get; set; }
    public int Score { get; set; }

    public Dictionary<AdventDate, DateTime> Solutions => Participant.Solutions;
    public int Silver => Participant.Solutions.Keys.Count(d => d.Part == 1);
    public int Golden => Participant.Solutions.Keys.Count(d => d.Part == 2);

    public int CompareTo(OverallRanking other) => other.Score.CompareTo(Score);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{Position,3}) ");
        sb.Append($"{Score,5} ");

        var g = (int)Math.Round(Golden / 10m, 0);
        sb.Append($"{new string('*', g)}{new string(' ', 25 - g)}");
        sb.Append($"  {Participant}");
        return sb.ToString();
    }
}
