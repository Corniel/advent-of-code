namespace SmartAss.Expressions;

public readonly struct Param(string name, Expr expr)
{
    public readonly string Name = name;
    public readonly Expr Expr = expr;

    public override string ToString() => $"{Name} = {Expr}";
}
