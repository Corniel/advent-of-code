﻿namespace Advent_of_Code_2015;

[Category(Category.ExpressionEvaluation)]
public class Day_07
{
    [Example(answer: 65079, "123 -> x;456 -> y;x AND y -> d;x OR y -> e;x LSHIFT 2 -> f;y RSHIFT 2 -> g;NOT x -> h;NOT y -> a")]
    [Puzzle(answer: 956, O.μs100)]
    public long part_one(string input) => Pars(input).Value("a");

    [Puzzle(answer: 40149, O.μs100)]
    public long part_two(string input)
    {
        var pars = Pars(input);
        pars["b"] = SmartAss.Expressions.Expr.Const(956);
        return pars.Value("a");
    }

    public static Params Pars(string input) => Params.New(input.Lines(Param));

    static Param Param(string line)
    {
        var parts = line.Separate(" ", "->");
        return new(parts[^1], new Ushort(Expr(parts)));
    }

    static Expr Expr(IReadOnlyList<string> parts) => parts.Count switch
    {
        2 => Arg(parts[0]),
        3 => new BitNot(Arg(parts[1])),
        _ => parts[1] switch
        {
            "LSHIFT" => new ShiftLeft(Arg(parts[0]), Arg(parts[2])),
            "RSHIFT" => new ShiftRight(Arg(parts[0]), Arg(parts[2])),
            "AND" => new BitAnd(Arg(parts[0]), Arg(parts[2])),
            "OR" or _ => new BitOr(Arg(parts[0]), Arg(parts[2])),
        }
    };

    static Expr Arg(string str) => str.Int32N() is { } n ? SmartAss.Expressions.Expr.Const(n) : SmartAss.Expressions.Expr.Ref(str);
}
