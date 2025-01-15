namespace Advent_of_Code_2016;

[Category(Category.PathFinding, Category.Cryptography)]
public class Day_17
{
    [Example(answer: "DDRRRD", "ihgpwlah")]
    [Example(answer: "DDUDRLRRUDRD", "kglvqrro")]
    [Puzzle(answer: "RLDRUDRDDR", "yjjvjgan", O.μs)]
    public string part_one(string str) => Navigate(str, s => s.Path.Length - s.Distance);

    [Example(answer: 370, "ihgpwlah")]
    [Example(answer: 492, "kglvqrro")]
    [Example(answer: 830, "ulqzkmiv")]
    [Puzzle(answer: 498, "yjjvjgan", O.ms10)]
    public int part_two(string str) => Navigate(str, s => s.Distance * 1000 - s.Path.Length).Length;

    static string Navigate(string str, Func<State, int> prio)
    {
        var map = new CharGrid(4, 4);
        var q = new PriorityQueue<State, int>([(new State(Point.O, str), int.MaxValue)]);

        while (q.TryDequeue(out var curr, out _))
        {
            if (curr.Point == (3, 3)) return curr.Path[str.Length..];
            foreach (var next in States(curr).Where(s => map.OnGrid(s.Point))) q.Enqueue(next, prio(next));
        }
        throw new NoAnswer();
    }

    static IEnumerable<State> States(State state)
    {
        var hash = Hashing.MD5.ComputeHash(Encoding.ASCII.GetBytes(state.Path));
        if ((hash[0] >> 4) > 10) yield return state.Next(Vector.N, 'U');
        if ((hash[0] & 15) > 10) yield return state.Next(Vector.S, 'D');
        if ((hash[1] >> 4) > 10) yield return state.Next(Vector.W, 'L');
        if ((hash[1] & 15) > 10) yield return state.Next(Vector.E, 'R');
    }

    readonly record struct State(Point Point, string Path)
    {
        public State Next(Vector dir, char c) => new(Point + dir, Path + c);

        public int Distance => Point.O.ManhattanDistance(Point);
    }
}
