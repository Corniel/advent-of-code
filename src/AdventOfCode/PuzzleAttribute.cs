namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleAttribute : Attribute, ITestBuilder, IApplyToTest, IImplyFixture
{
    public PuzzleAttribute(object answer, params object[] input)
    {
        input ??= Array.Empty<object>();
        Answer = answer;
        Order = input.OfType<O>().FirstOrDefault();
        Input = Filter(input);
    }
    static object[] Filter(IEnumerable<object> input)
    {
        var filtered = input.Where(o => o is not O && o is not Example).ToArray();
        return filtered.Length == 0 ? new object[] { null } : filtered;
    }

    public object Answer { get; }

    public O Order { get; }
    
    public object[] Input { get; }

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
        test.Name = TestName(method, puzzle.Input[0].ToString());
        yield return test;
    }

    protected virtual AdventPuzzle Puzzle(IMethodInfo method) => new(method.MethodInfo, Input, Answer, Order);

    protected virtual string TestName(IMethodInfo method, string input)
        => $"answer is {Answer} for {method.Name.Replace("_", " ")}";

    public void ApplyToTest(Test test)
    {
        if (Order != O.Unknown)
        {
            test.Properties.Add(PropertyNames.Category, Order);
        }
    }
}
