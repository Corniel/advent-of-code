namespace SmartAss.Expressions;

public sealed class Multiply(Expr left, Expr right) : Binary(left, right)
{
    protected override string Operator => "*";

    protected override long Solve(long value, long? left, long? right)
        => value / (left ?? right ?? throw new NotSolved());

    protected override long TryValue(long left, long right) => left * right;
}
