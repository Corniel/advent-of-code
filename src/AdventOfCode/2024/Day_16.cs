namespace Advent_of_Code_2024;

[Category(Category.Grid, Category.PathFinding)]
public class Day_16
{
    [Example(answer: 7036, Example._1)]
    [Example(answer: 11048, Example._2)]
    [Puzzle(answer: 91464, O.ms)]
    public int part_one(CharGrid map) => Navigate(map).Min;

    [Example(answer: 45, Example._1)]
    [Example(answer: 64, Example._2)]
    [Puzzle(answer: 494, O.ms)]
    public int part_two(CharGrid map)
    {
        var (min, done) = Navigate(map);

        var q = new Queue<State>().EnqueueRange(done.Where(s => s.Value == min).Select(kvp => new State(kvp.Key, kvp.Value)));
        var path = new HashSet<Point>();

        foreach (var (cur, cost) in q.DequeueAll())
        {
            path.Add(cur.Pos);
            Enqueue(cur.Reverse(), cost - 1);
            Enqueue(cur.TurnLeft(), cost - 1000);
            Enqueue(cur.TurnRight(), cost - 1000);
        }
        return path.Count;

        void Enqueue(Cursor cursor, int cost)
        {
            if (done.TryGetValue(cursor, out var prev) && prev == cost) q.Enqueue(new(cursor, cost));
        }
    }

    static (int Min, Dictionary<Cursor, int> Done) Navigate(CharGrid map)
    {
        var q = new PriorityQueue<State, int>();
        q.Enqueue(new(new(map.Position(c => c is 'S'), Vector.E), 0), 0);
        var done = new Dictionary<Cursor, int>() { [q.Peek().Cursor] = 0 };
        var end = map.Position(c => c is 'E');
        var min = int.MaxValue;

        while (q.TryDequeue(out var state, out var c))
        {
            if (state.Cursor.Pos == end) min = Math.Min(min, c);
            else if (c < min) Enqueue(state);
        }
        return (min, done);

        void Enqueue(State state)
        {
            var cur = state.Cursor; var cos = state.Cost;
            var move = new State(cur.Move(), cos + 1);
            if (map[move.Cursor.Pos] is not '#' && Add(move))
            {
                q.Enqueue(move, move.Cost);
            }
            var left = new State(cur.TurnLeft(), cos + 1000);
            var rght = new State(cur.TurnRight(), cos + 1000);
            if (Add(left)) q.Enqueue(left, left.Cost);
            if (Add(rght)) q.Enqueue(rght, rght.Cost);
        }

        bool Add(State state)
        {
            if (!done.TryGetValue(state.Cursor, out var cost) || cost > state.Cost)
            {
                done[state.Cursor] = state.Cost;
                return true;
            }
            else return false;
        }
    }

    record struct State(Cursor Cursor, int Cost);
}
