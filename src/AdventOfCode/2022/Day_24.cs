namespace Advent_of_Code_2022;

[Category(Category.Grid)]
public class Day_24
{
    [Example(answer: 18, Example._1)]
    [Example(answer: 255, Example._2)]
    [Puzzle(answer: 295, O.ms10)]
    public int part_one(CharPixels chars)
    {
        var state = State.Parse(chars);
        return state.Nav(state.P1, state.P2).Time;
    }

    [Example(answer: 54, Example._1)]
    [Example(answer: 809, Example._2)]
    [Puzzle(answer: 851, O.ms100)]
    public int part_two(CharPixels chars)
    {
        var state = State.Parse(chars);
        return state.Nav(state.P1, state.P2).Nav(state.P2, state.P1).Nav(state.P1, state.P2).Time;
    }

    record State(Blizz[] Blizzards, Grid<bool> Occupied, Point P1, Point P2)
    {
        public int Time { get; private set; }

        public State Nav(Point from, Point to)
        {
            var nexts = new Grid<bool>(Occupied.Cols, Occupied.Rows);
            var queue = new Queue<Point>([from]);

            while (queue.NotEmpty())
            {
                Next();
                nexts.Clear();

                foreach (var curr in queue.DequeueCurrent())
                {
                    if (!Occupied[curr] && !nexts[curr])
                    {
                        queue.Enqueue(curr);
                        nexts[curr] = true;
                    }
                    foreach (var next in Neighbors.Grid(Occupied, curr, CompassPoints.Primary))
                    {
                        if (next == to) return this;
                        else if (!Occupied.OnEdge(next) && !Occupied[next] && !nexts[next])
                        {
                            nexts[next] = true;
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            throw new NoAnswer();
        }

        void Next()
        {
            Time++;
            Occupied.Clear();
            foreach (var blizz in Blizzards)
            {
                Occupied[blizz.Move(Occupied)] = true;
            }
        }

        public static State Parse(CharPixels pxs) => new(
            Blizzards: Blizz.Parse(pxs),
            Occupied: new Grid<bool>(pxs.Cols, pxs.Rows),
            P1: pxs.First(p => p.Value == '.').Key,
            P2: pxs.Last(p => p.Value == '.').Key);
    }

    class Blizz(Point pos, Vector dir)
    {
        public Point Pos { get; private set; } = pos;
        public Vector Dir { get; } = dir;

        public Point Move(Grid<bool> map)
        {
            Pos += Dir;
            if (map.OnEdge(Pos))
            {
                Pos += Dir * 2;
                Pos = new(Pos.X.Mod(map.Cols), Pos.Y.Mod(map.Rows));
            }
            return Pos;
        }

        public static Blizz[] Parse(CharPixels pxs) => [..pxs.Select(px => px.Value switch
        {
            '^' => new Blizz(px.Key, Vector.N),
            'v' => new Blizz(px.Key, Vector.S),
            '>' => new Blizz(px.Key, Vector.E),
            '<' => new Blizz(px.Key, Vector.W),
            _ => null,
        }).OfType<Blizz>()];
    }
}
