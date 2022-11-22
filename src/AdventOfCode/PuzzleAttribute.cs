namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleAttribute : Attribute, ITestBuilder, IImplyFixture
{
    public PuzzleAttribute(object answer, string input)
    {
        Answer = answer;
        Input = input;
    }

    public PuzzleAttribute(object answer, int year, int day)
        : this(answer, Puzzle.Input(year, day)) => Do.Nothing();

    public object Answer { get; }
    public string Input { get; }

    /// <summary>
    /// Builds a single test from the specified method and context.
    /// </summary>
    /// <param name="method">The MethodInfo for which tests are to be constructed.</param>
    /// <param name="suite">The suite to which the tests will be added.</param>
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
        var parameters = new TestCaseParameters(new[] { Input })
        {
            ExpectedResult = ExpectedResult(method.MethodInfo.ReturnType),
        };

        var test = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
        test.Name = TestName(method);
        yield return test;
    }
    private object ExpectedResult(Type type)
    {
        if (type == typeof(Int))
        {
            if (Answer is int int32) { return (Int)int32; }
            else if (Answer is long int64) { return (Int)int64; }
            else if (Answer is string str) { return Int.Parse(str); }
            else { return Answer; }
        }
        else if (type == typeof(string) && Answer is string str) { return str.Trim(); }
        else { return Answer; }
    }

    protected virtual string TestName(IMethodInfo method)
        => $"answer is {Answer} for {method.Name.Replace("_", " ")}";
}
