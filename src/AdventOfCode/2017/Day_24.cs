namespace Advent_of_Code_2017;

[Category(Category.PathFinding)]
public class Day_24
{
    [Example(answer: 31, @"0/2;2/2;2/3;3/4;3/5;0/1;10/1;9/10")]
    [Puzzle(answer: 1511, O.ms10)]
    public int part_one(Ints numbers) =>  Walk(numbers, One);

    [Puzzle(answer: 1471, O.ms10)]
    public int part_two(Ints numbers) => Walk(numbers, Two);

    static bool One(State t, State b) => t.Score > b.Score;

    static bool Two(State t, State b) => (t.Length - b.Length) switch 
    {
        > 0 => true,
        0 => One(t, b),
        _ => false, 
    };

    static int Walk(Ints numbers, Func<State, State, bool> improves)
    {
        var flags = new Dictionary<Bridge, int>();
        var graph = Range(0, 51).Select(_ => new HashSet<int>()).ToArray();
        var best = default(State);
        var queue = new Queue<State>().EnqueueRange(best);

        foreach (var ns in numbers.ChunkBy(2)) { flags[Bridge.New(ns[0], ns[1])] = flags.Count; }
        
        foreach (var b in flags.Keys) { graph[b.Lo].Add(b.Hi); graph[b.Hi].Add(b.Lo); }

        foreach (var test in queue.DequeueAll())
        {
            var none = true;

            foreach (var next in graph[test.Node])
            {
                var brigde = Bridge.New(test.Node, next);
                var done = test.Done | 1UL << flags[brigde];

                if (test.Add(next, brigde, done) is { } q) { none = false; queue.Enqueue(q); }
            }
            best = none && improves(test, best) ? test : best;
        }
        return best.Score;
    }

    readonly record struct State(int Node, int Score, int Length, ulong Done)
    {
        public State? Add(int next, Bridge b, ulong done) => done != Done ? new(next, Score + b.Lo + b.Hi, Length + 1, done) : null;
    }

    readonly record struct Bridge(int Lo, int Hi)
    {
        public static Bridge New(int l, int r) => l > r ? new(r, l) : new(l, r); 
    }
}
