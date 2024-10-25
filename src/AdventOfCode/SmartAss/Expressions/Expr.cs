namespace SmartAss.Expressions;

public abstract class Expr
{
    public long Value(Params pars) => TryValue(pars) ?? throw new NotSolved();

    public abstract void Solve(long value, Params pars);

    public abstract long? TryValue(Params pars);

    public static Const Const(long val) => new(val);

    public static Ref Ref(string name) => new(name);

    public static Variable Variable() => new();

    internal static Add Add(Expr left, Expr right) => new(left, right);
    
    internal static Subtract Subtract(Expr left, Expr right) => new(left, right);

    public static Binary Binary(Expr left, string op, Expr right) => op switch
    {
        "mul" or "*" => new Multiply(left, right),
        "div" or "/" => new Divide(left, right),
        "add" or "+" => Add(left, right),
        "sub" or "-" => Subtract(left, right),
        "==" => new Equal(left, right),
        "!=" => new NotEqual(left, right),
        ">" => new GT(left, right),
        "<" => new LT(left, right),
        ">=" => new GE(left, right),
        "<=" => new LE(left, right),
        "<<" => new ShiftLeft(left, right),
        ">>" => new ShiftRight(left, right),
        _ => throw new NotSupportedException($"The '{op}' is not supported.")
    };

   
}
