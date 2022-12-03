namespace Advent_of_Code_2016;

[Category(Category._2D)]
public class Day_03
{
    [Puzzle(answer: 1050)]
    public int part_one(string input) => input.Lines(Triangle.Parse).Sum();

    [Puzzle(answer: 1921)]
    public int part_two(string input)
    {
        var numbers = input.Int32s().ToArray();
        return 0 
            + Count(numbers.Skip(0).WithStep(3).ToArray())
            + Count(numbers.Skip(1).WithStep(3).ToArray())
            + Count(numbers.Skip(2).WithStep(3).ToArray());

        static int Count(int[] numbers) => numbers.ChunkBy(3).Sum(Triangle.Is);
    }

    static class Triangle
    {
        public static int Is(IEnumerable<int> numbers)
        {
            var triangle = numbers.OrderBy(n => n).ToArray();
            return triangle[2] < (triangle[1] + triangle[0]) ? 1 : 0;
        }
        public static int Parse(string line) => Is(line.Int32s());
    }
}
