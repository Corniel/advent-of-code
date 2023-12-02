namespace Advent_of_Code_2015;

[Category(Category.VectorAlgebra, Category.Simulation)]
public class Day_03
{
    [Example(answer: 4, "^>v<")]
    [Puzzle(answer: 2592, O.μs100)]
    public int part_one(string str)
    {
        var done = new HashSet<Point> { Point.O };
        var santa = Point.O;
        foreach(var dir in str.Select(Parse))
        {
            santa += dir;
            done.Add(santa);
        }
        return done.Count;
    }

    [Example(answer: 11, "^v^v^v^v^v")]
    [Puzzle(answer: 2360, O.μs100)]
    public int part_two(string str)
    {
        var done = new HashSet<Point> { Point.O };
        var santa = Point.O;
        var robo = Point.O;
        var first = true;
        foreach (var dir in str.Select(Parse))
        {
            if (first)
            {
                santa += dir;
                done.Add(santa);
            }
            else
            {
                robo += dir;
                done.Add(robo);
            }
            first = !first;
        }
        return done.Count;
    }

    static Vector Parse(char ch)
    => ch switch 
    {
        '^' => Vector.N,
        '>' => Vector.E,
        '<' => Vector.W,
        _ => Vector.S,
    };
}
