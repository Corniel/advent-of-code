namespace SmartAss.Expressions;

public sealed class Variable : Expr
{
    public long? Val { get; set; }

    public override void Solve(long value, Params pars) => Val = value;

    public override long? TryValue(Params pars) => Val;

    public override string ToString()=> Val is null ? "?" : Val.ToString();
}
