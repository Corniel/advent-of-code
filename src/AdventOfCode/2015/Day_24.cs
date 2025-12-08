namespace Advent_of_Code_2015;

/// <summary>
/// Packages have to divided into groups of equal weight.
///
/// Part one: Split in 3 groups with minium quantum entanglement.
/// Part two: Split in 4 groups with minium quantum entanglement.
/// </summary>
[Category(Category.Computation)]
public class Day_24
{
    [Example(answer: 99, "1 2 3 4 5 7 8 9 10 11")]
    [Puzzle(answer: 11846773891, O.ms)]
    public long part_one(Ints numbers) => Solve(numbers, 3);

    [Example(answer: 44, "1 2 3 4 5 7 8 9 10 11")]
    [Puzzle(answer: 80393059L, O.μs100)]
    public long part_two(Ints numbers) => Solve(numbers, 4);

    static long Solve(Ints numbers, int size)
    {
        // Pre-sort descending to reach solutions faster.
        int[] ns = [.. numbers.OrderDescending()];
        var len = ns.Length; var qe = 0L;
        Walk(0, 0, 1, numbers.Sum() / size);
        return qe;

        void Walk(int lo, int hi, long prod, long remain)
        {
            if (lo > len || remain < 0) return;
            if (remain > 0)
            {
                for (var i = hi; i < ns.Length; i++)
                {
                    var val = ns[i];
                    Walk(lo + 1, i + 1, prod * val, remain - val);
                }
            }
            else
            {
                // Shorter is always better.
                if (lo < len) (len, qe) = (lo, prod);
                // Take the minimum product.
                else qe = Math.Min(qe, prod);
            }
        }
    }
}
