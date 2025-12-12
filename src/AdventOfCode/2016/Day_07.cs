namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_07
{
    [Example(answer: 0, "aaaa[qwer]tyui")]
    [Example(answer: 0, "abcd[bddb]xyyx")]
    [Example(answer: 1, "abba[mnop]qrst")]
    [Example(answer: 1, "ioxxoj[asdfgh]zxcvbn")]
    [Puzzle(answer: 105, O.ms)]
    public int part_one(Lines lines) => lines.Count(SupportsTls);

    [Example(answer: 0, "xyx[xyx]xyx")]
    [Example(answer: 1, "aba[bab]xyz")]
    [Example(answer: 1, "zazbz[bzb]cdb")]
    [Puzzle(answer: 258, O.ms)]
    public int part_two(Lines lines) => lines.Count(SupportsSsl);

    static bool SupportsTls(string line)
    {
        var support = false;
        var odd = true;
        foreach (var block in line.Separate('[', ']'))
        {
            if (odd) support |= ABBA(block);
            else if (ABBA(block)) return false;
            odd = !odd;
        }
        return support;
    }

    static bool ABBA(string str) => str.SelectWithPrevious(4).Any(block
       => block[0] == block[3]
       && block[1] == block[2]
       && block[0] != block[1]);

    static bool SupportsSsl(string line)
    {
        var blocks = line.Separate('[', ']');
        var odds = blocks.WithStep(2).SelectMany(line => line.SelectWithPrevious(3));
        var evens = blocks.Skip(1).WithStep(2).FixMany(line => line.SelectWithPrevious(3));
        return odds.Any(odd => evens.Exists(even => ABA_BAB(odd, even)));
    }

    static bool ABA_BAB(string l, string r)
        => r[1] == l[0] && r[1] == l[2]
        && l[1] == r[0] && l[1] == r[2]
        && l[0] != l[1];
}

