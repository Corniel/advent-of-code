namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ExampleAttribute(object answer, params object[] input) : PuzzleAttribute(answer, input)
{
    private readonly Example Example = input.OfType<Example>().FirstOrDefault();

    protected override AdventPuzzle Puzzle(IMethodInfo method) => new(method.MethodInfo, Input, Answer, Order, Example);

    protected override string TestName(IMethodInfo method, string input)
    {
        if (Example != default) return $"answer is {Answer} for example {(int)Example}";
        else return input.Contains('\n')
            ? $"answer is {Answer} for {method.Name.Replace("_", " ")} example with length {input.Length}"
            : $"answer is {Answer} for {input}";
    }
}
