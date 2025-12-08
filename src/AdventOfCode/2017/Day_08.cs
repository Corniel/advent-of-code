namespace Advent_of_Code_2017;

[Category(Category.ExpressionEvaluation)]
public class Day_08
{
    [Example(answer: 1, "b inc 5 if a > 1;a inc 1 if b < 5;c dec -10 if a >= 1;c inc -20 if c == 10")]
    [Puzzle(answer: 6611L, O.μs100)]
    public long part_one(Inputs<Instr> input) => Max(input);

    [Example(answer: 10, "b inc 5 if a > 1;a inc 1 if b < 5;c dec -10 if a >= 1;c inc -20 if c == 10")]
    [Puzzle(answer: 6619L, O.μs100)]
    public long part_two(Inputs<Instr> input) => Max(input, true);

    static long Max(Inputs<Instr> input, bool intermediate = false)
    {
        var paramaters = Params.New(input.As(instr => instr.Name).Distinct().Select(n => new Param(n, Const.Zero)));
        var max = 0L;

        foreach (var instr in input.Where(i => i.Condition.Value(paramaters) == 1))
        {
            var test = instr.Action.Value(paramaters);
            paramaters[instr.Name] = Expr.Const(test);
            max = Math.Max(test, max);
        }
        return intermediate ? max : paramaters.Max(p => paramaters.Value(p.Name));
    }

    public record Instr(Binary Action, Binary Condition)
    {
        public string Name => ((Ref)Action.Left).Name;

        public static Instr Parse(string line)
        {
            var parts = line.Split(' ');
            var inc = Expr.Const(parts[2].Int32() * (parts[1] == "inc" ? 1 : -1));
            var val = Expr.Const(parts[6].Int32());

            return new(Expr.Add(Expr.Ref(parts[0]), inc), Expr.Binary(Expr.Ref(parts[4]), parts[5], val));
        }
    }
}
