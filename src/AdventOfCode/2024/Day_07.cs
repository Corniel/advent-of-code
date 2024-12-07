namespace Advent_of_Code_2024;

[Category(Category.ExpressionEvaluation)]
public class Day_07
{
    [Example(answer: 3749, Example._1)]
    [Puzzle(answer: 28730327770375, O.ms)]
    public long part_one(Lines lines) => lines.As(l => Do(l, 2)).Sum();

    [Example(answer: 11387, Example._1)]
    [Puzzle(answer: 424977609625985, O.ms10)]
    public long part_two(Lines lines) => lines.As(l => { var n = Do(l, 2); return n == 0 ? Do(l, 3) : n; }).Sum();

    /// <remarks>
    /// The idea to implement the checks in reverse allowing extra early exits
    /// is not mine, I found in on Tweakers: https://gathering.tweakers.net/forum/list_message/80954930#80954930.
    /// The implementation however, is full my own.
    /// 
    /// By doing so, durations dropped from 1.5s to 44ms.
    /// </remarks>
    static long Do(string line, int ops)
    {
        Longs ns = [.. line.Int64s()];
        var pers = ops.Pow(ns.Count - 1);

        for (var per = 0; per < pers; per++)
        {
            if (Test(ns, per, ops) == 0) return ns[0];
        }
        return 0;
    }

    static long Test(Longs ns, int per, int ops)
    {
        var opr = per; var tot = ns[0];
        for (var n = ns.Count - 1; n > 0; n--)
        {
            if ((opr % ops) switch
            {
                0 => tot - ns[n],
                1 => Mul(tot, ns[n]),
                _ => Con(tot, ns[n]),
            } is { } nxt && nxt >= 0) tot = nxt;
            else return -1;

            opr /= ops;
        }
        return tot;
    }

    static long? Mul(long tot, long n)
    {
        var res = tot / n; return res * n == tot ? res : null;
    }

    static long? Con(long tot, long n)
    {
        do
        {
            if ((tot - n) % 10 != 0) return null;
            tot /= 10; n /= 10;
        }
        while (n != 0);

        return tot;
    }
}
