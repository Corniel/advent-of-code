namespace Advent_of_Code_2022;

[Category(Category.SequenceProgression)]
public class Day_20
{
    [Example(answer: 3, "1,2,-3,3,-2,0,4")]
    [Puzzle(answer: 5498, O.ms10)]
    public long part_one(string input) => Decrypt(input, 1, 1);

    [Example(answer: 1623178306, "1,2,-3,3,-2,0,4")]
    [Puzzle(answer: 3390007892081, O.ms100)]
    public long part_two(string input) => Decrypt(input, 811589153, 10);

    private static long Decrypt(string input, int key, int times)
    {
        var ns = Number.Parse(input, key);
        var zero = ns.First(n => n.Value == 0);

        foreach (var number in Repeat(ns, times).SelectMany(n => n)) number.Move();

        return zero.Move(1000).Value + zero.Move(2000).Value + zero.Move(3000).Value;
    }

    public record Number(long Value)
    {
        public Number Prev { get; set; }
        public Number Next { get; set; }
        public int Shift { get; private set; }

        public Number Move(int times)
        {
            var next = this;
            while (times-- > 0) next = next.Next;
            return next;
        }

        public void Move()
        {
            if (Shift == 0) return;

            var prev = Prev; var next = Next;
            prev.Next = next; next.Prev = prev;

            var self = Move(Shift); prev = self.Next;
            prev.Prev = this; self.Next = this;
            this.Next = prev; this.Prev = self;
        }

        public static Number[] Parse(string input, long key)
        {
            var ns = input.Lines().Int32s().Select(n => new Number(n * key)).ToArray();
            for (var i = 0; i < ns.Length; i++)
            {
                ns[i].Prev = ns[(i - 1).Mod(ns.Length)];
                ns[i].Next = ns[(i + 1).Mod(ns.Length)];
                ns[i].Shift = (int)ns[i].Value.Mod(ns.Length - 1);
            }
            return ns;
        }
    }
}
