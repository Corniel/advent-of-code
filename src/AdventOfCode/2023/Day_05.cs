namespace Advent_of_Code_2023;

[Category(Category.SequenceProgression)]
public class Day_05
{
    [Example(answer: 35, Example._1)]
    [Puzzle(answer: 3374647L, O.μs100)]
    public long part_one(GroupedLines groups) => Process(groups, One);

    [Example(answer: 46, Example._1)]
    [Puzzle(answer: 6082852L, O.μs100)]
    public long part_two(GroupedLines groups) => Process(groups, Two);

    static IEnumerable<Int64Range> One(long[] ns) => ns.Select(n => new Int64Range(n));

    static IEnumerable<Int64Range> Two(long[] ns) => Range(0, ns.Length / 2).Select(i => new Int64Range(ns[2 * i], ns[2 * i] + ns[2 * i + 1]));

    private static long Process(GroupedLines groups, Func<long[], IEnumerable<Int64Range>> select)
    {
        var sources = select(groups[0][0].Int64s().ToArray()).ToList();
        var targets = new List<Int64Range>();

        foreach (var maps in groups.Skip(1).Select(Map.Parse))
        {
            targets.Clear();

            var unchanged = sources.ToList();

            foreach (var map in maps)
            {
                foreach (var section in sources.Select(s => s.Intersection(map.Range)).Where(i => !i.IsEmpty))
                {
                    unchanged = unchanged.Except(section);
                    targets.Add(map.Next(section));
                }
            }
            targets.AddRange(unchanged);

            (sources, targets) = (targets, sources);
        }
        return sources.Min(t => t.Lower);
    }

    record struct Map(long Dest, long Source, long Length)
    {
        public Int64Range Range => new(Source, Source + Length);

        public Int64Range Next(Int64Range start) => new(start.Lower + Dest - Source, start.Upper + Dest - Source);

        public static Map[] Parse(string[] lines) => lines[1..].Select(l => Ctor.New<Map>(l.Int64s())).ToArray();
    }
}
