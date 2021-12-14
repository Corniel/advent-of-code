namespace Advent_of_Code_2021;

public class Day_14
{
    [Example(answer: 1588, "NNCB;CH -> B;HH -> N;CB -> H;NH -> C;HB -> C;HC -> B;HN -> C;NN -> C;BH -> H;NC -> B;NB -> B;BN -> B;BB -> N;BC -> B;CC -> N;CN -> C")]
    [Puzzle(answer: 3247, year: 2021, day: 14)]
    public long part_one(string input) => Run(input, 10);

    [Example(answer: 2188189693529, "NNCB;CH -> B;HH -> N;CB -> H;NH -> C;HB -> C;HC -> B;HN -> C;NN -> C;BH -> H;NC -> B;NB -> B;BN -> B;BB -> N;BC -> B;CC -> N;CN -> C")]
    [Puzzle(answer: 4110568157153, year: 2021, day: 14)]
    public long part_two(string input) => Run(input, 40);

    private static long Run(string input, int steps)
    {
        var lines = input.Lines();
        var insertions = lines.Skip(1).Select(Insertion.Parse).ToDictionary(i => i.Pair, i => i);
        var pairs = new ItemInt64Counter<string>();
        pairs.Add(lines[0].SelectWithPrevious());

        for (var step = 0; step < steps; step++)
        {
            foreach (var kvp in pairs.ToArray())
            {
                Adjust(insertions, pairs, kvp.Key, kvp.Value);
            }
        }
        var counter = new ItemInt64Counter<char>();
        counter[lines[0][^1]]++;
        foreach (var kvp in pairs) { counter[kvp.Key[0]] += kvp.Value; }
        return counter.Counts.Max() - counter.Counts.Min();
    }

    private static void Adjust(Dictionary<string, Insertion> insertions, ItemInt64Counter<string> pairs, string pair, long count)
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