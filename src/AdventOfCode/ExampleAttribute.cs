namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ExampleAttribute : PuzzleAttribute
{
    public ExampleAttribute(object answer, string input)
        : base(answer, input) => Do.Nothing();
    public ExampleAttribute(object answer, int example) : base(answer)
    {
        Example = example;
    }

    private readonly int Example;

    protected override AdventPuzzle Puzzle(IMethodInfo method) => new AdventPuzzle(method.MethodInfo, Input, Answer, Example);

    protected override string TestName(IMethodInfo method, string input)
       => input.Contains('\n')
       ? $"answer is {Answer} for {method.Name.Replace("_", " ")} example with length {input.Length}"
       : $"answer is {Answer} for {input}";
}
