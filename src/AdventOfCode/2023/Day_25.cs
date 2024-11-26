namespace Advent_of_Code_2023;

[Category(Category.Graph)]
public class Day_25
{
    [Example(answer: 54, Example._1)]
    [Puzzle(answer: 556467, O.ms100)]
    public int part_one(Lines lines)
    {
        var nodes = Node.Parse(lines);
        var main = new HashSet<int>(nodes.Select(n => n.Id));
        var edge = new HashSet<int>();

        while (main.Intersect(edge).Count() != 3)
        {
            var other = main.OrderByDescending(s => nodes[s].Connections.Intersect(edge).Count()).First();

            main.Remove(other);
            edge.AddRange(nodes[other].Connections);
        }
        return main.Count * (nodes.Length - main.Count);
    }

    [Puzzle(answer: "Power required is now 49 stars.", "Power required is now 49 stars.")]
    public string part_two(string str) => str;

    record Node(int Id, HashSet<int> Connections)
    {
        public static Node[] Parse(Lines lines)
        {
            var id = 0;
            var nodes = new Dictionary<string, Node>();

            foreach (var parts in lines.As(l => l.Separate(':', ' ')))
            {
                var node = nodes.GetValueOrDefault(parts[0]) ?? new Node(id++, []);
                nodes[parts[0]] = node;

                foreach (var c in parts[1..])
                {
                    var n = nodes.GetValueOrDefault(c) ?? new Node(id++, []);
                    node.Connections.Add(n.Id);
                    n.Connections.Add(node.Id);
                    nodes[c] = n;
                }
            }
            return [.. nodes.Values];
        }
    }
}
