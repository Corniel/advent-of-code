namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleAttribute : Attribute, ITestBuilder, IImplyFixture
{
    public PuzzleAttribute(object answer, string input)
    {
        Answer = answer;
        Input = input;
    }

    public PuzzleAttribute(object answer) : this(answer, null) { }

    public object Answer { get; }
    public string Input { get; }

    /// <summary>
    /// Builds a single test from the specified method and context.
    /// </summary>
    /// <param name="method">The MethodInfo for which tests are to be constructed.</param>
    /// <param name="suite">The suite to which the tests will be added.</param>
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
        var puzzle = Puzzle(method);
        var parameters = puzzle.TestCaseParameters();
        var test = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
        test.Name = TestName(method, puzzle.Input);
        yield return test;
    }

    protected virtual AdventPuzzle Puzzle(IMethodInfo method) => new AdventPuzzle(method.MethodInfo, Input, Answer);

    protected virtual string TestName(IMethodInfo method, string input)
        => $"answer is {Answer} for {method.Name.Replace("_", " ")}";
}
