namespace Advent_of_Code_2021;

/// <remarks>
/// .6.  the numbering of the segments of the displays
/// 5 4
/// .3.
/// 2.1
/// .0.
/// </remarks>
[Category(Category.BitManupilation, Category.Cryptography)]
public class Day_08
{
    [Example(answer: 26, Example._1)]
    [Puzzle(answer: 479, O.μs10)]
    public int part_one(Lines lines) => lines.As(line => line[61..])
        .SelectMany(line => line.Split(' '))
        .Count(w => w.Length == 2 || w.Length == 3 || w.Length == 4 || w.Length == 7);

    [Example(answer: 5353, "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")]
    [Example(answer: 61229, Example._1)]
    [Puzzle(answer: 1041746, O.μs10)]
    public int part_two(Lines lines) => lines.As(line => line.Split(" | ")).Sum(Decode);

    private readonly int[] segments = new int[7];

    int Decode(string[] line)
    {
        var sum = 0; var num1 = 127; var num7 = 127; var len5 = 127; var len6 = 127;

        foreach (var code in line[0].Split(' '))
        {
            switch (code.Length)
            {
                case 2: num1 = Display(code); break;
                case 3: num7 = Display(code); break;
                case 5: len5 &= Display(code); break;
                case 6: len6 &= Display(code); break;
            }
        }

        var seg06 = len5 & len6;
        var seg15 = len6 ^ seg06;

        segments[4] = (num7 & num1) & ~seg15;
        segments[3] = len5 ^ seg06;
        segments[1] = seg15 & num1;

        foreach (var str in line[1].Split(' '))
        {
            sum = sum * 10 + Value(str.Length, Display(str));
        }
        return sum;
    }

    int Value(int length, int display)
        => length switch { 2 => 1, 3 => 7, 4 => 4, 7 => 8, 5 => Match5(display), _ => Match6(display) };

    int Match6(int display)
    {
        if ((segments[3] & display) == 0) return 0;
        else if ((segments[4] & display) == 0) return 6;
        else return 9;
    }

    int Match5(int display)
    {
        if ((segments[1] & display) == 0) return 2;
        else if ((segments[4] & display) == 0) return 5;
        else return 3;
    }

    public static int Display(string str)
    {
        int mask = 0;
        foreach (var ch in str) { mask |= 1 << (ch - 'a'); }
        return mask;
    }
}
