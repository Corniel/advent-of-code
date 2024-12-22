namespace Advent_of_Code_2024;

[Category(Category.Grid, Category.PathFinding)]
public class Day_20
{
    [Puzzle(answer: 1441, O.ms10)]
    public int part_one(CharGrid map) => Race(map, 2);

    [Puzzle(answer: 1021490, O.ms100)]
    public int part_two(CharGrid map) => Race(map, 20);

    /// <remarks>
    /// Due to the characteristics of the map:
    /// * No point is further away from E than S.
    /// * All point are connected to both E and S.
    /// * The paths in the maze of a width of 1.
    /// 
    /// Therefor, if by jumping to other point on the maze with a certain
    /// Manhattan a point is reached that is closer by (taking the length of
    /// the cheat into account) this cheat is per definition unique, and
    /// existing.
    /// By setting all #'s tho <see cref="short.MaxValue"/>, those points
    /// can be treated as points that are just far away.
    /// </remarks>
    static int Race(CharGrid map, int cheat, int gain = 100)
    {
        map.SetNeighbors(Neighbors.Grid);
        var dists = Distances(map, map.Position(c => c is 'E')).Set(short.MaxValue, map.Positions(c => c is '#'));
        return map.Positions(c => c is not '#').Sum(Shortcuts);

        int Shortcuts(Point p)
        {
            var max = dists[p] - gain;
            return Range(2, cheat - 1)
                .Sum(d => p.OnManhattanDistance(d).Where(map.OnGrid).Count(n => dists[n] + d <= max));
        }
    }

    static Grid<int> Distances(CharGrid map, Point from, int d = 0)
    {
        var dis = new Grid<int>(map.Cols, map.Rows);
        var q = new Queue<Point>().EnqueueRange(from);

        while (q.Count != 0)
        {
            d++;
            foreach (var n in q.DequeueCurrent().SelectMany(p => map.Neighbors[p].Where(n => n != from && map[n] != '#' && dis[n] == 0)))
            {
                dis[n] = d;
                q.Enqueue(n);
            }
        }
        return dis;
    }
}
