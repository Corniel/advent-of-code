namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public sealed class Day_19
{
    [Example(answer: 6, Example._1)]
    [Puzzle(answer: 304L, O.μs100)]
    public long part_one(Lines lines) => Count(lines, c => c == 0 ? 0L : 1L);

    [Example(answer: 16, Example._1)]
    [Puzzle(answer: 705756472327497, O.μs100)]
    public long part_two(Lines lines) => Count(lines, c => c);

    /// <remarks>
    /// This solution has be altered afterwards to increase performance (a lot).
    /// 
    /// Together with https://github.com/renzo-baasdam we reduced the chars to
    /// match by creating a search tree, a (solution specific) index creation.
    /// 
    /// The durations dropped from the solution that got me the right answer
    /// (22 ms, in steps back to 340 μs). The downside is that the LoC went
    /// from 13 to 31.
    /// </remarks>
    static long Count(Lines lines, Func<long, long> sum)
    {
        var root = Node.Root(lines[0]);
        return lines[1..].Sum(l => sum(Count(l, root)));
    }

    private static long Count(string line, Node root)
    {
        var cnts = new long[line.Length + 1]; cnts[0] = 1;

        for (var i = 0; i < line.Length; i++)
        {
            var node = root; var n = i;
            
            while (n < line.Length && node.Next[Index[line[n++]]] is { } next)
            {
                if (next.End) cnts[n] += cnts[i];
                node = next;
            }
        }
        return cnts[^1];
    }

    sealed class Node
    {
        public bool End { get; set; }
        public Node[] Next { get; } = new Node[5];
        public static Node Root(string line)
        {
            var root = new Node();

            foreach (var token in line.CommaSeparated())
            {
                var node = root;
                foreach (var c in token)
                {
                    var i = Index[c];
                    node = node.Next[i] ?? (node.Next[i] = new());
                }
                node.End = true;
            }
            return root;
        }
    }

    static readonly int[] Index = Init();

    private static int[] Init()
    {
        var ix = new int['x'];
        ix['g'] = 1; ix['r'] = 2; ix['u'] = 3; ix['w'] = 4;
        return ix;
    }
}
