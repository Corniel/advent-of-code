namespace Advent_of_Code_2022;

[Category(Category.Grid)]
public class Day_23
{
    [Example(answer: 110, Example._1)]
    [Puzzle(answer: 3762, O.ms)]
    public int part_one(CharPixels chars) => Run(chars, 10);

    [Example(answer: 20, Example._1)]
    [Puzzle(answer: 997, O.ms100)]
    public int part_two(CharPixels chars) => Run(chars, int.MaxValue);

    static int Run(CharPixels chars, int rounds)
    {
        var elves = chars.Where(p => p.Value == '#').Select(p => p.Key).ToHashSet();
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
                    if (Scans[dir].TrueForAll(d => !elves.Contains(elf + d)))
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

    static readonly Vector[] Directions = [Vector.E, Vector.N, Vector.S, Vector.W, Vector.E, Vector.N, Vector.S, Vector.W];
    static readonly Vector[] Neighbors = [..CompassPoints.All.Select(p => p.ToVector())];
    static readonly Dictionary<Vector, Vector[]> Scans = new()
    {
        [Vector.N] = [Vector.N, Vector.NE, Vector.NW],
        [Vector.E] = [Vector.E, Vector.NE, Vector.SE],
        [Vector.S] = [Vector.S, Vector.SE, Vector.SW],
        [Vector.W] = [Vector.W, Vector.NW, Vector.SW],
    };
}
