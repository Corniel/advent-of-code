namespace Advent_of_Code_2019;

public class UnknownInstruction(string message) : InvalidOperationException(message)
{
    public static UnknownInstruction For(int code) => new($"Instruction {code:00} is unknown.");
}
