namespace Advent_of_Code_2018;

[Category(Category.ASCII, Category.VectorAlgebra)]
public class Day_10
{
    [Puzzle(answer: @"
█░░░░█░░██████░░█░░░░█░░█████░░░█░░░░░░░█████░░░█░░░░█░░█░░░░█
██░░░█░░█░░░░░░░█░░░░█░░█░░░░█░░█░░░░░░░█░░░░█░░█░░░░█░░█░░░█░
██░░░█░░█░░░░░░░░█░░█░░░█░░░░█░░█░░░░░░░█░░░░█░░░█░░█░░░█░░█░░
█░█░░█░░█░░░░░░░░█░░█░░░█░░░░█░░█░░░░░░░█░░░░█░░░█░░█░░░█░█░░░
█░█░░█░░█████░░░░░██░░░░█████░░░█░░░░░░░█████░░░░░██░░░░██░░░░
█░░█░█░░█░░░░░░░░░██░░░░█░░░░░░░█░░░░░░░█░░█░░░░░░██░░░░██░░░░
█░░█░█░░█░░░░░░░░█░░█░░░█░░░░░░░█░░░░░░░█░░░█░░░░█░░█░░░█░█░░░
█░░░██░░█░░░░░░░░█░░█░░░█░░░░░░░█░░░░░░░█░░░█░░░░█░░█░░░█░░█░░
█░░░██░░█░░░░░░░█░░░░█░░█░░░░░░░█░░░░░░░█░░░░█░░█░░░░█░░█░░░█░
█░░░░█░░██████░░█░░░░█░░█░░░░░░░██████░░█░░░░█░░█░░░░█░░█░░░░█", O.ms10)]
    public string part_one(Lines input)=> Simulate(input).Grid.ToString(b => b ? '█' : '░');

    [Puzzle(answer: 10459, O.ms10)]
    public int part_two(Lines input) => Simulate(input).Seconds;

    static (int Seconds, Grid<bool> Grid) Simulate(Lines input)
    {
        var dots = input.As(Dot.Parse).ToArray();
        var seconds = 0;
        while (true)
        {
            seconds++;

            foreach (var dot in dots)
            {
                dot.Position += dot.Velocity;
            }

            var min_x = dots.Min(d => d.Position.X);
            var min_y = dots.Min(d => d.Position.Y);
            var max_x = dots.Max(d => d.Position.X);
            var max_y = dots.Max(d => d.Position.Y);

            if (max_y - min_y == 9)
            {
                var min = new Vector(min_x, min_y);
                var max = new Vector(max_x, max_y);
                var grid = new Grid<bool>(cols: (max - min).X + 1, rows: (max - min).Y + 1);
                grid.Set(true, dots.Select(dot => dot.Position - min));
                return (seconds, grid);
            }
        }
        throw new InfiniteLoop();
    }

    class Dot
    {
        public Point Position { get; set; }
        public Vector Velocity { get; init; }

        public static Dot Parse(string line)
        {
            var numbers = line.Int32s().ToArray();
            return new() { Position = new(numbers[0], numbers[1]), Velocity = new(numbers[2], numbers[3]) };
        }
    }
}
