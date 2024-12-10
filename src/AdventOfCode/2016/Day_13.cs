namespace Advent_of_Code_2016;

[Category(Category.Grid, Category.PathFinding)]
public class Day_13
{
    [Puzzle(answer: 82, 1362, O.μs10)]
    public int part_one(int number) => Navigate(number, int.MaxValue, p => p == new Point(31, 39));

    [Puzzle(answer: 138, 1362, O.μs10)]
    public int part_two(int number) => Navigate(number, 50, p => false);

    static int Navigate(int number, int turns, Predicate<Point> exit)
    {
        var turn = 0; var map = new HashSet<Point>();
        var q = new Queue<Point>().EnqueueRange(new Point(1, 1));

        while (turn++ < turns)
        {
            foreach (var n in q.DequeueCurrent().SelectMany(Neighbors).Where(n => n.X >= 0 && n.Y >= 0 && NoWall(n, number) && map.Add(n)))
            {
                if (exit(n)) return turn;
                q.Enqueue(n);
            }
        }
        return map.Count;
    }

    static bool NoWall(Point p, int number) => Bits.UInt32.Count((uint)(Find(p.X, p.Y) + number)).IsEven();

    static int Find(int x, int y) => x * x + 3 * x + 2 * x * y + y + y * y;

    static IEnumerable<Point> Neighbors(Point p) => CompassPoints.Primary.Select(c => p + c.ToVector());
}
