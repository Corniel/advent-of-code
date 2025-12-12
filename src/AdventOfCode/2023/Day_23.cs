namespace Advent_of_Code_2023;

[Category(Category.Grid, Category.PathFinding)]
public class Day_23
{
    [Example(answer: 94, Example._1)]
    [Puzzle(answer: 2134, O.ms10)]
    public int part_one(CharGrid map) => Navigate(map, Restricted);

    [Example(answer: 154, Example._1)]
    [Puzzle(answer: 6298, O.s)]
    public int part_two(CharGrid map) => Navigate(map, Unrestricted);

    static bool Restricted(Point c, Point n, CharGrid map) => map[n] switch
    {
        '#' => false,
        '^' => n - c == Vector.N,
        '>' => n - c == Vector.E,
        'v' => n - c == Vector.S,
        '<' => n - c == Vector.W,
        _ => true
    };

    static bool Unrestricted(Point curr, Point next, CharGrid map) => map[next] is not '#';

    static int Navigate(CharGrid map, Func<Point, Point, CharGrid, bool> access)
    {
        var graph = Graph(map, access);
        var queue = new Queue<Path>().EnqueueRange(new Path(graph[0], new(), 0));
        var dis = 0;

        while (queue.TryDequeue(out var path))
        {
            foreach (var c in path.Node.Connections.Where(c => !Bits.UInt64.HasFlag(path.Done, c.Id)))
            {
                var next = new Path(c.Target, Bits.UInt64.Flag(path.Done, c.Id), path.Distance + c.Distance);

                if (next.Node == graph[^1]) dis = Math.Max(next.Distance, dis);
                else queue.Enqueue(next);
            }
        }
        return dis;
    }

    static Node[] Graph(CharGrid map, Func<Point, Point, CharGrid, bool> access)
    {
        map.SetNeighbors(AocGrid.Neighbors);
        var graph = map
            .NonHashes()
            .Where(p => map.Neighbors[p].Count != 2)
            .Select((p, i) => new Node(i, p, []))
            .ToArray();

        foreach (var n in graph) n.Connections.AddRange(Connect(n, map, graph, access));

        return graph;
    }

    static IEnumerable<Connection> Connect(Node node, CharGrid map, Node[] graph, Func<Point, Point, CharGrid, bool> access)
    {
        var queue = new Queue<Point>().EnqueueRange(node.Point);
        var done = new HashSet<Point>() { node.Point };
        var distance = 0;

        while (queue.NotEmpty)
        {
            distance++;
            foreach (var n in queue.DequeueCurrent().SelectMany(p => map.Neighbors[p].Where(n => access(p, n, map) && done.Add(n))))
            {
                if (graph.Find(x => x.Point == n) is { } target)
                    yield return new Connection(target, distance);
                else queue.Enqueue(n);
            }
        }
    }

    record Node(int Id, Point Point, List<Connection> Connections);

    record Connection(Node Target, int Distance) { public int Id => Target.Id; }

    record struct Path(Node Node, ulong Done, int Distance);
}
