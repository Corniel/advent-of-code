namespace SmartAss.Expressions;

public sealed class Ushort(Expr expression) : Unary(expression)
{
    public override void Solve(long value, Params pars) => throw new NotSupportedException();

    public override long? TryValue(Params pars)
        => Expression.TryValue(pars) is { } value
        ? value & ushort.MaxValue
        : null;

    public override string ToString() => $"(ushort){Expression}";
}
