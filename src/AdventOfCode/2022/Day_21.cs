namespace Advent_of_Code_2022;

[Category(Category.Expressions)]
public class Day_21
{
    [Example(answer: 152, Example._1)]
    [Puzzle(answer: 158731561459602, O.μs100)]
    public long part_one(string input) => Expressions(input).Value("root");

    [Example(answer: 301, Example._1)]
    [Puzzle(answer: 3769668716709, O.μs100)]
    public long part_two(string input)
    {
        var pars = Expressions(input);
        pars["humn"] = Expr.Variable();
        var root = (Binary)pars["root"];
        Expr.Subtract(root.Left, root.Right).Solve(0, pars);
        return pars["humn"].Value(pars);
    }

    static Params Expressions(string input) => Params.New(input.Lines(Param));

    static Param Param(string line) => new(line[0..4], line.Int32N() is { } n 
        ? Expr.Const(n) 
        : Expr.Binary(Expr.Ref(line[6..10]), line[11..12], Expr.Ref(line[13..17])));
}
