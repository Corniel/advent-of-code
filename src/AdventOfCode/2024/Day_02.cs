namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public class Day_02
{
    [Example(answer: 2, "7 6 4 2 1;1 2 7 8 9;9 7 6 2 1;1 3 2 4 5;8 6 4 4 1;1 3 6 7 9")]
    [Puzzle(answer: 526, O.μs100)]
    public int part_one(Lines lines) => lines.As(One).Count(Safe);

    [Example(answer: 4, "7 6 4 2 1;1 2 7 8 9;9 7 6 2 1;1 3 2 4 5;8 6 4 4 1;1 3 6 7 9")]
    [Puzzle(answer: 566, O.μs100)]
    public int part_two(Lines lines) => lines.As(Two).Count(Safe);

    static int[] One(string l) => [.. l.Int32s()];

    static int[][] Two(string l)
    {
        var ns = One(l);
        var options = new int[ns.Length][];

        for (var i = 0; i < ns.Length; i++)
        {
            options[i] = [.. ns[0..i], .. ns[(i + 1)..]];
        }
        return options;
    }

    static bool Safe(int[][] ns) => ns.Any(Safe);

    static bool Safe(int[] ns)
        => ns.SelectWithPrevious().All(p => (p.Current - p.Previous).InRange(+1, +3))
        || ns.SelectWithPrevious().All(p => (p.Current - p.Previous).InRange(-3, -1));
}
