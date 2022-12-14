﻿namespace Advent_of_Code_2016;

[Category(Category.μs, Category.Grid)]
public class Day_08
{
    [Puzzle(answer: 115)]
    public int part_one(string input) => Process(input).Count(c => c.Value);

    [Puzzle(answer: "EFEYKFRFIJ")]
    public string part_two(string input) => Ascii(Process(input));

    private static string Ascii(Dictionary<Point, bool> grid)
    {
        var max = grid.Where(k => k.Value).Select(kvp => kvp.Key).Max();
        var canvas = new Grid<bool>(max);
        foreach (var p in grid) canvas[p.Key] = p.Value;
        return canvas.AsciiText();
    }

    private static Dictionary<Point, bool> Process(string input)
    {
        var grid = new Dictionary<Point, bool>();
        foreach (var inst in input.Lines(Instruction.Parse)) inst.Transform(grid);
        return grid;
    }

    abstract record Instruction
    {
       public abstract void Transform(Dictionary<Point, bool> grid);

        public static Instruction Parse(string line) => new string(line.Take(10).ToArray()) switch
        {
            "rotate row" => Ctor.New<RotateRow>(line.Int32s()),
            "rotate col" => Ctor.New<RotateCol>(line.Int32s()),
            _ => Ctor.New<Rect>(line.Int32s()),
        };
    }

    record Rect(int Cols, int Rows) : Instruction
    {
        public override void Transform(Dictionary<Point, bool> grid)
        {
            foreach (var point in Points.Range(Point.O, new(Cols -1 , Rows - 1))) grid[point] = true;
        }
    }

    record RotateCol(int X, int By) : Instruction
    {
        public override void Transform(Dictionary<Point, bool> grid)
        {
            var column = grid.Where(p => p.Key.X == X && p.Value).Select(p => p.Key).ToArray();
            foreach (var cell in column) grid[cell] = false;
            foreach (var cell in column) grid[new(cell.X , (cell.Y + By).Mod(6))] = true;
        }
    }

    record RotateRow(int Y, int By) : Instruction
    {
        public override void Transform(Dictionary<Point, bool> grid)
        {
            var column = grid.Where(p => p.Key.Y == Y && p.Value).Select(p => p.Key).ToArray();
            foreach (var cell in column) grid[cell] = false;
            foreach (var cell in column) grid[new((cell.X + By).Mod(50), cell.Y)] = true;
        }
    }
}
