namespace Advent_of_Code_2015;

[Category(Category.Cryptography)]
public class Day_05
{
    [Puzzle(answer: 236, O.μs100)]
    public int part_one(string input) => input.Lines().Count(IsNice1);

    [Example(answer: 0, "ieodomkazucvgmuy")]
    [Example(answer: 0, "uurcxstgmygtbstg")]
    [Example(answer: 1, "qjhvhtzxzqqjkmpb")]
    [Example(answer: 1, "xxyxx")]
    [Puzzle(answer: 51, O.μs100)]
    public int part_two(string input) => input.Lines().Count(IsNice2);

    static bool IsNice1(string line)
        => Repeating(line)
        && AtLeast3Vowels(line)
        && !ContainsAbCdPqXy(line);

    static bool Repeating(string line) => line.SelectWithPrevious().Any(pair => pair[0] == pair[1]);
    static bool AtLeast3Vowels(string line) => line.Count(ch => "aeiou".IndexOf(ch) != -1) >= 3;
    static bool ContainsAbCdPqXy(string line)
        => line.Contains("ab")
        || line.Contains("cd")
        || line.Contains("pq")
        || line.Contains("xy");

    static bool IsNice2(string line) => RepeatingPair(line) && RepeatAt2(line);

    static bool RepeatAt2(string line)
    {
        for (var i = 0; i < line.Length - 3; i++)
        {
            if (line[(i + 2)..].Contains(line[i..(i + 2)])) return true;
        }
        return false;
    }

    static bool RepeatingPair(string line) => line.SelectWithPrevious(3).Any(history => history[0] == history[2]);
}
