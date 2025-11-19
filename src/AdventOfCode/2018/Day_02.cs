namespace Advent_of_Code_2018;

[Category(Category.Cryptography)]
public class Day_02
{
    [Puzzle(answer: 5928, O.μs10)]
    public int part_one(Lines lines)
        => lines.Count(line => Repeat(line, 2))
        * lines.Count(line => Repeat(line, 3));


    static bool Repeat(string line, int n) => line.Any(ch => line.Count(ch) == n);

    [Example(answer: "fgij", "abcde;fghij;klmno;pqrst;fguij;axcye;wvxyz")]
    [Puzzle(answer: "bqlporuexkwzyabnmgjqctvfs", O.μs100)]
    public string part_two(Lines lines) => lines.RoundRobin().Select(Matching).First(m => m is { });

    static string Matching(Pair<string> p)
    {
        var matching = new char[p.First.Length - 1];
        var missing = 0;
        for (var i = 0; i < p.First.Length; i++)
        {
            if (p.First[i] == p.Second[i])
            {
                matching[i - missing] = p.First[i];
            }
            else if (missing++ > 0) return null;
        }
        return new(matching);
    }
}
