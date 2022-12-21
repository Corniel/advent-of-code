namespace SmartAss.Expressions;

public sealed class Add : Binary
{
    public Add(Expr left, Expr right) : base(left, right) { }

    protected override string Operator => "+";

    protected override long TryValue(long left, long right) => left + right;

    protected override long Solve(long value, long? left, long? right)
        => value - (left ?? right ?? throw new NotSolved());
}
