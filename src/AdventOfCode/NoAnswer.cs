namespace Advent_of_Code;

public class NoAnswer : InvalidOperationException
{
    public NoAnswer() : base("No answer was found.") => Do.Nothing();
}
