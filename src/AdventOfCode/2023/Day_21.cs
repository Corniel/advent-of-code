namespace Advent_of_Code_2023;

[Category(Category.Graph, Category.PathFinding)]
public class Day_21
{
    [Example(answer: 16, null, 6, Example._1)]
    [Puzzle(answer: 3562, null, 64, O.μs100)]
    public int part_one(CharGrid map, int steps) => Plots(Center(map), map, steps);

    /// <remarks>
    ///                        +-----+
    ///                        |     |
    ///                        |  o  |
    ///                      / |     | \
    ///                  +-----+-----+-----+
    ///                  |     |     |     |
    ///                  |  o  |  e  |  o  |
    ///                / |     |     |     | \
    ///            +-----+-----+-----+-----+-----+
    ///            |     |     |     |     |     |
    ///            |  o  |  e  |  o  |  e  |  o  |
    ///            |     |     |     |     |     |
    ///            +-----+-----+-----+-----+-----+
    ///                \ |     |     |     | /
    ///                  |  o  |  e  |  o  |
    ///                  |     |     |     |
    ///                  +-----+-----+-----+
    ///                      \ |     | /
    ///                        |  o  |
    ///                        |     |
    ///                        +-----+
    ///                        
    [Puzzle(answer: 592723929260582, null, 26_501_365, O.ms10)]
    public long part_two(CharGrid map, int steps)
    {
        var p = map.Rows;
        var ps = steps / p;
        var max = p - 1;
        var mid = p / 2;
        var odd = 1L;
        var even = 0L;

        for (var r = 2; r <= ps; r++)
        {
            if (r.IsOdd()) odd += Ring(r);
            else even += Ring(r);
        }

        var ne = map.Corner(CompassPoint.NE);
        var nw = map.Corner(CompassPoint.NW);
        var se = map.Corner(CompassPoint.SE);
        var sw = map.Corner(CompassPoint.SW);

        return new ItemCounter<State>
        {
            [new(Center(map), p)] = odd,
            [new(Center(map), p - 1)] = even,

            [new((mid, 000), p - 1)] = 1,
            [new((max, mid), p - 1)] = 1,
            [new((mid, max), p - 1)] = 1,
            [new((000, mid), p - 1)] = 1,

            [new(ne, p / 2 - 1)] = ps,
            [new(nw, p / 2 - 1)] = ps,
            [new(se, p / 2 - 1)] = ps,
            [new(sw, p / 2 - 1)] = ps,

            [new(ne, (p - 1) * 3 / 2)] = ps - 1,
            [new(nw, (p - 1) * 3 / 2)] = ps - 1,
            [new(se, (p - 1) * 3 / 2)] = ps - 1,
            [new(sw, (p - 1) * 3 / 2)] = ps - 1,
        }
        .Sum(r => Plots(r, map));
    }

    static Point Center(CharGrid map) => new(map.Cols / 2, map.Rows / 2);

    static long Ring(int r) => 4 * r - 4;

    static long Plots(ItemCount<State> s, CharGrid map) => s.Count * Plots(s.Item.Start, map, s.Item.Steps);

    static int Plots(Point start, CharGrid map, int steps)
    {
        if (map.Neighbors is null) map.SetNeighbors(AocGrid.Neighbors);
        var queue = new Queue<Point>().EnqueueRange(start);
        var done = new HashSet<Point>() { start };
        var step = 0;
        var phase = steps % 2;
        var plots = 1 - phase;

        while (queue.Any() && ++step <= steps)
        {
            foreach (var n in queue.DequeueCurrent().SelectMany(p => map.Neighbors[p].Where(n => done.Add(n))))
            {
                if (step % 2 == phase) plots++;
                queue.Enqueue(n);
            }
        }
        return plots;
    }

    record State(Point Start, int Steps);
}
