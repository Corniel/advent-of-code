namespace Advent_of_Code_2024;

[Category(Category.Graph)]
public class Day_23
{
    [Example(answer: 7, Example._1)]
    [Puzzle(answer: 1344, O.ms10)]
    public int part_one(Lines lines)
    {
        var (graph, edges) = Graph(lines);

        var lan = new HashSet<string>();

        foreach (var e in edges)
        {
            foreach (var (v, n) in graph)
            {
                if (n.Contains(e.V0) && n.Contains(e.V1))
                {
                    string[] clique = [v, e.V0, e.V1];
                    if (clique.Any(T))
                    {
                        lan.Add(string.Concat(clique.Order()));
                    }
                }
            }
        }
        return lan.Count;

        static bool T(string v) => v[0] is 't';
    }

    [Puzzle(answer: "ab,al,cq,cr,da,db,dr,fw,ly,mn,od,py,uh", O.ms100)]
    public string part_two(Lines lines)
    {
        var (graph, edges) = Graph(lines);
        var curr = new List<HashSet<string>>();
        var next = new List<HashSet<string>>();

        foreach (var e in edges) curr.Add(new HashSet<string>([e.V0, e.V1]));

        while (curr.Count != 0)
        {
            next.Clear();

            foreach (var clique in curr)
                foreach (var (v, _) in graph)
                    if (clique.All(c => graph[c].Contains(v)) && clique.Add(v))
                        next.Add(clique);

            (next, curr) = (curr, next);
        }

        return string.Join(',', next.First().Order());
    }

    static (Dictionary<string, HashSet<string>> graph, Edge[] edges) Graph(Lines lines)
    {
        var edges = lines.ToArray(l => new Edge(l[..2], l[3..]));
        var graph = new Dictionary<string, HashSet<string>>();

        foreach (var edge in edges)
        {
            graph.TryAdd(edge.V0, []);
            graph.TryAdd(edge.V1, []);
            graph[edge.V0].Add(edge.V1);
            graph[edge.V1].Add(edge.V0);
        }
        return (graph, edges);
    }

    readonly record struct Edge(string V0, string V1);
}
