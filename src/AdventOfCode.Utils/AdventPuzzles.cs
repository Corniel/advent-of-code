using System.Reflection;

namespace Advent_of_Code;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public partial class AdventPuzzles : IEnumerable<AdventPuzzle>
{
    private readonly List<AdventPuzzle> items = new();

    public int Count => items.Count;

    public bool Contains(AdventDate date) => items.Any(puzzle => puzzle.Matches(date));

    public IEnumerable<AdventPuzzle> Matching(AdventDate date)
        => items
        .Where(puzzle => puzzle.Matches(date))
        .OrderBy(puzzle => puzzle.Date);

    public static AdventPuzzles Load() => Load(
        typeof(AdventPuzzle).Assembly.GetExportedTypes().Concat(
        typeof(Now.Dummy).Assembly.GetExportedTypes()));

    public static AdventPuzzles Load(IEnumerable<Type> types)
    {
        var puzzles = new AdventPuzzles();

        foreach (var method in types.SelectMany(t => t.GetMethods().Where(IsPuzzle)))
        {
            var attr = method
                .GetCustomAttributes<PuzzleAttribute>()
                .Single(att => att is not ExampleAttribute);

            var puzzle = new AdventPuzzle(method, attr.Input, attr.Answer);
            puzzles.items.Add(puzzle);
        }
        return puzzles;

        static bool IsPuzzle(MethodInfo method) => method.GetCustomAttributes<PuzzleAttribute>().Any();
    }

    public IEnumerator<AdventPuzzle> GetEnumerator() => items.OrderBy(item => item.Date).GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
