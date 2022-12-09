namespace Advent_of_Code_2022;

[Category(Category.ms, Category.Grid, Category.VectorAlgebra)]
public class Day_09
{
    [Example(answer: 13, "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2")]
    [Puzzle(answer: 6018)]
    public int part_one(string input) => Simulate(input, 2);

    [Example(answer: 1, "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2")]
    [Example(answer: 36, "R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20")]
    [Puzzle(answer: 2619)]
    public long part_two(string input) => Simulate(input, 10);
    
    private static int Simulate(string input, int size)
    {
        var rope = Repeat(Point.O, size).ToArray();
        var visited = new HashSet<Point>();

        foreach (var move in input.Lines().SelectMany(l => Repeat(Move(l[0]), l[2..].Int32())))
        {
            rope[0] += move;

            for (var i = 0; i < size - 1; i++)
            {
                rope[i + 1] = Tail(rope[i], rope[i + 1]);
            }
            visited.Add(rope[^1]);
        }
        return visited.Count;
    }

    static Vector Move(char c) => c switch { 'U' => Vector.N, 'D' => Vector.S, 'L' => Vector.W, _ /* R */ => Vector.E };

    static Point Tail(Point head, Point tail)
    {
        if (head - tail is { Length2: > 2 } delta)
        {
            var moved = head;
            if (delta.X.Abs() > 1) moved += Vector.W * delta.X.Sign();
            if (delta.Y.Abs() > 1) moved += Vector.N * delta.Y.Sign();
            return moved;
        }
        else return tail;
    }
}
