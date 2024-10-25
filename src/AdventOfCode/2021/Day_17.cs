namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_17
{
    [Example(answer: 45, "target area: x=20..30, y=-10..-5")]
    [Puzzle(answer: 17766, "target area: x=48..70, y=-189..-148", O.ns100)]
    public int part_one(string str)
    {
        var area = Area.Parse(str);
        return area.Y_lo * (area.Y_lo + 1) / 2;
    }

    [Example(answer: 112, "target area: x=20..30, y=-10..-5")]
    [Puzzle(answer: 1733, "target area: x=48..70, y=-189..-148", O.μs100)]
    public int part_two(string str)
    {
        var area = Area.Parse(str);
        var x_lo = (((8 * area.X_lo + 1).Sqrt() - 1) / 2).Ceil();
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

    record Area(int X_lo, int X_hi, int Y_lo, int Y_hi)
    {
        public static Area Parse(string line) => Ctor.New<Area>(line.Int32s());
    }
}
