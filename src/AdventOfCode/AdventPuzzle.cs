using System.Reflection;
using static Advent_of_Code_2021.Day_24;

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
        var targets = Method.GetParameters().Select(p => p.ParameterType);
        return [.. input.Zip(targets, Tweak)];
    }
    object Tweak(object obj, Type target)
    {
        return obj is not string str
            ? obj
            : target switch
            {
                _ when GenericInput() is { } generic && Inputs.Parse(generic, Method.DeclaringType, str) is { } tweak => tweak,
                _ when Is<Lines>() => Inputs.New(str.Lines()),
                _ when Is<Ints>() => Inputs.New(str.Int32s()),
                _ when Is<Longs>() => Inputs.New(str.Int64s()),
                _ when Is<Point2Ds>() => Inputs.New(str.Int32s().ChunkBy(2).Fix(Ctor.New<Point>)),
                _ when Is<Point3Ds>() => Inputs.New(str.Int32s().ChunkBy(3).Fix(Ctor.New<Point3D>)),
                _ when Is<Point4Ds>() => Inputs.New(str.Int32s().ChunkBy(4).Fix(Ctor.New<Point4D>)),
                _ when Is<Int32Ranges>() => Int32Ranges.Parse(str),
                _ when Is<GroupedLines>() => new GroupedLines([.. str.GroupedLines(StringSplitOptions.None)]),
                _ when Is<CharPixels>() => str.CharPixels(),
                _ when Is<CharGrid>() => str.CharPixels().Grid(),
                _ when Is<Int64Ranges>() => Int64Ranges.Parse(str),
                _ when GenericInput() is { } generic => Inputs.Parse(generic, str),
                _ => str,
            };

        bool Is<T>() => typeof(T) == target;

        Type GenericInput()
            => target.IsGenericType
            && target.GetGenericTypeDefinition() == typeof(Inputs<>)
            ? target.GenericTypeArguments[0]
            : null;
    }

    public AdventDate Date { get; }
    public Example Example { get; }
    public MethodInfo Method { get; }
    public object[] Input { get; }
    public object Answer { get; }
    public O Order { get; }

    public bool Matches(AdventDate date) => Date.Matches(date);


    public TestCaseParameters TestCaseParameters() => new(Input) { ExpectedResult = Answer };

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"{Date} {Method.Name}";

     string Embedded()
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
        return text switch
        {
            _ when text.EndsWith("\r\n") => text[..^2],
            _ when text.EndsWith('\n') => text[..^1],
            _ => text,
        };
    }

    static AdventDate GetDate(MethodInfo method)
    {
        int[] numbers =[.. method.DeclaringType.FullName.Int32s()];
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
            return answer switch
            {
                int int32 => (Int)int32,
                long int64 => (Int)int64,
                string str => Int.Parse(str),
                _ => answer,
            };
        }
        else if (type == typeof(string) && answer is string str) { return str.Trim(); }
        else if (type == typeof(Point) && answer is string point) { return Point.Parse(point); }
        else return answer;
    }
}
