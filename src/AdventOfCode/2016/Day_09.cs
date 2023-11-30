namespace Advent_of_Code_2016;

[Category(Category.ExpressionParsing)]
public class Day_09
{
    [Example(answer: 6, "ADVENT")]
    [Example(answer: 7, "A(1x5)BC")]
    [Example(answer: 9, "(3x3)XYZ")]
    [Example(answer: 18, "X(8x2)(3x3)ABCY")]
    [Puzzle(answer: 152851, O.μs)]
    public long part_one(string input) => Unzip(input, false);

    [Example(answer: 6, "ADVENT")]
    [Example(answer: 9, "(3x3)XYZ")]
    [Example(answer: 445, "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN")]
    [Puzzle(answer: 11797310782, O.μs10)]
    public long part_two(string input) => Unzip(input, true);

    private static long Unzip(string input, bool recusive)
    {
        var len = 0L; var buf = 0;

        for (var pos = 0; pos < input.Length; pos++)
        {
            var ch = input[pos];
            
            if (ch == '(')
            {
                len += buf;
                buf = 0;
            }
            else if (ch == ')')
            {
                var exp = input.Substring(pos - buf, buf);
                var size = exp.Int32();
                var repeat = exp.Int32s().Last();
                len += repeat * (recusive ? Unzip(input.Substring(pos + 1, size), true) : size);
                pos += size;
                buf = 0;
            }
            else buf++;
        }
        return len + buf;
    }
}
