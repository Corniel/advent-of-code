namespace Advent_of_Code_2022;

[Category(Category.Grid)]
public class Day_23
{
    static readonly Vector[] Directions = new[] { Vector.E, Vector.N, Vector.S, Vector.W, Vector.E, Vector.N, Vector.S, Vector.W };
    static readonly Vector[] Neighbors = CompassPoints.All.Select(p => p.ToVector()).ToArray();
    static readonly Dictionary<Vector, Vector[]> Scans = new()
    {
        [Vector.N] = new[] { Vector.N, Vector.NE, Vector.NW },
        [Vector.E] = new[] { Vector.E, Vector.NE, Vector.SE },
        [Vector.S] = new[] { Vector.S, Vector.SE, Vector.SW },
        [Vector.W] = new[] { Vector.W, Vector.NW, Vector.SW },
    };

    [Example(answer: 110, Example._1)]
    [Puzzle(answer: 3762, O.ms)]
    public int part_one(string input) => Run(input, 10);

    [Example(answer: 20, Example._1)]
    [Puzzle(answer: 997, O.ms100)]
    public long part_two(string input) => Run(input, int.MaxValue);
   
    private static int Run(string input, int rounds)
    {
        var elves = input.CharPixels().Where(p => p.Value == '#').Select(p => p.Key).ToHashSet();
        var moves = new List<Move>();
        var count = new ItemCounter<Point>();

        for (var round = 1; round <= rounds; round++)
        {
            moves.Clear();
            count.Clear();
            var exit = true;

            foreach (var elf in elves.Where(e => Neighbors.Select(n => e + n).Any(elves.Contains)))
            {
                foreach (var dir in Directions.Skip(round.Mod(4)).Take(4))
                {
                    if (Scans[dir].All(d => !elves.Contains(elf + d)))
                    {
                        moves.Add(new(elf, elf + dir));
                        count[elf + dir]++;
                        break;
                    }
                }
            }
            foreach (var move in moves.Where(m => count[m.To] == 1))
            {
                elves.Remove(move.From);
                elves.Add(move.To);
                exit = false;
            }
            if (exit) return round;
        }
        var min = Points.Min(elves);
        var max = Points.Max(elves);
        return (1 + max.X - min.X) * (1 + max.Y - min.Y) - elves.Count;
    }
    record struct Move(Point From, Point To);
}
