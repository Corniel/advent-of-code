namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_02
{
    [Example(answer: "1985", "ULL;RRDDD;LURDL;UUUUD")]
    [Puzzle(answer: "24862", O.μs100)]
    public string part_one(Lines input) => Solve(input, Point.O, One);

    [Example(answer: "5DB3", "ULL;RRDDD;LURDL;UUUUD")]
    [Puzzle(answer: "46C91", O.μs10)]
    public string part_two(Lines input) => Solve(input, new(-2, 0), Two);

    static string Solve(Lines input, Point point, Dictionary<Point, char> codes)
    {
        var code = new StringBuilder();
        foreach (var moves in input.As(line => line.Select(Parse)))
        {
            foreach (var move in moves)
            {
                var test = point + move;
                if (codes.ContainsKey(test)) { point = test; }
            }
            code.Append(codes[point]);
        }
        return code.ToString();
    }

    static Vector Parse(char ch) => ch switch
    {
       'U' => Vector.N,
       'D' => Vector.S,
       'L' => Vector.W,
       'R' => Vector.E,
        _ => Vector.O
    };

    static readonly Dictionary<Point, char> One = new()
    {
        [new(-1, -1)] = '1',
        [new(+0, -1)] = '2',
        [new(+1, -1)] = '3',
        [new(-1, +0)] = '4',
        [new(+0, +0)] = '5',
        [new(+1, +0)] = '6',
        [new(-1, +1)] = '7',
        [new(+0, +1)] = '8',
        [new(+1, +1)] = '9',
    };

    static readonly Dictionary<Point, char> Two = new()
    {
        [new(+0, -2)] = '1',
        [new(-1, -1)] = '2',
        [new(+0, -1)] = '3',
        [new(+1, -1)] = '4',
        [new(-2, +0)] = '5',
        [new(-1, +0)] = '6',
        [new(+0, +0)] = '7',
        [new(+1, +0)] = '8',
        [new(+2, +0)] = '9',
        [new(-1, +1)] = 'A',
        [new(+0, +1)] = 'B',
        [new(+1, +1)] = 'C',
        [new(+0, +2)] = 'D',
    };
}
