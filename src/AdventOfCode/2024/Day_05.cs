namespace Advent_of_Code_2024;

[Category(Category.Computation, Category.Sorting)]
public class Day_05
{
    [Example(answer: 143, Example._1)]
    [Puzzle(answer: 5087, O.μs100)]
    public int part_one(GroupedLines groups) => Process(groups[1], new(groups[0]), true, (ns, _) => ns[ns.Length / 2]);

    [Example(answer: 123, Example._1)]
    [Puzzle(answer: 4971, O.μs100)]
    public int part_two(GroupedLines groups) => Process(groups[1], new(groups[0]), false, Two);

    static int Process(string[] lines, Sort sort, bool correct, Func<int[], Sort, int> sum) => lines
        .Select(l => l.Int32s().ToArray())
        .Where(ns => Correct(ns, sort) == correct)
        .Sum(ns => sum(ns, sort));

    static int Two(int[] ns, Sort sort) { Array.Sort(ns, sort); return ns[ns.Length / 2]; }

    static bool Correct(int[] ns, Sort sort) => ns.RoundRobin().All(p => sort.Compare(p.First, p.Second) != +1);

    class Sort(string[] lines) : IComparer<int>
    {
        readonly HashSet<Pair<int>> Pairs = new(lines.Int32s().ChunkBy(2).Select(c => new Pair<int>(c[0], c[1])));

        public int Compare(int x, int y) => Pairs.Contains(new(x, y)) ? -1 : Pairs.Contains(new(y, x)) ? 1 : 0;
    }
}
