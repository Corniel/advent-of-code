namespace Advent_of_Code_2022;

[Category(Category.Simulation)]
public class Day_11
{
    [Example(answer: 10605, Example._1)]
    [Puzzle(answer: 121450, O.μs10)]
    public long part_one(GroupedLines groups) => Simulate(groups, 20, 3);

    [Example(answer: 2713310158, Example._1)]
    [Puzzle(answer: 28244037010, O.ms)]
    public long part_two(GroupedLines groups) => Simulate(groups, 10_000, 1);

    static long Simulate(GroupedLines groups, int simulations, int reduce)
    {
        var monkeys = groups.Select(Monkey.Parse).ToArray();
        var modulo = monkeys.Select(m => m.Factor).Product();

        foreach (var monkey in Range(1, simulations).SelectMany(_ => monkeys))
        {
            monkey.Play(monkeys, reduce, modulo);
        }
        return monkeys.Select(m => m.Inspected).OrderDescending().Take(2).Product();
    }

    record Monkey(Queue<long> Items, Operation Operation, int Divisible, int IfTrue, int IfFalse)
    {
        public long WorryLevel { get; set; }
        public long Inspected { get; set; }
        public long Factor => Divisible * Operation.Factor;
        public void Play(Monkey[] other, int reduce, long mod)
        {
            foreach (var item in Items.DequeueCurrent())
            {
                Inspected++;
                WorryLevel = (Operation.Invoke(item) / reduce).Mod(mod);
                var next = WorryLevel % Divisible == 0 ? IfTrue : IfFalse;
                other[next].Items.Enqueue(WorryLevel);
            }
        }

        public static Monkey Parse(string[] lines) => new(new(lines[1].Int64s()), Operation.Parse(lines[2]), lines[3].Int32(), lines[4].Int32(), lines[5].Int32());
    }
    record Operation(char Op, long? Right)
    {
        public long Invoke(long value) => Op == '+' ? value + (Right ?? value) : value * (Right ?? value);

        public long Factor => Op == '*' && Right.HasValue ? Right.Value : 1L;

        public static Operation Parse(string line) => new(line.TrimStart()[21], line.Int32N());
    }
}
