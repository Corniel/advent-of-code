namespace SmartAss.Syntax;

public class SyntaxError : FormatException
{
    public SyntaxError() : this("Syntax error.") => Do.Nothing();
    public SyntaxError(string message) : base(message) => Do.Nothing();
    public SyntaxError(string message, Exception innerException)
        : base(message, innerException) => Do.Nothing();

    public static SyntaxError EndOfInput => new("End of input reached.");

    internal static SyntaxError UnexpectedToken(char actual, int position)
       => new($"Unexpected token at pos {position}. Expected none, but got {actual}.");

    internal static SyntaxError UnexpectedToken(char expected, char actual, int position)
        => new($"Unexpected token at pos {position}. Expected '{expected}', but got {actual}.");

    internal static SyntaxError UnexpectedToken(char expected, string actual, int position)
        => new($"Unexpected token at pos {position}. Expected {expected}, but got {actual}.");
}
