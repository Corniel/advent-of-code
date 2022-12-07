﻿namespace Advent_of_Code_2022;

[Category(Category.μs, Category.Simulation, Category.Grid)]
public class Day_05
{
    [Example(answer: "CMZ", 1)]
    [Puzzle(answer: "RNZLFZSJH")]
    public string part_one(string input) => Restack(input, SingleStack);

    [Example(answer: "MCD", 1)]
    [Puzzle(answer: "CNSFCGJSM")]
    public string part_two(string input) => Restack(input, MultiStack);

    static string Restack(string input, Action<IEnumerable<Move>, Stack<char>[]> apply)
    {
        var groups = input.GroupedLines(StringSplitOptions.None).ToArray();
        var grid = groups[0].CharPixels(false);
        var stacks = Range(0, 2 + grid.Cols / 4).Select(_ => new Stack<char>()).ToArray();

        foreach (var pixel in grid.OrderByDescending(p => p.Key.Y).Where(p => char.IsAsciiLetterUpper(p.Value)))
        {
            stacks[1 + pixel.Key.X / 4].Push(pixel.Value);
        }
        apply(groups[1..].SelectMany(g => g).Select(Move.Parse), stacks);

        return new(stacks.Skip(1).Select(s => s.Pop()).ToArray());
    }

    static void SingleStack(IEnumerable<Move> moves, Stack<char>[] stacks)
    {
        foreach (var move in moves.SelectMany(m => Repeat(m, m.Repeat)))
        {
            stacks[move.To].Push(stacks[move.From].Pop());
        }
    }

    private static void MultiStack(IEnumerable<Move> moves, Stack<char>[] stacks)
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