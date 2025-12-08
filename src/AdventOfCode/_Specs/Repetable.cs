namespace Specs.Repeatable;

[Explicit]
public class Puzzles_are
{
    public static readonly AdventPuzzle[] Puzzles = [..AdventPuzzles.Load(typeof(Puzzles_are).Assembly)];

    [TestCaseSource(nameof(Puzzles))]
    public void Repeatable(AdventPuzzle puzzle)
    {
        var instance = Activator.CreateInstance(puzzle.Method.DeclaringType);

        var first = puzzle.Method.Invoke(instance, puzzle.Input);
        first.Should().Be(puzzle.Answer, "first");

        var second = puzzle.Method.Invoke(instance, puzzle.Input);
        second.Should().Be(puzzle.Answer, "second");
    }
}
