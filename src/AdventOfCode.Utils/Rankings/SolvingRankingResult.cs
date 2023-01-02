namespace Advent_of_Code.Rankings;

public sealed record SolvingRankingResult(AdventDate Date, DateTime Solved)
{
    public int Rank { get; internal set; }
    public decimal Score { get; internal set; }
}
