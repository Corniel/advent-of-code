namespace SmartAss.Expressions;

internal class BitNot(Expr expression) : Unary(expression)
{
    public override void Solve(long value, Params pars)=> throw new NotImplementedException();

    public override long? TryValue(Params pars) => Expression.Value(pars) is { } value ? ~value : null;

    public override string ToString() => $"~{Expression}";
}
