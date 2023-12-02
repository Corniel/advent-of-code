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
        Input = Tweak(input);
        Answer = GetAnswer(answer, method.ReturnType);
        Order = order;
    }

    object[] Tweak(object[] input)
    {
        input[0] ??= Embedded();
        var targets = Method.GetParameters().Select(p => p.ParameterType).ToArray();
        return input.Select((obj, i) => Tweak(obj, targets[i])).ToArray();
    }
    static object Tweak(object obj, Type target)
    {
        if (obj is string str)
        {
            if (target == typeof(Lines)) return new Lines(str.Lines());
            else if (target == typeof(GroupedLines)) return new GroupedLines(str.GroupedLines(StringSplitOptions.None).ToArray());
            else if (target == typeof(CharPixels)) return str.CharPixels();
            else if (target == typeof(CharGrid)) return str.CharPixels().Grid();
            else return str;
        }
        else return obj;
    }

    public AdventDate Date { get; }
    public Example Example { get; }
    public MethodInfo Method { get; }
    public object[] Input { get; }
    public object Answer { get; }
    public O Order { get; }

    public bool Matches(AdventDate date) => Date.Matches(date);


    public TestCaseParameters TestCaseParameters()
    {
        return new TestCaseParameters(Input) { ExpectedResult = Answer };
    }

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"{Date} {Method.Name}";

    private string Embedded()
    {
        var assembly = Method.DeclaringType.Assembly;
        var path = $"Advent_of_Code._{Date.Year}.Day_{Date.Day:00}{(Example != Example.None ? $"_{(int)Example}" : "")}.txt";
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream is null)
        {
            var x = new FileNotFoundException(path);
            Console.WriteLine(x);
            return x.ToString();
        }
        var reader = new StreamReader(stream, Encoding.UTF8);
        var text = reader.ReadToEnd();
        return text.Length >= 2 && text[^2..] == "\r\n" ? text[..^2] : text;
    }

    static AdventDate GetDate(MethodInfo method)
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

    static object GetAnswer(object answer, Type type)
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
