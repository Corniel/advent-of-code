namespace Advent_of_Code_2016;

[Category(Category._2D)]
public class Day_03
{
    [Puzzle(answer: 1050, O.μs100)]
    public int part_one(Lines lines) => lines.As(Triangle.Parse).Sum();

    [Puzzle(answer: 1921, O.μs100)]
    public int part_two(Ints numbers) => 0
        + Count([..numbers.WithStep(3)])
        + Count([..numbers[1..].WithStep(3)])
        + Count([..numbers[2..].WithStep(3)]);

    static int Count(int[] numbers) => numbers.ChunkBy(3).Sum(n => Triangle.Is(n));

    static class Triangle
    {
        public static int Is(IEnumerable<int> ns)
        {
            int[] triangle = [..ns.Order()];
            return triangle[2] < (triangle[1] + triangle[0]) ? 1 : 0;
        }
        public static int Parse(string line) => Is(line.Int32s());
    }
}
