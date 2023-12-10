namespace Advent_of_Code_2019;

[Category(Category.VectorAlgebra)]
public class Day_03
{
    [Example(answer: 159, @"R75,D30,R83,U83,L12,D49,R71,U7,L72;U62,R66,U55,R34,D71,R55,D58,R83")]
    [Example(answer: 135, @"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51;U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
    [Puzzle(answer: 1195, O.ms)]
    public int part_one(Lines lines)
    {
        var wires0 = lines[0].CommaSeparated(Move.Parse).ToArray();
        var wires1 = lines[1].CommaSeparated(Move.Parse).ToArray();
        var passed = new HashSet<Point>();
        var wire0 = Point.O;

        foreach (var move in wires0.SelectMany(m => Repeat(m.Direction, m.Length)))
        {
            wire0 += move;
            passed.Add(wire0);
        }

        var distance = int.MaxValue;
        var wire1 = Point.O;

        foreach (var move in wires1.SelectMany(m => Repeat(m.Direction, m.Length)))
        {
            wire1 += move;
            if (passed.Contains(wire1))
            {
                distance = Math.Min(Point.O.ManhattanDistance(wire1), distance);
            }
        }
        return distance;
    }

    [Example(answer: 610, @"R75,D30,R83,U83,L12,D49,R71,U7,L72;U62,R66,U55,R34,D71,R55,D58,R83")]
    [Example(answer: 410, @"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51;U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
    [Puzzle(answer: 91518, O.ms)]
    public int part_two(Lines lines)
    {
        var wires0 = lines[0].CommaSeparated(Move.Parse).ToArray();
        var wires1 = lines[1].CommaSeparated(Move.Parse).ToArray();
        var steps = new Dictionary<Point, int>();
        var wire0 = Point.O;
        var steps0 = 1;

        foreach (var move in wires0.SelectMany(m => Repeat(m.Direction, m.Length)))
        {
            wire0 += move;
            steps[wire0] = steps0++;
        }

        var distance = int.MaxValue;
        var wire1 = Point.O;
        var steps1 = 1;

        foreach (var move in wires1.SelectMany(m => Repeat(m.Direction, m.Length)))
        {
            wire1 += move;

            if (steps.TryGetValue(wire1, out int other))
            {
                distance = Math.Min(other + steps1, distance);
            }
            steps1++;
        }
        return distance;
    }

    record struct Move(Vector Direction, int Length)
    {
        public static Move Parse(string str) => new(str[0].Vector(), str.Int32());
    }
}
