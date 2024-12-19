namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public class Day_19
{
    [Example(answer: 6, Example._1)]
    [Puzzle(answer: 304L, O.ms)]
    public long part_one(Lines lines) => Count(lines, (ws, ls) => ws.Count(w => Count(w, ls) != 0));

    [Example(answer: 16, Example._1)]
    [Puzzle(answer: 705756472327497, O.ms)]
    public long part_two(Lines lines) => Count(lines, (ws, ls) => ws.Sum(w => Count(w, ls)));

    static long Count(Lines lines, Func<Slice<string>, Dictionary<char, string[]>, long> count)
    {
        var tokens = lines[0].CommaSeparated().GroupBy(r => r[0]).ToDictionary(r => r.Key, r => r.ToArray());
        return count(lines[1..], tokens);
    }

    static long Count(ReadOnlySpan<char> word, Dictionary<char, string[]> tokens)
    {
        var counts = new long[word.Length + 1]; counts[0] = 1;

        for (var i = 0; i < word.Length; i++)
        {
            if (counts[i] != 0 && tokens.TryGetValue(word[i], out var startsWith))
            {
                var cnt = counts[i]; var sub = word[i..];
                foreach (var token in startsWith)
                {
                    if (sub.StartsWith(token)) counts[i + token.Length] += cnt;
                }
            }
        }
        return counts[^1];
    }
}
