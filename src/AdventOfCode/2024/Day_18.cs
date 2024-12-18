namespace Advent_of_Code_2024;

[Category(Category.PathFinding, Category.Grid)]
public class Day_18
{
    [Example(answer: 22, null, 7, 12, Example._1)]
    [Puzzle(answer: 404, null, 71, 1024, O.ms)]
    public int part_one(Lines lines, int size, int fallen) => Navigate(Parse(lines, size, fallen).map);

    [Example(answer: "6,1", null, 7, 12, Example._1)]
    [Puzzle(answer: "27,60", null, 71, 1024, O.ms100)]
    public Point part_two(Lines lines, int size, int fallen)
    {
        var (map, points) = Parse(lines, size, fallen);
        return points.Skip(fallen).First(p => NoExit(p, map));
    }

    bool NoExit(Point p, Grid<bool> map) => Navigate(map.Set(true, p)) == -1;

    int Navigate(Grid<bool> map, int dis = 0)
    {
        var end = map.Corner(CompassPoint.SE);
        Q.Clear(); Q.Enqueue(Point.O);
        Done.Clear(); Done.Add(Point.O);

        while (Q.Count != 0)
        {
            dis++;
            foreach (var p in Q.DequeueCurrent().SelectMany(p => map.Neighbors[p].Where(n => !map[n] && Done.Add(n))))
            {
                if (p == end) return dis;
                Q.Enqueue(p);
            }
        }
        return -1;
    }

    static (Grid<bool> map, Point[] points) Parse(Lines lines, int size, int fallen)
    {
        var map = new Grid<bool>(size, size).SetNeighbors(Neighbors.Grid);
        var points = lines.ToArray(Point.Parse);
        return (map.Set(true, points.Take(fallen)) , points);
    }

    readonly Queue<Point> Q = [];
    readonly HashSet<Point> Done = [];
}
