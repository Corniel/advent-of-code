namespace Advent_of_Code_2023;

[Category(Category.Grid, Category.PathFinding)]
public class Day_17
{
    [Example(answer: 102, Example._1)]
    [Puzzle(answer: 1246, O.ms10)]
    public int part_one(CharGrid map) => Naviate(map, 0, 3);

    [Example(answer: 94, Example._1)]
    [Example(answer: 71, Example._2)]
    [Puzzle(answer: 1389, O.ms100)]
    public int part_two(CharGrid map) => Naviate(map, 4, 10);

    static int Naviate(CharGrid map, int min, int max)
    {
        var tar = map.Corner(CompassPoint.SE);
        var q = new PriorityQueue<State, int>();
        var costs = new Dictionary<State, int>();

        foreach (var s in CompassPoints.Primary.Select(c => new State(new(Point.O, c), map.Size)))
        {
            q.Enqueue(s, 0); costs[s] = 0;
        }

        while (q.TryDequeue(out var pr, out var prio))
        {
            if (pr.Cur.Pos == tar && pr.Straight >= min) return prio;

            var prev = costs[pr];

            foreach (var state in pr.Next(min, max).Where(n => map.OnGrid(n.Cur)))
            {
                var cost = prev + map.Val(state.Cur).Digit();

                if (cost < costs.GetValueOrDefault(state, int.MaxValue))
                {
                    costs[state] = cost;
                    q.Enqueue(state, cost + state.Cur.Pos.ManhattanDistance(tar));
                }
            }
        }
        throw new NoAnswer();
    }

    record struct State(Cursor Cur, int Straight = 1)
    {
        public IEnumerable<State> Next(int min, int max)
        {
            if (Straight < max) yield return new(Cur.Move(), Straight + 1);
            if (Straight >= min)
            {
                yield return new(Cur.TurnRight().Move());
                yield return new(Cur.TurnLeft().Move());
            }
        }
    }
}
