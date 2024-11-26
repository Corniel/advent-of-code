namespace Advent_of_Code_2022;

[Category(Category.Grid, Category.VectorAlgebra)]
public class Day_09
{
    [Example(answer: 13, "R 4;U 4;L 3;D 1;R 4;D 1;L 5;R 2")]
    [Puzzle(answer: 6018, O.μs100)]
    public int part_one(Lines lines) => Simulate(lines, 2);

    [Example(answer: 1, "R 4;U 4;L 3;D 1;R 4;D 1;L 5;R 2")]
    [Example(answer: 36, "R 5;U 8;L 8;D 3;R 17;D 10;L 25;U 20")]
    [Puzzle(answer: 2619, O.μs100)]
    public int part_two(Lines lines) => Simulate(lines, 10);

    static int Simulate(Lines lines, int size)
    {
        var rope = Repeat(Point.O, size).ToArray();
        var visited = new HashSet<Point>();

        foreach (var move in lines.SelectMany(l => Repeat(l[0].Vector(), l.Int32())))
        {
            rope[0] += move;

            for (var i = 1; i < size; i++)
            {
                rope[i] = Tail(rope[i - 1], rope[i]);
            }
            visited.Add(rope[^1]);
        }
        return visited.Count;
    }

    static Point Tail(Point head, Point tail)
        => head - tail is { Length2: > 2 } delta
        ? head - new Vector(delta.X / 2, delta.Y / 2)
        : tail;
}
