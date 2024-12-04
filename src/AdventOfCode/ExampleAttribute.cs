namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ExampleAttribute(object answer, params object[] input) : PuzzleAttribute(answer, input)
{
    private readonly Example Example = input.OfType<Example>().FirstOrDefault();

    protected override AdventPuzzle Puzzle(IMethodInfo method) => new(method.MethodInfo, Input, Answer, Order, Example);

    protected override string TestName(IMethodInfo method, object input)
        => Example != default
        ? $"{base.TestName(method, input)} (example {(int)Example})"
        : $"{base.TestName(method, input)}, {TestInput(method, input)}";

    static string TestInput(IMethodInfo method, object input)
    {
        var str = input switch
        {
            CharPixels pixels => pixels.ToString().Replace("\r\n", ";"),
            CharGrid grid => grid.ToString(c => c).Replace("\r\n", ";"),
            _ => input.ToString(),
        };

        return str switch
        {
            _ when str.Contains('\n') => $"{method.Name.Replace("_", " ")} example with length {input.ToString().Length}",
            _ when str.Length > 50 => str[..40] + "...",
            _ => str,
        };
    }
}
