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
    public string part_one(Lines lines) => Simulate(lines).Grid.ToString(b => b ? '█' : '░');

    [Puzzle(answer: 10459, O.ms10)]
    public int part_two(Lines lines) => Simulate(lines).Seconds;

    static (int Seconds, Grid<bool> Grid) Simulate(Lines lines)
    {
        var dots = lines.ToArray(Dot.Parse);
        for (var s = 1; s < int.MaxValue; s++)
        {
            foreach (var dot in dots) dot.Position += dot.Velocity;

            var min_y = dots.Min(d => d.Position.Y);
            var max_y = dots.Max(d => d.Position.Y);

            if (max_y - min_y == 9)
            {
                var min = new Vector(dots.Min(d => d.Position.X), min_y);
                var max = new Vector(dots.Max(d => d.Position.X), max_y);
                var grid = new Grid<bool>(cols: (max - min).X + 1, rows: (max - min).Y + 1);
                return (s, grid.Set(true, dots.Select(dot => dot.Position - min)));
            }
        }
        throw new InfiniteLoop();
    }

    record class Dot(Vector Velocity)
    {
        public Point Position { get; set; }

        public static Dot Parse(string line)
        {
            int[] ns = [.. line.Int32s()];
            return new(new Vector(ns[2], ns[3])) { Position = new(ns[0], ns[1]) };
        }
    }
}
