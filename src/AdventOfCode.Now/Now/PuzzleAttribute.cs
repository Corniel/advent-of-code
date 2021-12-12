namespace Advent_of_Code.Now;

[AttributeUsage(AttributeTargets.Method)]
public sealed class PuzzleAttribute : Advent_of_Code.PuzzleAttribute
{
    public PuzzleAttribute(object answer, string input)
        : base(answer, input) => Do.Nothing();

    public PuzzleAttribute(object answer, int year, int day)
        : this(answer, Puzzle.Input(year, day, assembly: typeof(PuzzleAttribute).Assembly)) => Do.Nothing();
}
