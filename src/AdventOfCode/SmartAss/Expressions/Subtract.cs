namespace SmartAss.Expressions;

public sealed class Subtract(Expr left, Expr right) : Binary(left, right)
{
    protected override string Operator => "-";

    protected override long TryValue(long left, long right) => left - right;

    protected override long Solve(long value, long? left, long? right)
        => left is not { } left_
        ? value + right ?? throw new NoAnswer()
        : left_ - value;
}
