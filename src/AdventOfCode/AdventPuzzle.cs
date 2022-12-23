using System.Diagnostics.Contracts;
using System.Reflection;

namespace Advent_of_Code;

public sealed class AdventPuzzle
{
    public AdventPuzzle(MethodInfo method, object[] input, object answer, O order, Example example = default)
        : this(GetDate(method), method, input, answer, order, example) { }

    public AdventPuzzle(AdventDate date, MethodInfo method, object[] input, object answer, O order, Example example)
    {
        Date = date;
        Example = example;
        Method = method;
        Input = input;
        Answer = GetAnswer(answer, method.ReturnType);
        Input[0] ??= Embedded();
        Order = order;
    }

    public AdventDate Date { get; }
    public Example Example { get; }
    public MethodInfo Method { get; }
    public object[] Input { get; }
    public object Answer { get; }
    public O Order { get; }

    public bool Matches(AdventDate date) => Date.Matches(date);

    public TestCaseParameters TestCaseParameters()  => new(Input) { ExpectedResult = Answer };

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"{Date} {Method.Name}";

    private string Embedded()
    {
        var assembly = Method.DeclaringType.Assembly;
        var path = $"Advent_of_Code._{Date.Year}.Day_{Date.Day:00}{(Example != Example.None ? $"_{(int)Example}" : "")}.txt";
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream is null) return new FileNotFoundException(path).ToString();
        var reader = new StreamReader(stream, Encoding.UTF8);
        var text = reader.ReadToEnd();
        return text[^2..] == "\r\n" ? text[..^2] : text;
    }

    private static AdventDate GetDate(MethodInfo method)
    {
        var numbers = method.DeclaringType.FullName.Int32s().ToArray();
        return new(year: numbers[0], day: numbers[1], part: Day(method));

        static int? Day(MethodInfo method) => method.Name switch
        {
            nameof(Advent_of_Code_2015.Day_01.part_one) => 1,
            nameof(Advent_of_Code_2015.Day_01.part_two) => 2,
            _ => null,
        };
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
        else if (type == typeof(Point) && answer is string point) { return Point.Parse(point); }
        else return answer;
    }
}
