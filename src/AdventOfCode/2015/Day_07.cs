using Circuit = SmartAss.Circuits.Circuit<ushort>;
using Node = SmartAss.Circuits.CircuitNode<ushort>;

namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_07
{
    private const string Example = @"
123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> a";
    [Example(answer: 65079, Example)]
    [Puzzle(answer: 956)]
    public ushort part_one(string input)
        => Parse(input)["a"].Output ?? throw new NoAnswer();

    [Puzzle(answer: 40149)]
    public ushort part_two(string input)
    {
        var circuit = Parse(input);
        circuit["b"] = new Circuit.Constant(956);
        return circuit["a"].Output ?? throw new NoAnswer();
    }

    private static Circuit Parse(string input)
    {
        var circuit = new Circuit();

        foreach (var line in input.Lines())
        {
            var split = line.Split(" -> ");
            var instruction = split[0].Split(' ');
            var assigned = split[1];

            if (instruction.Length == 1 && ushort.TryParse(instruction[0], out var constant))
            {
                circuit[assigned] = new Circuit.Constant(constant);
            }
            else if (instruction.Length == 1)
            {
                circuit[assigned] = new Circuit.Assignment(circuit.NewVariable(instruction[0]));
            }
            else if (instruction.Length == 2 && instruction[0] == "NOT")
            {
                circuit[assigned] = new Not(New(instruction[1], circuit));
            }
            else if (instruction.Length == 3 && instruction[1] == "LSHIFT" && ushort.TryParse(instruction[2], out var lshift))
            {
                circuit[assigned] = new LShift(New(instruction[0], circuit), lshift);
            }
            else if (instruction.Length == 3 && instruction[1] == "RSHIFT" && ushort.TryParse(instruction[2], out var rshift))
            {
                circuit[assigned] = new RShift(New(instruction[0], circuit), rshift);
            }
            else if (instruction.Length == 3 && instruction[1] == "OR")
            {
                circuit[assigned] = new Or(New(instruction[0], circuit), New(instruction[2], circuit));
            }
            else if (instruction.Length == 3 && instruction[1] == "AND")
            {
                circuit[assigned] = new And(New(instruction[0], circuit), New(instruction[2], circuit));
            }
            else throw new FormatException($"'{line}' could not be parsed.");
        }

        return circuit;

        static Node New(string str, Circuit circuit)
            => ushort.TryParse(str, out var constant)
            ? new Circuit.Constant(constant)
            : circuit.NewVariable(str);
    }

    record Not(Node Other) : Node
    {
        protected override ushort? Execute() => Other.Output.HasValue ? (ushort)~Other.Output.Value : null;
    }
    record And(Node Left, Node Right) : Node
    {
        protected override ushort? Execute()
            => Left.Output.HasValue && Right.Output.HasValue
            ? (ushort)(Left.Output.Value & Right.Output.Value)
            : null;
    }
    record Or(Node Left, Node Right) : Node
    {
        protected override ushort? Execute() 
            => Left.Output.HasValue  && Right.Output.HasValue
            ? (ushort)(Left.Output.Value | Right.Output.Value)
            : null;
    }
    record LShift(Node Other, int Shift): Node
    {
        protected override ushort? Execute() => Other.Output.HasValue ? (ushort)(Other.Output.Value << Shift) : null;
    }
    record RShift(Node Other, int Shift) : Node
    {
        protected override ushort? Execute() => Other.Output.HasValue ? (ushort)(Other.Output.Value >> Shift) : null;
    }
}
