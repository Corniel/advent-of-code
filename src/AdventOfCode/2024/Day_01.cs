namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public class Day_01
{
    [Example(answer: 11, "3 4;4 3;2 5;1 3;3 9;3 3")]
    [Puzzle(answer: 1873376, O.μs10)]
    public int part_one(Ints numbers) => Chunk(numbers, 0).Order().Zip(Chunk(numbers, 1).Order(), (l, r) => (l - r).Abs()).Sum();

    [Example(answer: 31, "3 4;4 3;2 5;1 3;3 9;3 3")]
    [Puzzle(answer: 18997088, O.μs10)]
    public int part_two(Ints numbers)
    {
        var count = ItemCounter.New(Chunk(numbers, 1));
        return Chunk(numbers, 0).Sum(n => n * (int)count[n]);
    }

    static IEnumerable<int> Chunk(Ints numbers, int i) => numbers.ChunkBy(2).Select(n => n[i]);
}
