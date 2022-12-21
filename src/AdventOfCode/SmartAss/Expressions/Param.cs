namespace SmartAss.Expressions;

public readonly struct Param
{
    public readonly string Name;
    public readonly Expr Expr;

    public Param(string name, Expr expr)
    {
        Name = name;
        Expr = expr;
    }

    public override string ToString() => $"{Name} = {Expr}";
}
