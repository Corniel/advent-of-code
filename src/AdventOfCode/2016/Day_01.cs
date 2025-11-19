namespace Advent_of_Code_2016;

[Category(Category.VectorAlgebra)]
public class Day_01
{
    [Example(answer: 5, "R2, L3")]
    [Example(answer: 2, "R2, R2, R2")]
    [Example(answer: 12, "R5, L5, R5, R3")]
    [Puzzle(answer: 226, O.μs)]
    public int part_one(Lines lines)
    {
        var cursor = new Cursor(Point.O, Vector.S);

        foreach (var instr in lines)
        {
            cursor = cursor.Rotate(instr[0]).Move(instr.Int32());
        }
        return cursor.Pos.ManhattanDistance(Point.O);
    }

    [Example(answer: 4, "R8, R4, R4, R8")]
    [Puzzle(answer: 79, O.μs10)]
    public int part_two(Lines lines)
    {
        var cursor = new Cursor(Point.O, Vector.N);
        var done = new HashSet<Point> { cursor };

        foreach (var instr in lines)
        {
            cursor = cursor.Rotate(instr[0]);

            foreach (var _ in Range(0, instr.Int32()))
            {
                cursor = cursor.Move();
                if (!done.Add(cursor)) return cursor.Pos.ManhattanDistance(Point.O);
            }
        }
        throw new NoAnswer();
    }
}
