namespace Advent_of_Code_2025;

/// <summary>
/// There is a wide range of ID's that have to be validated.
/// 
/// Part one: Sum ID's where digits repeat twice.
/// Part two: Sum ID's where digits repeat at least twice.
/// </summary>
[Category(Category.Cryptography)]
public class Day_02
{
    [Example(answer: 1227775554, "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124")]
    [Puzzle(answer: 31210613313, O.ms10)]
    public long part_one(Int64Ranges ranges) => ranges.Where(One).Sum();

    [Example(answer: 4174379265, "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124")]
    [Puzzle(answer: 41823587546, O.ms10)]
    public long part_two(Int64Ranges ranges) => ranges.Where(Two).Sum();

    static bool One(long n)
    {
        n.TryFormat(chs, out var len);
        var a = chs.AsSpan(..len);
        var h = len / 2;
        return len.IsEven && a[0..h].Is(a[^h..]);
    }

    static bool Two(long n)
    {
        n.TryFormat(chs, out var len);
        var span = chs.AsSpan(..len);

        foreach (var size in Sizes[len])
        {
            var f = span[..size]; var i = size; bool repeat;

            do repeat = f.Is(span[i..(i + size)]);
            while (repeat && (i += size) < len);

            if (repeat) return true;
        }
        return false;
    }

    // Per size the prime factors that can be valid sub sizes.
    static readonly int[][] Sizes = [[], [], [1], [1], [1, 2], [1], [1, 2, 3], [1], [1, 2, 4], [1, 3], [1, 2, 5]];

    static readonly char[] chs = new char[10];
}
