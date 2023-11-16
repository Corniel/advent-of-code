namespace Advent_of_Code_2017;

[Category(Category.Graph)]
public class Day_07
{
    [Example(answer: "tknk", Example._1)]
    [Puzzle(answer: "cyrupz", O.ms)]
    public string part_one(string input) => Root(input).Name;

    [Example(answer: 60, Example._1)]
    [Puzzle(answer: 193, O.ms)]
    public long part_two(string input)
    {
        var sorted = Root(input).Children.OrderByDescending(c => c.Weight).ToArray();
        var current = sorted[0];
        var delta = current.Weight - sorted[1].Weight;
        
        while (current.Children.NotEmpty() && current.Children.Exists(c => c.Weight != current.Children[0].Weight))
        {
            sorted = [.. current.Children.OrderByDescending(c => c.Weight)];
            delta = sorted[0].Weight - sorted[1].Weight;
            current = sorted[0];
        }
        return current.Own - delta;
    }

    static Node Root(string input)
    {
        var nodes = new Dictionary<string, Node>();
        foreach (var line in input.Lines())
        {
            var node = new Node(line.SpaceSeparated()[0], line.Int32());
            nodes[node.Name] = node;
        }
        foreach (var line in input.Lines())
        {
            var items = line.SpaceSeparated().Where(n => n[0] >= 'a' && n[0] <= 'z').Select(name => nodes[name.Trim(',')]).ToArray();
            items[0].Children.AddRange(items[1..]);
        }
        var children = new HashSet<string>(nodes.Values.SelectMany(n => n.Children).Select(n => n.Name));
        return nodes.Values.First(n => !children.Contains(n.Name));
    }

    record Node(string Name, int Own)
    {
        public readonly List<Node> Children = [];
        public int Weight => Own + Children.Sum(c => c.Weight);
    }
}
