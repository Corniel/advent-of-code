namespace Advent_of_Code;

public class InfiniteLoop : InvalidOperationException
{
    public InfiniteLoop() : base("Infinite loop is occurring.") => Do.Nothing();
}
