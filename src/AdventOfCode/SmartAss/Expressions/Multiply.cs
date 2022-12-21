namespace SmartAss.Expressions;

public sealed class Multiply : Binary
{
    public Multiply(Expr left, Expr right) : base(left, right) { }

    protected override long Solve(long value, long? left, long? right)
        => value / (left ?? right ?? throw new NotSolved());

    protected override long TryValue(long left, long right) => left * right;

    public override string ToString() => $"({Left} * {Right})";
}
