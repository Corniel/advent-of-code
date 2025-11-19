namespace Advent_of_Code_2024;

[Category(Category.Cryptography, Category.BitManupilation)]
public class Day_22
{
    [Example(answer: 37327623, "1,10,100,2024")]
    [Puzzle(answer: 19854248602, O.ms10)]
    public long part_one(Longs numbers) => numbers.As(n => Sequances.AdHoc(n, Next).Skip(2000).First()).Sum();

    [Example(answer: 9, "123")]
    [Example(answer: 23, "1,2,3,2024")]
    [Puzzle(answer: 2223L, O.ms100)]
    public long part_two(Longs numbers) => numbers
        .SelectMany(n => Sequances.AdHoc(n, Next)
            .Select(n => n % 10).SelectWithPrevious().Take(2000)
            .Select(n => (val: n.Current, dt: n.Current - n.Previous))
            .SelectWithPrevious(4)
            .Select(h => new Price(h[3].val, new Deltas((int)h[0].dt, (int)h[1].dt, (int)h[2].dt, (int)h[3].dt)))
            .Where(p => p.Deltas.D > 0)
            .DistinctBy(p => p.Deltas))
        .GroupBy(p => p.Deltas, p => p.Val).Max(g => g.Sum());

    readonly record struct Deltas(int A, int B, int C, int D);
    
    readonly record struct Price(long Val, Deltas Deltas);

    [TestCase(123, ExpectedResult = 15887950)]
    public long Next(long n)
    {
        n = (n ^ (n << 6)) % 16777216;
        n = (n ^ (n >> 5)) % 16777216;
        return (n ^ (n << 11)) % 16777216;
    }
}
