using System.Reflection;

namespace Advent_of_Code;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public partial class AdventPuzzles : IReadOnlyCollection<AdventPuzzle>
{
    private readonly List<AdventPuzzle> items = [];

    public int Count => items.Count;

    public bool Contains(AdventDate date) => items.Exists(puzzle => puzzle.Matches(date));

    public IEnumerable<AdventPuzzle> Matching(AdventDate date)
        => items
        .Where(puzzle => puzzle.Matches(date))
        .OrderBy(puzzle => puzzle.Date);

    public static AdventPuzzles Load(params Assembly[] additional) => Load(
        typeof(AdventPuzzle).Assembly.GetExportedTypes().Concat(
        additional.SelectMany(a => a.GetExportedTypes())));

    public static AdventPuzzles Load(IEnumerable<Type> types)
    {
        var puzzles = new AdventPuzzles();

        foreach (var method in types.SelectMany(t => t.GetMethods().Where(IsPuzzle)))
        {
            if (method
                .GetCustomAttributes<PuzzleAttribute>()
                .SingleOrDefault(att => att is not ExampleAttribute) is { } attr)
            {
                var puzzle = new AdventPuzzle(method, attr.Input, attr.Answer, attr.Order);
                puzzles.items.Add(puzzle);
            }
        }
        return puzzles;

        static bool IsPuzzle(MethodInfo method) => method.GetCustomAttributes<PuzzleAttribute>().Any();
    }

    public IEnumerator<AdventPuzzle> GetEnumerator() => items.OrderBy(item => item.Date).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
