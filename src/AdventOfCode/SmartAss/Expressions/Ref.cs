namespace SmartAss.Expressions;

public sealed class Ref : Expr
{
    public Ref(string name) => Name = name;

    public string Name { get; }

    public override void Solve(long value, Params pars) => pars[Name].Solve(value, pars);

    public override long? TryValue(Params pars) => pars.TryValue(Name);

    public override string ToString() => Name;
}
