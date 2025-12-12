namespace Advent_of_Code;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PuzzleAttribute : Attribute, ITestBuilder, IApplyToTest, IImplyFixture
{
    public PuzzleAttribute(object answer, params object[] input)
    {
        input ??= [];
        Answer = answer;
        Order = input.OfType<O>().FirstOrDefault();
        Input = Filter(input);
    }
    static object[] Filter(IEnumerable<object> input)
        => input.Where(o => o is not O && o is not Example).ToArray() is { Length :> 0 } f 
        ? f
        : [null];

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
        parameters.TestName = Hash(method, puzzle.Input);

        var test = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
        test.Name = TestName(method, puzzle.Input[0]);
        return [test];
    }

    protected virtual AdventPuzzle Puzzle(IMethodInfo method) => new(method.MethodInfo, Input, Answer, Order);

    protected virtual string TestName(IMethodInfo method, object input)
        => $"{method.Name.Replace("_", " ")}: {Answer}";

    /// <summary>Gets a unique input based named.</summary>
    /// <remarks>
    /// MS Test groups unit tests based on names. However, the test display name
    /// is not always helping to do the right thing, nor is the name that is
    /// automatically provided. This hash name solves the problem.
    /// </remarks>
    private static string Hash(IMethodInfo method, object[] parameters)
        => method.Name + string.Join(";", parameters.Select(Hash));

    private static string Hash(object obj) => Hash(obj is CharGrid map ? map.ToString(c => c) : obj.ToString());

    private static string Hash(string str) => Uuid.GenerateWithSHA1(Encoding.UTF8.GetBytes(str)).ToString();

    public void ApplyToTest(Test test)
    {
        if (Order != O.Unknown)
        {
            test.Properties.Add(PropertyNames.Category, Order);
        }
    }
}
