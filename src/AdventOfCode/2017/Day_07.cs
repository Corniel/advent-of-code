namespace Advent_of_Code_2017;

[Category(Category.ms, Category.Graph)]
public class Day_07
{
    private const string Example = @"
pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";

    [Example(answer: "tknk", Example)]
    [Puzzle(answer: "cyrupz")]
    public string part_one(string input) => Root(input).Name;

    [Example(answer: 60, Example)]
    [Puzzle(answer: 193)]
    public long part_two(string input)
    {
        var sorted = Root(input).Children.OrderByDescending(c => c.Weight).ToArray();
        var current = sorted[0];
        var delta = current.Weight - sorted[1].Weight;
        
        while (current.Children.Any() && current.Children.Any(c => c.Weight != current.Children[0].Weight))
        {
            sorted = current.Children.OrderByDescending(c => c.Weight).ToArray();
            delta = sorted[0].Weight - sorted[1].Weight;
            current = sorted[0];
        }
        return current.Own - delta;
    }

    private static Node Root(string input)
    {
        var nodes = new Dictionary<string, Node>();
        foreach (var line in input.Lines())
        {
            var node = new Node(line.SpaceSeparated().First(), line.Int32());
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

    [DebuggerDisplay("{Name}: {Weight} ({Children.Count})")]
    record Node(string Name, int Own)
    {
        public readonly List<Node> Children = new();
        public int Weight => Own + Children.Sum(c => c.Weight);
    }
}
