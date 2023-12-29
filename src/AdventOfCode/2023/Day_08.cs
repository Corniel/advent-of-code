namespace Advent_of_Code_2023;

[Category(Category.SequenceProgression)]
public class Day_08
{
    [Example(answer: 2, Example._1)]
    [Puzzle(answer: 21883, O.μs100)]
    public int part_one(GroupedLines groups)
    {
        var map = Map.New(groups);
        return map.Navigate(map["AAA"], n => n.Name == "ZZZ");
    }

    [Example(answer: 6, Example._2)]
    [Puzzle(answer: 12833235391111, O.ms)]
    public long part_two(GroupedLines groups)
    {
        var map = Map.New(groups);
        return Maths.Lcm(map.Values
            .Where(n => n.Name[^1] == 'A')
            .Select(n => map.Navigate(n, n => n.Name[^1] == 'Z').Long()));
    }

    class Map : Dictionary<string, Node>
    {
        string Instrs;

        public int Navigate(Node cur, Predicate<Node> isTarget)
        {
            var steps = 0;
            foreach (var instr in Repeat(Instrs, int.MaxValue).SelectMany(c => c))
            {
                steps++;
                cur = cur.Next(instr, this);
                if (isTarget(cur)) return steps;
            }
            throw new InfiniteLoop();
        }

        public static Map New(GroupedLines groups)
        {
            var map = new Map() { Instrs = groups[0][0] };
            foreach (var n in groups[1].Select(Node.Parse)) map[n.Name] = n;
            return map;
        }
    }

    record Node(string Name, string L, string R)
    {
        public Node Next(char i, Map map) => i == 'L' ? map[L] : map[R];
        public static Node Parse(string line) => Ctor.New<Node>(line.Separate("=", ",", "(", ")"));
    }
}
