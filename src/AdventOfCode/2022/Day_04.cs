namespace Advent_of_Code_2022;

[Category(Category.Computation)]
public class Day_04
{
    [Example(answer: 2, "2-4,6-8;2-3,4-5;5-7,7-9;2-8,3-7;6-6,4-6;2-6,4-8")]
    [Puzzle(answer: 433)]
    public int part_one(string input) => input.Lines(Pair.Parse).Count(ps => ps[0].F == ps[1].F || ps[0].S >= ps[1].S);

    [Example(answer: 4, "2-4,6-8;2-3,4-5;5-7,7-9;2-8,3-7;6-6,4-6;2-6,4-8")]
    [Puzzle(answer: 852)]
    public long part_two(string input) => input.Lines(Pair.Parse).Count(ps => ps[0].S >= ps[1].F);

    record Pair(int F, int S)
    {
        public static Pair[] Parse(string line)
        {
            var ns = line.Separate(',', '-').Int32s().ToArray();
            var p1 = new Pair(ns[0], ns[1]);
            var p2 = new Pair(ns[2], ns[3]);
            return p1.F <= p2.F ? new[] { p1, p2 } : new[] { p2, p1 };
        }
    }
}
