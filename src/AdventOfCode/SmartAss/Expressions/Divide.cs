namespace SmartAss.Expressions;

public sealed class Divide : Binary
{
    public Divide(Expr left, Expr right) : base(left, right) { }

    protected override long TryValue(long left, long right) => left / right;

    public override string ToString() => $"({Left} / {Right})";

    protected override long Solve(long value, long? left, long? right)
        => left is not { } left_
        ? value * right ?? throw new NoAnswer()
        : left_ / value;
}
