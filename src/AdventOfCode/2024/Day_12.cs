namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_12
{
    [Example(answer: 1930, Example._1)]
    [Puzzle(answer: 1473276, O.ms10)]
    public int part_one(CharPixels chars) => Price(chars, 1, Borders);

    [Example(answer: 1206, Example._1)]
    [Puzzle(answer: 901100, O.ms100)]
    public int part_two(CharPixels chars) => Price(chars, 2, Corners) / 4;
    
    static int Price(CharPixels chars, int f, Func<Region, CharGrid, int> price)
    {
        var map = Map(chars, f);
        return Regions(map).Sum(r => r.Area.Count * price(r, map));
    }

    static int Borders(Region r, CharGrid map) => r.Neighbors.SelectMany(n => map.Neighbors[n]).Count(r.Area.Contains);

    static int Corners(Region r, CharGrid map) => r.Area.Count(a => IsCorner(a, r.Area, map));

    static bool IsCorner(Point p, HashSet<Point> area, CharGrid map) => map.Neighbors[p].Count(area.Contains) switch
    {
        2 => true,
        4 => Neighbors.Grid(map, p, CompassPoints.Secondary).Count(area.Contains) == 3,
        _ => false,
    };

    static CharGrid Map(CharPixels chars, int f)
    {
        var map = new CharGrid(chars.Cols * f + 2, chars.Rows * f + 2);
        map.SetNeighbors(Neighbors.Grid);

        foreach (var (px, ch) in chars)
            for (var x = 1; x <= f; x++)
                for (var y = 1; y <= f; y++)
                    map[px.X * f + x, px.Y * f + y] = ch;
        return map;
    }

    static List<Region> Regions(CharGrid map)
    {
        var q = new Queue<Point>();
        var regions = new List<Region>();

        foreach (var p in map.Positions(p => p != default(char)).Where(p => !regions.Any(r => r.Area.Contains(p))))
        {
            var region = new Region(map[p], [p], []);
            q.Enqueue(p);

            foreach (var n in q.DequeueAll().SelectMany(p => map.Neighbors[p]))
            {
                if (map[n] != region.Ch) region.Neighbors.Add(n);
                else if (region.Area.Add(n)) q.Enqueue(n);
            }
            regions.Add(region);
        }

        return regions;
    }

    record Region(char Ch, HashSet<Point> Area, HashSet<Point> Neighbors);
}
