namespace Advent_of_Code_2021;

public class Day_17
{
    [Example(answer: 45, "target area: x=20..30, y=-10..-5")]
    [Puzzle(answer: 17766, input: "target area: x=48..70, y=-189..-148")]
    public int part_one(string input)
    {
        var area = Area.Parse(input);
        return area.Y_lo * (area.Y_lo + 1) / 2;
    }

    [Example(answer: 112, "target area: x=20..30, y=-10..-5")]
    [Puzzle(answer: 1733, input: "target area: x=48..70, y=-189..-148")]
    public long part_two(string input)
    {
        var area = Area.Parse(input);
        var x_lo = (int)Math.Ceiling((-1 + Math.Sqrt(8 * area.X_lo + 1)) / 2);
        return Points
            .Range(new Point(x_lo, area.Y_lo), new Point(area.X_hi, -area.Y_lo))
            .Count(v => Hits(v.Vector(), area));
    }

    static bool Hits(Vector velocity, Area area)
    {
        var pos = Point.O;
        while (pos.X <= area.X_hi && pos.Y >= area.Y_lo)
        {
            if (pos.X >= area.X_lo && pos.Y <= area.Y_hi) return true;
            pos += velocity;
            velocity = new Vector(Math.Max(0, velocity.X - 1), velocity.Y - 1);
        }
        return false;
    }

    record Area(int X_hi, int X_lo, int Y_hi, int Y_lo)
    {
        public static Area Parse(string line)
        {
            var vals = line.Int32s().ToArray();
            return new(X_hi: vals[1], X_lo: vals[0], Y_hi: vals[3], Y_lo: vals[2]);
        }
    }
}
