namespace SmartAss.Expressions;

public sealed class Ref(string name) : Expr
{
    public string Name { get; } = name;

    public override void Solve(long value, Params pars) => pars[Name].Solve(value, pars);

    public override long? TryValue(Params pars) => pars.TryValue(Name);

    public override string ToString() => Name;
}
