namespace SmartAss.Expressions;

public sealed class Add : Binary
{
    public Add(Expr left, Expr right) : base(left, right) { }

    protected override long TryValue(long left, long right) => left + right;

    public override string ToString() => $"({Left} + {Right})";

    protected override long Solve(long value, long? left, long? right)
        => value - (left ?? right ?? throw new NotSolved());

}
