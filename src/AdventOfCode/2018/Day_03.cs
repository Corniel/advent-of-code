namespace Advent_of_Code_2018;

[Category(Category.Simulation, Category.VectorAlgebra)]
public class Day_03
{
    [Example(answer: 4, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2")]
    [Puzzle(answer: 107043, O.ms10)]
    public int part_one(Inputs<Claim> claims) => Counters(claims).Count(c => c.Count > 1);

    [Example(answer: 3, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2")]
    [Puzzle(answer: 346, O.ms10)]
    public int part_two(Inputs<Claim> claims)
    {
        var counters = Counters(claims);
        return claims.First(c => c.Squares().All(sq => counters[sq] == 1)).Id;
    }

    static ItemCounter<Point> Counters(Inputs<Claim> claims) => ItemCounter.New(claims.SelectMany(c => c.Squares()));

    public record Claim(int Id, Point Start, Vector Size)
    {
        public static Claim Parse(string str)
        {
            int[] ints = [..str.Int32s()];
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
