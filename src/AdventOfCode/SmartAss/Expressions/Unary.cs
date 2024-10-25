namespace SmartAss.Expressions;

public abstract class Unary : Expr
{
    protected Unary(Expr expression) => Expression = expression;

    public Expr Expression { get; }
}
