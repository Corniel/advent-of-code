namespace Advent_of_Code_2024;

[Category(Category.ExpressionEvaluation)]
public class Day_13
{
    [Example(answer: 480, Example._1)]
    [Puzzle(answer: 40069L, O.μs)]
    public long part_one(Longs numbers) => numbers.ChunkBy(6).Sum(n => Solve(n[0], n[1], n[2], n[3], n[4], n[5]));

    [Puzzle(answer: 71493195288102, null, 10000000000000, O.μs)]
    public long part_two(Longs numbers, long add) => numbers.ChunkBy(6).Sum(n => Solve(n[0], n[1], n[2], n[3], n[4] + add, n[5] + add));

    /// <remarks>
    /// x  = A · xa + B · xb
    /// y  = A · yα + B · yb
    /// cost = 3 · A + B
    /// 
    /// step 1:
    /// A = ( y - B · yb ) / yα
    /// B = ( x - A · xa ) / xb
    /// 
    /// step 2:
    /// A · yα = y - B · yb
    /// B · yb = ( ( x - A · xa ) / xb ) · yb
    /// B · yb = ( x · yb - A · xa · yb ) / xb
    /// B · yb = ( x · yb / xb ) - ( A · xa · yb / xb )
    /// 
    /// step 3:
    /// A · yα = y - ( x · yb / xb ) + ( A · xa · yb / xb )
    /// A · yα - ( A · xa · yb / xb ) = y - ( x · yb / xb )
    /// A · ( yα - ( xa · yb / xb ) ) = y - ( x · yb / xb )
    /// A = ( y - ( x · yb / xb ) ) / ( yα - ( xa · yb / xb ) ) 
    /// A = ( y · xb - x · yb ) / ( yα · xb - xa · yb ) 
    /// </remarks>
    static long Solve(long xa, long ya, long xb, long yb, long x, long y)
    {
        var A = (y * xb - x * yb) / (ya * xb - xa * yb);
        var B = (x - A * xa) / xb;

        return x == B * xb + A * xa 
            && y == B * yb + A * ya
            ? A * 3 + B: 0;
    }
}
