namespace Advent_of_Code_2025;

/// <summary>
/// There is a polygon defined with about 500 points.
///
/// Part one: Find the biggest square that can be drawn from two points on the polygon.
/// Part two: Find the biggest square that can be drawn from two points that fits polygon.
/// </summary>
[Category(Category._2D, Category.VectorAlgebra)]
public class Day_09
{
    [Example(answer: 50, "7,1 11,1 11,7 9,7 9,5 2,5 2,3 7,3")]
    [Puzzle(answer: 4760959496, O.μs100)]
    public long part_one(Point2Ds points) => points.RoundRobin().Select(p => Rect.New(p).Size).Max();

    [Example(answer: 24, "7,1 11,1 11,7 9,7 9,5 2,5 2,3 7,3")]
    [Puzzle(answer: 1343576598L, O.ms10)]
    public long part_two(Point2Ds points)
    {
        Rect[] edges = [.. points.SelectWithPrevious().Append(new(points[0], points[^1])).Select(Rect.New)];
        return points.RoundRobin().Select(Rect.New).OrderByDescending(r => r.Size).First(r => edges.None(e => r.Overlap(e))).Size;
    }
   
    readonly record struct Rect(Point Min, Point Max)
    {
        public long Size => 1L * (Max.X - Min.X + 1) * (Max.Y - Min.Y + 1);

        public bool Overlap(Rect o) 
            => Max.X > o.Min.X && Min.X < o.Max.X
            && Min.Y < o.Max.Y && Max.Y > o.Min.Y;

        public static Rect New(CurrentAndPrevious<Point> p) => New(p.Current, p.Previous);
        public static Rect New(Pair<Point> p) => New(p.First, p.Second);
        public static Rect New(Point a, Point b) => new((int.Min(a.X, b.X), int.Min(a.Y, b.Y)), (int.Max(a.X, b.X), int.Max(a.Y, b.Y)));
    }
}
