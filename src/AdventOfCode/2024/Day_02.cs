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
        int[] ns = [.. l.Int32s()];
        var options = new int[ns.Length + 1][];
        options[0] = ns;

        for (var i = 1; i <= ns.Length; i++)
        {
            options[i] = [.. ns[0..(i - 1)], .. ns[i..]];
        }
        return options;
    }

    static bool Safe(int[][] ns) => ns.Any(Safe);

    static bool Safe(int[] ns)
        => ns.SelectWithPrevious().All(p => (p.Current - p.Previous).InRange(+1, +3))
        || ns.SelectWithPrevious().All(p => (p.Current - p.Previous).InRange(-3, -1));
}
