namespace Advent_of_Code_2022;

[Category(Category.Computation)]
public class Day_21
{
    [Example(answer: 152, Example._1)]
    [Puzzle(answer: 158731561459602, O.μs100)]
    public double part_one(string input) => Node.All(input).Values.Max(m => m.Value);

    [Example(answer: 301, Example._1)]
    [Puzzle(answer: 3769668716709, O.ms)]
    public double part_two(string input)
    {
        var monkeys = Node.All(input);
        var root = monkeys.Values.OfType<Operation>().OrderByDescending(m => m.Value).First();
        root.Operator = '=';
        var human = (Constant)monkeys["humn"];
        var range = new Range(long.MinValue, long.MaxValue);

        while (root.Abs > 0.1)
        {
            human.Value = range.Left.Middle;
            var left = root.Abs;
            human.Value = range.Right.Middle;
            range = left < root.Abs ? range.Left: range.Right;
            human.Value = range.Middle;
        }
        return Math.Round(human.Value);
    }

    record struct Range(double Lo, double Hi)
    {
        public Range Left => new(Lo, Middle);
        public Range Right => new(Middle, Hi);
        public double Middle => Math.Round((Lo + Hi) / 2);
    }
    abstract class Node
    {
        protected Node(string name) => Name = name;
        public string Name { get; }
        public abstract double Value { get; set; }
        public double Abs => Math.Abs(Value);
        public static Dictionary<string, Node> All(string input)
        {
            var monkeys = input.Lines(Parse).ToDictionary(n => n.Name, n => n);
            foreach (var line in input.Lines())
            {
                if (monkeys[line[0..4]] is Operation monkey)
                {
                    monkey.Left = monkeys[line[6..10]];
                    monkey.Right = monkeys[line[13..17]];
                }
            }
            return monkeys;
        }
        static Node Parse(string line)  => line.Int32N() is { } n ? new Constant(line[0..4], n) : new Operation(line[0..4], line[11]);
    }

    class Constant : Node
    {
        public Constant(string name, double value) : base(name) => Value = value;
        public override double Value { get; set; }
    }
    class Operation : Node
    {
        public Operation(string name, char op) : base(name) => Operator = op;
        public char Operator { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public override double Value
        {
            get => Operator switch
            {
                '+' => Left.Value + Right.Value,
                '*' => Left.Value * Right.Value,
                '/' => Left.Value / Right.Value,
                /* '=' or '-'*/ _ => Left.Value - Right.Value,
            };
            set => throw new NotSupportedException();
        }
    }
}
