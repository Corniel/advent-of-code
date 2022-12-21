namespace SmartAss.Expressions;

[Serializable]
public class NotSolved : InvalidOperationException
{
    public NotSolved() : base("The expression is not solved.") { }
}
