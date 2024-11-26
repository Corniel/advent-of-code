namespace SmartAss.Expressions;

public abstract class Binary : Expr
{
    protected Binary(Expr left, Expr right)
    {
        Left = left;
        Right = right;
    }

    public Expr Left { get; }

    public Expr Right { get; }

    public override void Solve(long value, Params pars)
    {
        long value_;
        if (Left.TryValue(pars) is { } l)
        {
            value_ = Solve(value, l, null);
            Right.Solve(value_, pars);
        }
        else if (Right.TryValue(pars) is { } r)
        {
            value_ = Solve(value, null, r);
            Left.Solve(value_, pars);
        }
        else throw new NotSolved();
    }

    public override long? TryValue(Params pars)
        => Left.TryValue(pars) is { } left
        && Right.TryValue(pars) is { } right
        ? TryValue(left, right)
        : null;

    public sealed override string ToString() => $"({Left} {Operator} {Right})";

    protected abstract string Operator { get; }

    protected abstract long Solve(long value, long? left, long? right);

    protected abstract long TryValue(long left, long right);
}
