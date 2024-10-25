namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_10
{
    [Example(answer: 4, Example._1)]
    [Example(answer: 8, Example._2)]
    [Puzzle(answer: 6768, O.ms100)]
    public int part_one(CharGrid map) => Pipe(Blow(map), map.First(p => p.Value == 'S').Key).Count / 6;

    [Example(answer: 04, Example._3)]
    [Example(answer: 08, Example._4)]
    [Example(answer: 10, Example._5)]
    [Puzzle(answer: 351, O.ms100)]
    public int part_two(CharGrid map)
    {
        var topo = Blow(map);
        var pipe = Pipe(topo, map.First(p => p.Value == 'S').Key);
        var outside = Outside(topo, pipe);
        return map.Positions().Select(Blow).Count(tile => !pipe.Contains(tile) && !outside.Contains(tile));
    }


    /// <summary>Blows up the grid by a factor 3.</summary>
    ///      .#.      ...        .#.       .#.       .#.       .#.       ...
    /// S => ### - => ###   | => .#.  L => .##  J => ##.  F => .##  7 => ##.
    ///      .#.      ...        .#.       ...       ...       ...       .#.
    static Grid<bool> Blow(CharGrid map)
    {
        var topo = new Grid<bool>(map.Cols * 3, map.Rows * 3);
        topo.SetNeighbors(Neighbors.Grid);

        foreach (var pixel in map)
        {
            var p = Blow(pixel.Key);

            switch (pixel.Value)
            {
                case '-': { topo[p] = true; topo[p + Vector.W] = true; topo[p + Vector.E] = true; break; }
                case '|': { topo[p] = true; topo[p + Vector.N] = true; topo[p + Vector.S] = true; break; }
                case 'L': { topo[p] = true; topo[p + Vector.N] = true; topo[p + Vector.E] = true; break; }
                case 'J': { topo[p] = true; topo[p + Vector.N] = true; topo[p + Vector.W] = true; break; }
                case 'F': { topo[p] = true; topo[p + Vector.S] = true; topo[p + Vector.E] = true; break; }
                case '7': { topo[p] = true; topo[p + Vector.S] = true; topo[p + Vector.W] = true; break; }
                case 'S': { topo[p] = true; topo[p + Vector.W] = true; topo[p + Vector.E] = true; topo[p + Vector.N] = true; topo[p + Vector.S] = true; break; }
            }
        }
        return topo;
    }

    static Point Blow(Point p) => Point.O + (p.Vector() * 3) + Vector.SE;

    static HashSet<Point> Pipe(Grid<bool> topo, Point start)
    {
        var pipe = new HashSet<Point>() { Blow(start) };
        var queue = new Queue<Point>().EnqueueRange(pipe);

        foreach (var tile in queue.DequeueAll())
        {
            queue.EnqueueRange(topo.Neighbors[tile].Where(n => topo[n] && pipe.Add(n)));
        }
        return pipe;
    }

    static HashSet<Point> Outside(Grid<bool> topo, HashSet<Point> pipe)
    {
        var outside = new HashSet<Point>() { Point.O};
        var queue = new Queue<Point>().EnqueueRange(outside);

        foreach (var tile in queue.DequeueAll())
        {
            queue.EnqueueRange(topo.Neighbors[tile].Where(n => !pipe.Contains(n) && outside.Add(n)));
        }
        return outside;
    }
}
