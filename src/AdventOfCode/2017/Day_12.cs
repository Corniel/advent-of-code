namespace Advent_of_Code_2017;

[Category(Category.Graph)]
public class Day_12
{
    [Example(answer: 6, "0 <-> 2;1 <-> 1;2 <-> 0, 3, 4;3 <-> 2, 4;4 <-> 2, 3, 6;5 <-> 6;6 <-> 4, 5")]
    [Puzzle(answer: 128, O.μs100)]
    public int part_one(Lines lines) => Nodes(lines)[0].SelveAndAncestors().Count;

    [Example(answer: 2, "0 <-> 2;1 <-> 1;2 <-> 0, 3, 4;3 <-> 2, 4;4 <-> 2, 3, 6;5 <-> 6;6 <-> 4, 5")]
    [Puzzle(answer: 209, O.ms)]
    public int part_two(Lines lines)
    {
        var nodes = Nodes(lines).Select(n => n.SelveAndAncestors()).ToArray();
        var groups = 1;

        for (var id = 1; id < nodes.Length; id++)
        {
            groups += Range(0, id).Any(index => nodes[index].Contains(id)) ? 0 : 1;
        }
        return groups;
    }

    static Node[] Nodes(Lines lines)
    {
        var nodes = new Node[lines.Count];
        foreach (var line in lines)
        {
            var node = new Node(line.Int32());
            nodes[node.Id] = node;
        }
        var id = 0;
        foreach (var line in lines)
        {
            nodes[id++].Children.AddRange(line.Int32s().Skip(1).Select(i => nodes[i]));
        }
        return nodes;
    }

    [DebuggerDisplay("{Id}")]
    record Node(int Id)
    {
        public readonly List<Node> Children = [];
        public HashSet<int> SelveAndAncestors() => Visit([]);
        private HashSet<int> Visit(HashSet<int> visted)
        {
            if (visted.Add(Id)) { foreach (var child in Children) child.Visit(visted); }
            return visted;
        }
    }
}
