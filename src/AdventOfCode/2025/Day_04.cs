namespace Advent_of_Code_2025;

/// <summary>
/// On the floor/grid is full of rolls of paper (@).

/// Part one: Count paper with at fewer then 4 adjacent rolls.
/// Part two: Keep removing rolls with fewer then 4 adjacent rolls.
/// </summary>
[Category(Category.Grid)]
public class Day_04
{
    [Example(answer: 13, Example._1)]
    [Puzzle(answer: 1564, O.ms10)]
    public int part_one(CharGrid map)
    {
        map.SetNeighbors(Neighbors.Grid, CompassPoints.All);
        return map.Positions(p => Accessable(p, map)).Count();
    }

    [Example(answer: 43, Example._1)]
    [Puzzle(answer: 9401, O.ms10)]
    public int part_two(string str)
    {
        var removed = 0;

        // Make a copy so that the benchmark data is not modified.
        var map = str.CharPixels().Grid();

        map.SetNeighbors(Neighbors.Grid, CompassPoints.All);

        bool change;
        do
        {
            change = false; 
            foreach (var pos in map.Positions(x => x is '@').Where(p => Accessable(p, map)))
            {
                removed++;
                change = true;
                map[pos] = '.';
            }
        }
        while (change);

        return removed;
    }

    static bool Accessable(Point p, CharGrid map) => map[p] is '@' && map.Neighbors[p].Count(n => map[n] is '@') < 4;
}
