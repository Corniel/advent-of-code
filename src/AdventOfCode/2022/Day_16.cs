namespace Advent_of_Code_2022;

[Category(Category.PathFinding)]
public class Day_16
{
    [Example(answer: 1651, Example._1)]
    [Puzzle(answer: 1940, O.ms)]
    public int part_one(string input) => Presure(input, 30, true);

    [Example(answer: 1707, Example._1)]
    [Puzzle(answer: 2469, O.ms100)]
    public int part_two(string input) => Presure(input, 26, false);

    static int Presure(string input, int turns, bool solo)
    {
        var valves = Valve.Parse(input);
        uint closed = (1U << (valves.Length)) - 2;
        var state = new State(closed, 0, 0, 1, 1, 0, 0);
        var queue = new PriorityQueue<State, int>();

        foreach (var c in valves[0].Connections) queue.Enqueue(state.Move(c.Key, c.Value, turns, solo), 0);
        return Search(turns, valves, queue, maxPresure: valves.Sum(v => v.Rate), solo);
    }

    static int Search(int turns, Valve[] valves, PriorityQueue<State, int> queue, int maxPresure, bool solo)
    {
        var best = 0;

        while (queue.Count != 0)
        {
            var state = queue.Dequeue();
            foreach (var next in valves[state.Id1].Connections.Where(c => state.IsClosed(c.Key)).Select(c => state.Move(c.Key, c.Value, turns, solo)))
            {
                if (next.Turn1 < turns && next.Closed != 0 && next.Maximum(turns, maxPresure) >= best)
                {
                    queue.Enqueue(next, -next.Score);
                }
                best = Math.Max(best, next.Score);
            }
        }
        return best;
    }

    record struct State(uint Closed, int Score, int Rate, int Turn1, int Turn2, byte Id1, byte Id2)
    {
        public readonly bool IsClosed(Valve valve) => Bits.UInt32.HasFlag(Closed, valve.Id);

        public readonly int Maximum(int turns, int max) => Score + (max - Rate) * (turns - Turn1 - 2);

        public readonly State Move(Valve valve, int steps, int turns, bool solo)
        {
            var closed = Closed; var id1 = Id1; var rate = Rate; var score = Score;

            if ((Turn1 + steps + 1) is { } turn1 && turn1 <= turns)
            {
                closed = Bits.UInt32.Unflag(Closed, valve.Id);
                score += valve.Rate * (turns - turn1 + 1);
                rate += valve.Rate;
                id1 = valve.Id;
            }
            else  turn1 = turns;

            if (solo) return new(closed, score, rate, turn1, turn1, id1, id1);
            else if (turn1 > Turn2) return new(closed, score, rate, Turn2, turn1, Id2, id1);
            else return new(closed, score, rate, turn1, Turn2, id1, Id2);
        }
    }

    record Valve(string Name, int Rate, Dictionary<Valve, int> Connections)
    {
        public byte Id { get; private set; }

        void Connect()
        {
            var queue = new Queue<Valve>(Connections.Keys);
            var distance = 1;

            while (distance++ is { } && queue.NotEmpty())
            {
                foreach (var next in queue.DequeueCurrent()
                    .SelectMany(c => c.Connections.Where(d => d.Value == 1 && Connections.TryAdd(d.Key, distance)))
                    .Select(d => d.Key))
                {
                    queue.Enqueue(next);
                }
            }
        }

        public static Valve[] Parse(string input)
        {
            var lines = input.Replace(";", "").Replace("to valve ", "to valves ").Lines(Line.Parse).OrderBy(l => l.Name).ToArray();
            var tmp = lines.ToDictionary(l => l.Name, l => new Valve(l.Name, l.Rate, []));

            foreach (var line in lines)
            {
                var valve = tmp[line.Name];
                valve.Connections[valve] = int.MaxValue;
                foreach (var c in line.Connections.Select(n => tmp[n])) valve.Connections[c] = 1;
            }
            foreach (var valve in tmp.Values) valve.Connect();
            foreach (var valve in tmp.Values.Where(v => v.Rate == 0 && v.Name != "AA").ToArray()) tmp.Remove(valve.Name);
            foreach (var valve in tmp.Values)
            {
                foreach (var c in valve.Connections.Keys.Where(c => c.Rate == 0 || c == valve).ToArray()) valve.Connections.Remove(c);
            }
            byte id = 0;
            foreach (var valve in tmp.Values) valve.Id = id++;
            return [.. tmp.Values];
        }
    }
    record Line(string Name, int Rate, IReadOnlyList<string> Connections)
    {
        public static Line Parse(string line) => new(line[6..8], line.Int32(), line.Separate("valves")[1].CommaSeparated());
    }
}
