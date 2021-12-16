namespace Advent_of_Code.Rankings;

public sealed record DefaultRanking(Participant Participant, int Year) : IComparable<DefaultRanking>
{
    public int Position { get; set; }
    public int Score { get; set; }

    public Dictionary<AdventDate, DateTime> Solutions => Participant.Solutions;

    public int CompareTo(DefaultRanking other) => other.Score.CompareTo(Score);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{Position,3}) ");
        sb.Append($"{Score,5} ");

        for (var day = 1; day <= 25; day++)
        {
            if (Participant.Solutions.ContainsKey(new AdventDate(Year, day, 2)))
            {
                sb.Append('*');
            }
            else if (Participant.Solutions.ContainsKey(new AdventDate(Year, day, 1)))
            {
                sb.Append('+');
            }
            else { sb.Append('.'); }
        }
        sb.Append($"  {Participant}");
        return sb.ToString();
    }
}
