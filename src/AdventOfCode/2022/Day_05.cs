namespace Advent_of_Code_2022;

[Category(Category.μs, Category.Simulation, Category.Grid)]
public class Day_05
{
    [Example(answer: "CMZ", 1)]
    [Puzzle(answer: "RNZLFZSJH")]
    public string part_one(string input) => Restack(input, SingleStack);

    [Example(answer: "MCD", 1)]
    [Puzzle(answer: "CNSFCGJSM")]
    public string part_two(string input) => Restack(input, MultiStack);

    static string Restack(string input, Action<Move[], Stack<char>[]> apply)
    {
        var groups = input.GroupedLines(StringSplitOptions.None).ToArray();
        var grid = groups[0].CharPixels(false).Grid();
        var stacks = Enumerable.Range(0, 2 + grid.Cols / 4).Select(_ => new Stack<char>()).ToArray();

        foreach (var pixel in grid.OrderByDescending(p => p.Key.Y).Where(p => char.IsAsciiLetterUpper(grid[p.Key])))
        {
            stacks[1 + pixel.Key.X / 4].Push(grid[pixel.Key]);
        }
        apply(groups[1..].SelectMany(g => g).Select(Move.Parse).ToArray(), stacks);

        return new(stacks.Skip(1).Select(s => s.Pop()).ToArray());
    }

    static void SingleStack(Move[] moves, Stack<char>[] stacks)
    {
        foreach (var move in moves.SelectMany(m => Enumerable.Repeat(m, m.Repeat)))
        {
            stacks[move.To].Push(stacks[move.From].Pop());
        }
    }

    private static void MultiStack(Move[] moves, Stack<char>[] stacks)
    {
        var buffer = new Stack<char>();
        foreach (var move in moves)
        {
            for (var i = 0; i < move.Repeat; i++)
            {
                buffer.Push(stacks[move.From].Pop());
            }
            while (buffer.Any()) stacks[move.To].Push(buffer.Pop());
        }
    }

    record Move(int Repeat, int From, int To)
    {
        public static Move Parse(string line) => Ctor.New<Move>(line.Int32s());
    }
}
