namespace Advent_of_Code.Benchmarking;

public sealed record BenchmarkResult(AdventPuzzle Puzzle, string Log)
{
    public TimeSpan? Time => Duration.TryParse(Log);
    public O Order => Time is { } time ? time.O() : default;

    public override string ToString() 
        => $"{Puzzle.Date}: {Log,9}, {Order}"
        + $"{(Puzzle.Order != Order ? $" != {Puzzle.Order}": "")}";
}
