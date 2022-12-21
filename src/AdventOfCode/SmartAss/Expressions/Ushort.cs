namespace SmartAss.Expressions;

public sealed class Ushort : Unary
{
    public Ushort(Expr expression) : base(expression) { }

    public override void Solve(long value, Params pars) => throw new NotSupportedException();

    public override long? TryValue(Params pars)
        => Expression.TryValue(pars) is { } value
        ? value & ushort.MaxValue
        : null;

    public override string ToString() => $"(ushort){Expression}";
}
