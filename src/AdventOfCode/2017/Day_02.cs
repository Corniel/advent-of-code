namespace Advent_of_Code_2017;

[Category(Category.Cryptography)]
public class Day_02
{
    [Example(answer: 18, "5 1 9 5;7 5 3;2 4 6 8")]
    [Puzzle(answer: 44216)]
    public int part_one(string input) => input.Lines(AsNumbers).Select(MaxMin).Sum();

    [Example(answer: 9, "5 9 2 8; 9 4 7 3;3 8 6 5")]
    [Puzzle(answer: 320)]
    public int part_two(string input) => input.Lines(AsNumbers).Select(Checksum).Sum();

    static int[] AsNumbers(string line) => line.Int32s().Order().ToArray();
    static int MaxMin(int[] n) => n[^1] - n[0];

    static int Checksum(int[] n)
    {
        for (var r = 1; r < n.Length; r++)
        {
            for (var l = 0; l < r; l++)
            {
                var max = n[r]; var min = n[l];
                if (max != min && max % min == 0) return max / min;
            }
        }
        throw new NoAnswer();
    }
}
