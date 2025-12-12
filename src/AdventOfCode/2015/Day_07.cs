namespace Advent_of_Code_2015;

[Category(Category.ExpressionEvaluation)]
public class Day_07
{
    [Example(answer: 65079, "123 -> x;456 -> y;x AND y -> d;x OR y -> e;x LSHIFT 2 -> f;y RSHIFT 2 -> g;NOT x -> h;NOT y -> a")]
    [Puzzle(answer: 956L, O.μs10)]
    public long part_one(Inputs<Param> pars) => Params.New(pars).Value("a");

    [Puzzle(answer: 40149L, O.μs10)]
    public long part_two(Inputs<Param> pars)
    {
        var ps = Params.New(pars);
        ps["b"] = Expr.Const(956);
        return ps.Value("a");
    }

    public static Param Parse(string line)
    {
        var parts = line.Separate(" ", "->");
        return new(parts[^1], new Ushort(Express(parts)));
    }

    static Expr Express(string[] parts) => parts.Length switch
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

    static Expr Arg(string str) => str.Int32N() is { } n ? Expr.Const(n) : Expr.Ref(str);
}
