namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_23
{
    [Example(answer: 67384529, 389125467)]
    [Puzzle(answer: 94238657, 739862541, O.ns100)]
    public int part_one(int number)
    {
        var cups = Cups.Create(number.Digits());
        while (cups.Turn < 100) { cups.Next(); }
        return Digits.ToInt32(cups.Answer());
    }

    [Example(answer: 149245887792, 389125467)]
    [Puzzle(answer: 3072905352L, 739862541, O.s)]
    public long part_two(int number)
    {
        var cups = Cups.Create(number.Digits(), 1_000_000);
        while (cups.Turn < 10_000_000) { cups.Next(); }
        return (long)cups.Search(1).Next.Value * cups.Search(1).Next.Next.Value;
    }

    class Cups(int size)
    {
        readonly Cup[] search = new Cup[size + 1];
        public readonly int Size = size;
        public int Turn { get; private set; }
        public Cup Curr { get; private set; }

        public Cup Search(int value) => search[value];
        public void Next()
        {
            Turn++;
            var head = Curr.Next;
            var midd = head.Next;
            var tail = midd.Next;
            var dest = Destination(Curr.Value, head.Value, midd.Value, tail.Value);
            Curr.Next = tail.Next;
            tail.Next = dest.Next;
            dest.Next = head;
            Curr = Curr.Next;
        }
        Cup Destination(int val, int p1, int p2, int p3)
        {
            val = val == 1 ? Size : val - 1;
            return (val == p1 || val == p2 || val == p3)
                ? Destination(val, p1, p2, p3)
                : Search(val);
        }
        public static Cups Create(IEnumerable<int> values, int size = 9)
        {
            var cups = new Cups(size);
            var done = 1;
            var first = new Cup(values.First());
            cups.search[first.Value] = first;
            var prev = first;

            foreach (var val in values.Skip(1))
            {
                var curr = new Cup(val);
                cups.search[val] = curr;
                prev.Next = curr;
                prev = curr;
                done++;
            }
            while (++done <= size)
            {
                var curr = new Cup(done);
                cups.search[done] = curr;
                prev.Next = curr;
                prev = curr;
            }
            prev.Next = first;
            cups.Curr = first;
            return cups;
        }
        public IEnumerable<int> Answer()
        {
            var next = Search(1).Next;
            while (next.Value != 1)
            {
                yield return next.Value;
                next = next.Next;
            }
        }
    }
    record Cup(int Value)
    {
        public Cup Next { get; set; }
    }
}
