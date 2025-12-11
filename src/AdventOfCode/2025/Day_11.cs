namespace Advent_of_Code_2025;

/// <summary>
/// There is a directed graph defined (without loops).
///
/// Part one: Find the unique paths from you to out.
/// Part two: Fint the unique paths from svr to out via fft and dac.
/// </summary>
[Category(Category.PathFinding, Category.Graph)]
public class Day_11
{
    [Example(answer: 5, Example._1)]
    [Example(answer: 2, Example._3)]
    [Puzzle(answer: 590L, O.μs10)]
    public long part_one(Lines lines) => Graph.New(lines).Paths("you", "out");

    [Example(answer: 2, Example._2)]
    [Example(answer: 377452269415704, Example._3)]
    [Puzzle(answer: 319473830844560, O.μs100)]
    public long part_two(Lines lines)
    {
        var graph = Graph.New(lines);
        // It turns out that furst dac and fft afterwards is not possible.
        return graph.Paths("svr", "fft") * graph.Paths("fft", "dac") * graph.Paths("dac", "out");
    }

    class Graph(Dictionary<string, string[]> nodes) : Dictionary<string, long>
    {
        public long Paths(string f, string t) => (f + t) switch
        {
            var p when TryGetValue(p, out var c) => c,
            var _ when f == t => 1,
            var p => this[p] = nodes[f].Skip(1).Sum(o => Paths(o, t)),
        };

        public static Graph New(Lines lines)
            => new(lines.As(l => l.Separate(' ', ':')).Concat([["out"]]).ToDictionary(p => p[0], p => p));
    }
}
