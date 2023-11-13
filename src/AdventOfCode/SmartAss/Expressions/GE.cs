namespace SmartAss.Expressions;

public sealed class GE : Binary
{
    public GE(Expr left, Expr right) : base(left, right) { }

    protected override string Operator => ">=";

    protected override long Solve(long value, long? left, long? right) => throw new NotSupportedException();

    protected override long TryValue(long left, long right) => left >= right ? 1 : 0;
}
