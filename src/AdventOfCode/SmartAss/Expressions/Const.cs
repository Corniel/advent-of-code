namespace SmartAss.Expressions;

public sealed class Const(long value) : Expr
{
    public long Val { get; } = value;

    public override void Solve(long value, Params pars) => throw new NotSolved();

    public override long? TryValue(Params pars) => Val;

    public override string ToString() => Val.ToString();
}
