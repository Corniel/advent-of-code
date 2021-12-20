using Advent_of_Code_2019.IntComputing;

namespace Advent_of_Code_2019;

[Category(Category.IntComputer, Category.ASCII)]
public class Day_11
{
    [Puzzle(answer: 2184, year: 2019, day: 11)]
    public int part_one(string input)
        => DrawCanvas(Computer.Parse(input), 0).Count;

    [Puzzle(answer: "AHCHZEPK", year: 2019, day: 11)]
    public string part_two(string input)
    {
        var computer = Computer.Parse(input);
        var dots = DrawCanvas(computer, 1)
            .Where(kvp => kvp.Value == 1)
            .Select(kvp => kvp.Key);
        return Grid<bool>.FromPoints(dots, true).AsciiText(trim: true);
    }

    private static Dictionary<Point, int> DrawCanvas(Computer computer, int color)
    {
        var canvas = new Dictionary<Point, int> { { Point.O, color } };
        var bot = Point.O;
        var dir = Vector.N;

        while (!computer.Finished)
        {
            canvas.TryGetValue(bot, out color);
            color = (int)computer.Run(new RunArguments(false, true, color)).LastOrDefault();
            var turn = computer.Run(new RunArguments(false, true)).LastOrDefault();

            canvas[bot] = color;
            dir = dir.Rotate(turn == 0 ? DiscreteRotation.Deg090 : DiscreteRotation.Deg270);
            bot += dir;
        }

        return canvas;
    }
}
