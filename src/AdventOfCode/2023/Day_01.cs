namespace Advent_of_Code_2023;

[Category(Category.Cryptography)]
public class Day_01
{
    [Example(answer: 142, "1abc2;pqr3stu8vwx;a1b2c3d4e5f;treb7uchet")]
    [Puzzle(answer: 56108, O.μs100)]
    public int part_one(Lines input) => input.Select(l => Number(l, false)).Sum();

    [Example(answer: 281, "two1nine;eightwothree;abcone2threexyz;xtwone3four;4nineeightseven2;zoneight234;7pqrstsixteen")]
    [Example(answer: 83, "eightwothree")]
    [Example(answer: 88, "vrpplrtqxvssgnvdf8")]
    [Example(answer: 54925, Example._1)]
    [Puzzle(answer: 55652, O.ms)]
    public int part_two(Lines input) => input.Select(l => Number(l, true)).Sum();

    int Number(string line, bool words)
    {
        var n = line.Select(Digit).WithValue().ToArray();
        return n[0] * 10 + n[^1];

        int? Digit(char ch, int i)
        {
            if (char.IsDigit(ch)) return ch - '0';
            else return words && line[i..] is { Length: >= 3 } ln
                ? Range(1, 9).FirstOrNone(n => ln.StartsWith(ws[n]))
                : null;
        }
    }

    readonly string[] ws = ["", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
}
