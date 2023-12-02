namespace Advent_of_Code_2018;

[Category(Category.Cryptography)]
public class Day_02
{
    [Puzzle(answer: 5928, O.μs100)]
    public int part_one(Lines lines)
        => lines.Count(line => Repeat(line, 2))
        * lines.Count(line => Repeat(line, 3));


    static bool Repeat(string line, int n) => line.Any(ch => line.Count(c => c == ch) == n);

    [Example(answer: "fgij", "abcde;fghij;klmno;pqrst;fguij;axcye;wvxyz")]
    [Puzzle(answer: "bqlporuexkwzyabnmgjqctvfs", O.μs100)]
    public string part_two(Lines lines)
    {
        for (var f = 0; f < lines.Count; f++)
        {
            for (var s = f + 1; s < lines.Count; s++)
            {
                if (Matching(lines[f], lines[s]) is { } match) return match;
            }
        }
        throw new NoAnswer();
    }

    static string Matching(string f, string s)
    {
        var matching = new char[f.Length - 1];
        var missing = 0;
        for (var i = 0; i < f.Length; i++)
        {
            if (f[i] == s[i])
            {
                matching[i - missing] = f[i];
            }
            else if (missing++ > 0) return null;
        }
        return new(matching);
    }
}
