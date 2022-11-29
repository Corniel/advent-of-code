using System.Diagnostics.Contracts;
using System.Reflection;

namespace Advent_of_Code;

public sealed class AdventPuzzle
{
    public AdventPuzzle(MethodInfo method, string input, object answer, int? example = null)
        : this(GetDate(method), method, input, answer, example) { }

    public AdventPuzzle(AdventDate date, MethodInfo method, string input, object answer, int? example = null)
    {
        Date = date;
        Example = example;
        Method = method;
        Input = input ?? Embedded();
        Answer = GetAnswer(answer, method.ReturnType);
    }

    public AdventDate Date { get; }
    public int? Example { get; }
    public MethodInfo Method { get; }
    public string Input { get; }
    public object Answer { get; }

    public bool Matches(AdventDate date) => Date.Matches(date);

    public TestCaseParameters TestCaseParameters() 
        => new(new object[] { Input })
        {
            ExpectedResult = Answer,
        };

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"{Date} {Method.Name}";

    private string Embedded()
    {
        var assembly = Method.DeclaringType.Assembly;
        var path = $"Advent_of_Code._{Date.Year}.Day_{Date.Day:00}{(Example.HasValue ? $"_{Example}" : "")}.txt";
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream is null) return new FileNotFoundException(path).ToString();
        var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private static AdventDate GetDate(MethodInfo method)
    {
        var numbers = method.DeclaringType.FullName.Int32s().ToArray();
        return new(year: numbers[0], day: numbers[1], part: null);
    }

    private static object GetAnswer(object answer, Type type)
    {
        if (type == typeof(Int))
        {
            if (answer is int int32) { return (Int)int32; }
            else if (answer is long int64) { return (Int)int64; }
            else if (answer is string str) { return Int.Parse(str); }
            else { return answer; }
        }
        else if (type == typeof(string) && answer is string str) { return str.Trim(); }
        else return answer;
    }
}
