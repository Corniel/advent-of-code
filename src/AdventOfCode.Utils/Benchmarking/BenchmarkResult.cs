namespace Advent_of_Code.Benchmarking;

public sealed record BenchmarkResult(AdventPuzzle Puzzle, string Log)
{
    public TimeSpan? Time => Duration.TryParse(Log);
    public O Order => Time is { } time ? time.O() : default;

    public override string ToString()
        => $"{Puzzle.Date}: {Log,9}, {Order}" + Order.ComparesTo(Puzzle.Order) switch
        {
            > 0 => $" > {Puzzle.Order}",
            < 0 => $" < {Puzzle.Order}",
            _ =>  string.Empty,
        };
}
