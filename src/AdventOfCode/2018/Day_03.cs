namespace Advent_of_Code_2018;

[Category(Category.Simulation, Category.VectorAlgebra)]
public class Day_03
{
    [Example(answer: 4, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2")]
    [Puzzle(answer: 107043, year: 2018, day: 03)]
    public int part_one(string input)
        => Counters(input.Lines(Claim.Parse)).Count(c => c.Count > 1);

    [Example(answer: 3, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2")]
    [Puzzle(answer: 346, year: 2018, day: 03)]
    public int part_two(string input)
    {
        var claims = input.Lines(Claim.Parse).ToArray();
        var counters = Counters(claims);
        return claims.First(c => c.Squares().All(sq => counters[sq] == 1)).Id;
    }

    private static ItemCounter<Point> Counters(IEnumerable<Claim> claims)
    {
        var map = new ItemCounter<Point>();
        foreach (var claim in claims) map.Add(claim.Squares());
        return map;
    }

    sealed record Claim(int Id, Point Start, Vector Size)
    {
        public static Claim Parse(string str)
        {
            var ints = str.Int32s().ToArray();
            return new(ints[0], new(ints[1], ints[2]), new(ints[3], ints[4]));
        }

        public IEnumerable<Point> Squares()
        {
            for (var dx = 0; dx < Size.X; dx++)
            {
                for (var dy = 0; dy < Size.Y; dy++)
                {
                    yield return Start + new Vector(dx, dy);
                }
            }
        }
    }
}
