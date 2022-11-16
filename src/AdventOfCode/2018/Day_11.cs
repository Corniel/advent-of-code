namespace Advent_of_Code_2018;

public class Day_11
{
    [Puzzle(answer: "21,77", "3999")]
    public string part_one(string input)
    {
        var max = FindMaximum(Grid(input)).Point;
        return $"{max.X},{max.Y}";
    }

    [Puzzle(answer: "224,222,27", "3999")]
    public string part_two(string input)
    {
        var grid = Grid(input);
        var max = Enumerable.Range(3, 50).Select(size => FindMaximum(grid, size)).OrderByDescending(r => r.Max).First();
        return $"{max.Point.X},{max.Point.Y},{max.Size}";
    }

    private static Result FindMaximum(Grid<int> grid, int size = 3)
    {
        var vectors = Points.Range(Point.O, new(size - 1, size - 1)).Select(p => p.Vector()).ToArray();
        return grid.Positions.Where(p => p.X <= 300 - size && p.Y <= 300 - size)
            .Select(p => new Result(p, vectors.Sum(v => grid[p + v]), size))
            .OrderByDescending(r => r.Max)
            .First();
    }

    static Grid<int> Grid(string serial)
    {
        var grid = new Grid<int>(301, 301);
        foreach (var point in grid.Positions)
        {
            grid[point] = Power(point, serial.Int32());
        }
        return grid;

        static int Power(Point point, int serial)
        {
            var rackId = point.X + 10;
            var power = (point.Y * rackId + serial) * rackId;
            return (power / 100 % 10) - 5;
        }
    }

    record Result(Point Point, int Max, int Size);
}
