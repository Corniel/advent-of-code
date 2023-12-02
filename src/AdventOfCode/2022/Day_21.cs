namespace Advent_of_Code_2022;

[Category(Category.ExpressionEvaluation)]
public class Day_21
{
    [Example(answer: 152, Example._1)]
    [Puzzle(answer: 158731561459602, O.μs100)]
    public long part_one(Lines lines) => Expressions(lines).Value("root");

    [Example(answer: 301, Example._1)]
    [Puzzle(answer: 3769668716709, O.μs100)]
    public long part_two(Lines lines)
    {
        var pars = Expressions(lines);
        pars["humn"] = Expr.Variable();
        var root = (Binary)pars["root"];
        Expr.Subtract(root.Left, root.Right).Solve(0, pars);
        return pars["humn"].Value(pars);
    }

    static Params Expressions(Lines lines) => Params.New(lines.As(Param));

    static Param Param(string line) => new(line[0..4], line.Int32N() is { } n 
        ? Expr.Const(n) 
        : Expr.Binary(Expr.Ref(line[6..10]), line[11..12], Expr.Ref(line[13..17])));
}
