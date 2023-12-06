namespace Advent_of_Code_2021;

[Category(Category.SequenceProgression)]
public class Day_14
{
    [Example(answer: 1588, "NNCB;CH -> B;HH -> N;CB -> H;NH -> C;HB -> C;HC -> B;HN -> C;NN -> C;BH -> H;NC -> B;NB -> B;BN -> B;BB -> N;BC -> B;CC -> N;CN -> C")]
    [Puzzle(answer: 3247L, O.μs10)]
    public long part_one(Lines lines) => Run(lines, 10);

    [Example(answer: 2188189693529, "NNCB;CH -> B;HH -> N;CB -> H;NH -> C;HB -> C;HC -> B;HN -> C;NN -> C;BH -> H;NC -> B;NB -> B;BN -> B;BB -> N;BC -> B;CC -> N;CN -> C")]
    [Puzzle(answer: 4110568157153, O.μs100)]
    public long part_two(Lines lines) => Run(lines, 40);

    static long Run(Lines lines, int steps)
    {
        var insertions = lines.Skip(1).Select(Insertion.Parse).ToDictionary(i => i.Pair, i => i);
        var pairs = new ItemCounter<string> { lines[0].SelectWithPrevious() };

        for (var step = 0; step < steps; step++)
        {
            foreach (var pair in pairs.ToArray())
            {
                Adjust(insertions, pairs, pair.Item, pair.Count);
            }
        }
        var counter = new ItemCounter<char>();
        counter[lines[0][^1]]++;
        foreach (var kvp in pairs) { counter[kvp.Item[0]] += kvp.Count; }
        return counter.Counts.Max() - counter.Counts.Min();
    }

    static void Adjust(Dictionary<string, Insertion> insertions, ItemCounter<string> pairs, string pair, long count)
    {
        var insert = insertions[pair];
        pairs[pair] -= count;
        pairs[insert.First] += count;
        pairs[insert.Second] += count;
    }

    record Insertion(string Pair, string First, string Second)
    {
        public static Insertion Parse(string line) => new(line[0..2], $"{line[0]}{line[^1]}", $"{line[^1]}{line[1]}");
    }
}
