namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_15
{
    [Example(answer: 2028, Example._1)]
    [Puzzle(answer: 1398947, O.μs100)]
    public int part_one(GroupedLines groups) => Push(groups, One);

    [Example(answer: 618, Example._2)]
    [Example(answer: 9021, Example._3)]
    [Puzzle(answer: 1397393, O.ms)]
    public int part_two(GroupedLines groups) => Push(groups, Two);

    private static int Push(GroupedLines groups, Func<CharPixels, CharGrid> init)
    {
        var map = init(groups[0].CharPixels());
        var pos = map.Position(p => p == '@');
        map[pos] = '.';

        foreach (var dir in groups[1].SelectMany(s => s.Select(Parse)))
        {
            pos = (pos + dir) switch
            {
                var n when map[n] is '#' => pos,
                var n when map[n] is '.' => n,
                var n when map[n] is 'O' => Single(n, dir, map),
                var n => Double(n, dir, map),
            };
        }
        return map.Positions(p => p is 'O' or '[').Sum(p => p.X + p.Y * 100);
    }

    static Point Single(Point pos, Vector dir, CharGrid map)
    {
        if (pos.Repeat(dir).TakeWhile(p => map[p] != '#').FirstOrNone(p => map[p] == '.') is { } p)
        {
            do { map[p] = map[p - dir]; p -= dir; }
            while (p != pos);
            map[pos] = '.';
            return pos;
        }
        // We can not move, return position before moving.
        else return pos - dir;
    }

    static Point Double(Point pos, Vector dir, CharGrid map)
    {
        if (dir == Vector.E || dir == Vector.W) return Single(pos, dir, map);

        var move = new Stack<Point>();
        var done = new HashSet<Point>();
        var q = new Queue<Point>().EnqueueRange(pos);

        foreach (var (n, c) in q.DequeueAll().Select(p => (p, map[p])))
        {
            // We can not move, return position before moving.
            if (c == '#') return pos - dir;

            move.Push(n);

            if (c is '[')
            {
                if (done.Add(n + dir)) q.Enqueue(n + dir);
                if (done.Add(n + dir + Vector.E)) q.Enqueue(n + dir + Vector.E);
            }
            else if (c is ']')
            {
                if (done.Add(n + dir)) q.Enqueue(n + dir);
                if (done.Add(n + dir + Vector.W)) q.Enqueue(n + dir + Vector.W);
            }
        }
        foreach (var p in move) { map[p] = map[p - dir]; map[p - dir] = '.'; }

        return pos;
    }

    static CharGrid One(CharPixels pixels) => pixels.Grid();

    static CharGrid Two(CharPixels pixels)
    {
        var map = new CharGrid(pixels.Cols * 2, pixels.Rows);
        foreach (var (p, c) in pixels)
        {
            if (c is 'O')
            {
                map[p.X * 2 + 0, p.Y] = '[';
                map[p.X * 2 + 1, p.Y] = ']';
            }
            else
            {
                map[p.X * 2 + 0, p.Y] = c;
                map[p.X * 2 + 1, p.Y] = c;
            }
        }
        map[map.Position(p => p == '@') + Vector.E] = '.';
        return map;
    }

    static Vector Parse(char c) => c switch { '^' => Vector.N, '>' => Vector.E, 'v' => Vector.S, _ => Vector.W };
}
