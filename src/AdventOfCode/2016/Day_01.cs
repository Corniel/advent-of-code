namespace Advent_of_Code_2016;

[Category(Category.VectorAlgebra)]
public class Day_01
{
    [Example(answer: 5, "R2, L3")]
    [Example(answer: 2, "R2, R2, R2")]
    [Example(answer: 12, "R5, L5, R5, R3")]
    [Puzzle(answer: 226, O.μs10)]
    public int part_one(string input)
    {
        var current = Point.O;
        var movement = Vector.N;
        foreach(var instruction in input.Split(", "))
        {
            var rotation = instruction[0] == 'R' ? DiscreteRotation.Deg090 : DiscreteRotation.Deg270;
            movement = movement.Rotate(rotation).Sign() * int.Parse(instruction[1..]);
            current += movement;
        }
        return current.ManhattanDistance(Point.O);
    }

    [Example(answer: 4, "R8, R4, R4, R8")]
    [Puzzle(answer: 79, O.μs10)]
    public int part_two(string input)
    {
        var current = Point.O;
        var movement = Vector.N;
        var done = new HashSet<Point> { current };
        foreach (var instruction in input.Split(", "))
        {
            var rotation = instruction[0] == 'R' ? DiscreteRotation.Deg090 : DiscreteRotation.Deg270;
            movement = movement.Rotate(rotation);
            var steps = int.Parse(instruction[1..]);
            for (var step = 0; step < steps; step++)
            {
                current += movement;
                if (!done.Add(current)) return current.ManhattanDistance(Point.O);
            }
        }
        throw new NoAnswer();
    }
}
