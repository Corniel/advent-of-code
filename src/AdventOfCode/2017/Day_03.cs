namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_03
{
    [Example(answer: 3, "12")]
    [Example(answer: 2, "23")]
    [Example(answer: 4, "25")]
    [Puzzle(answer: 438, "265149")]
    public int part_one(string input) => Point.O.ManhattanDistance(Location(input.Int32()));

    [Puzzle(answer: 266330, "265149")]
    public int part_two(string input)
    {
        var grid = new Dictionary<Point, int>() { { Point.O, 1 } };
        var max = Enumerable.Range(2, short.MaxValue).First(n => Sum(n, grid) > input.Int32());
        return grid[Location(max)];
    }

    static Point Location(int n)
    {
        int depth = (1 + (n - 1).Sqrt()) / 2;
        var size = depth * 2;
        var index = n - (size - 1).Sqr();
        var rot = (DiscreteRotation)(index / size);
        var @out = Vector.E.Rotate(rot) * depth;
        var offset = Vector.N.Rotate(rot) * ((index % size) - depth);
        return Point.O + @out + offset;
    }

    static int Sum(int n, Dictionary<Point, int> grid)
    {
        var loc = Location(n);
        return grid[loc] = loc.Projections(CompassPoints.All.ToVectors())
            .Sum(n => grid.TryGetValue(n, out var v) ? v : 0);
    }
}