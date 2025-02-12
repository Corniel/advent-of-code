namespace Advent_of_Code.Rankings;

public sealed class SolvingRankingParticipant(Participant participant, IReadOnlyCollection<SolvingRankingResult> results) : IComparable<SolvingRankingParticipant>
{
    public Participant Participant { get; } = participant;

    public IReadOnlyCollection<SolvingRankingResult> Results { get; } = results;

    public SolvingRankingResult Result(AdventDate date) => Results.FirstOrDefault(r => r.Date.Matches(date));

    public string Name => Participant.Alias ?? Participant.Name;
    public decimal Rank => Results.Average(r => (decimal)r.Rank);
    public DateTime Last => Results.Select(r => r.Solved).OrderDescending().FirstOrDefault();
    public decimal Score => Results.Count + Results.Sum(r => r.Score);

    public override string ToString()
        => $"{Participant.Alias ?? Participant.Name}, Score: {Score}, Results: {Results.Count}";

    public int CompareTo(SolvingRankingParticipant other) => other.Score.CompareTo(Score);
}
