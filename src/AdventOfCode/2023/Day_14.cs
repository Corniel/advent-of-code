namespace Advent_of_Code_2023;

[Category(Category.Grid, Category.CyclusDetection)]
public class Day_14
{
    [Example(answer: 136, Example._1)]
    [Puzzle(answer: 110128, O.μs100)]
    public int part_one(CharGrid map)
    {
        var rocks = map.Positions(t => t == 'O').ToHashSet();
        var buffer = new HashSet<Point>();
        MoveDir(CompassPoint.N, ref rocks, ref buffer, map);
        return rocks.Sum(r => map.Rows - r.Y);
    }

    [Example(answer: 64, Example._1)]
    [Puzzle(answer: 103861, O.ms100)]
    public int part_two(CharGrid map)
    {
        var rocks = map.Positions(t => t == 'O').ToHashSet();
        var buffer = new HashSet<Point>();
        var cycle = Cycle(ref rocks, ref buffer, map);

        foreach (var _ in Range(0, 1_000_000_000.Mod(cycle))) MoveCycle(ref rocks, ref buffer, map);

        return rocks.Sum(r => map.Rows - r.Y);
    }

    static ModuloInt32 Cycle(ref HashSet<Point> rocks, ref HashSet<Point> buffer, CharGrid map)
    {
        var history = new Dictionary<int, int>();
        do { MoveCycle(ref rocks, ref buffer, map); }
        while (history.TryAdd(Hash(rocks), history.Count));

        var start = history[Hash(rocks)];

        return (start + 1).Modulo(history.Count - start);
    }

    static void MoveCycle(ref HashSet<Point> rocks, ref HashSet<Point> buffer, CharGrid map)
    {
        foreach (var dir in Dirs) MoveDir(dir, ref rocks, ref buffer, map);
    }

    static void MoveDir(CompassPoint dir, ref HashSet<Point> rocks, ref HashSet<Point> buffer, CharGrid map)
    {
        buffer.Clear();
        foreach (var rock in Order(rocks, dir)) buffer.Add(Move(new(rock, dir), buffer, map));
        (buffer, rocks) = (rocks, buffer);
    }

    static Cursor Move(Cursor rock, HashSet<Point> occupied, CharGrid map)
    {
        do { rock = rock.Move(); }
        while (!occupied.Contains(rock) && map.OnGrid(rock) && map.Val(rock) != '#');
        return rock.Reverse();
    }

    static IEnumerable<Point> Order(HashSet<Point> rocks, CompassPoint dir) => dir switch
    {
        CompassPoint.N => rocks.OrderBy(r => r.Y),
        CompassPoint.W => rocks.OrderBy(r => r.X),
        CompassPoint.S => rocks.OrderByDescending(r => r.Y),
        _ => rocks.OrderByDescending(r => r.X),
    };

    static int Hash(HashSet<Point> ps)
    {
        var h = 0;
        foreach (var p in ps) h = h * 17 + p.GetHashCode();
        return h;
    }

    static readonly CompassPoint[] Dirs = [CompassPoint.N, CompassPoint.W, CompassPoint.S, CompassPoint.E];
}
