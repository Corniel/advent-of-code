namespace Advent_of_Code_2019;

[Category(Category.IntComputer, Category.ASCII)]
public class Day_11
{
    [Puzzle(answer: 2184, O.ms)]
    public int part_one(string str) => DrawCanvas(Computer.Parse(str), 0).Count;

    [Puzzle(answer: "AHCHZEPK", O.μs100)]
    public string part_two(string str)
    {
        var computer = Computer.Parse(str);
        var dots = DrawCanvas(computer, 1)
            .Where(kvp => kvp.Value == 1)
            .Select(kvp => kvp.Key);
        return Grid<bool>.FromPoints(dots, true).AsciiText(trim: true);
    }

    static Dictionary<Point, int> DrawCanvas(Computer computer, int color)
    {
        var canvas = new Dictionary<Point, int> { { Point.O, color } };
        var bot = new Cursor(Point.O, Vector.N);

        while (!computer.Finished)
        {
            canvas.TryGetValue(bot.Pos, out color);
            color = (int)computer.Run(new RunArguments(false, true, color)).LastOrDefault();
            var turn = computer.Run(new RunArguments(false, true)).LastOrDefault();

            canvas[bot.Pos] = color;
            bot = bot.Rotate(turn == 0 ? DiscreteRotation.Deg090 : DiscreteRotation.Deg270).Move();
        }
        return canvas;
    }
}
