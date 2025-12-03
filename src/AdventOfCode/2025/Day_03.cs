namespace Advent_of_Code_2025;

/// <summary>
/// There are banks of batteries with single digit labels.
/// 
/// Part one: Find biggest number of 2 digits per bank without reordening.
/// Part two: Find biggest number of 12 digits per bank without reordening.
/// </summary>
[Category(Category.SequenceProgression)]
public class Day_03
{
    [Example(answer: 357, "987654321111111;811111111111119;234234234234278;818181911112111")]
    [Puzzle(answer: 17087L, O.μs10)]
    public long part_one(Lines lines) => lines.Sum(l => Number(l, 2));

    [Example(answer: 3121910778619, "987654321111111;811111111111119;234234234234278;818181911112111")]
    [Puzzle(answer: 169019504359949, O.μs10)]
    public long part_two(Lines lines) => lines.Sum(l => Number(l, 12));

    private static long Number(string s, int len)
    {
        var num = 0L; char max; var lo = -1; var f = 0;

        for (var rem = len; rem > 0; rem--)
        {
            max = default;
            // Find the first occurence of the maximum digit.
            for (var i = s.Length - rem; i > lo; i--)
            {
                var ch = s[i];
                if (ch >= max)
                {
                    max = ch;
                    f = i;
                }
            }
            // update the lowerbound.
            lo = f;
            num = num * 10 + (max - '0');
        }
        return num;
    }
}
