using Advent_of_Code.Diagnostics;
using SmartAss.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace Specs.Days;

public class Are
{
    static readonly AdventPuzzles Puzzles = AdventPuzzles.Load();

    [TestCaseSource(nameof(Puzzles))]
    public void Catogized(AdventPuzzle puzzle)
    {
        var categories = puzzle.Method.DeclaringType.GetCustomAttribute<Advent_of_Code.CategoryAttribute>()?.Categories.Where(c => c != default) ?? Array.Empty<Category>();
        categories.Should().NotBeEmpty(because: puzzle.Date.ToString());
    }

    [TestCaseSource(nameof(Puzzles))]
    public void Parameter_matching_type(AdventPuzzle puzzle)
    {
        var parameter = puzzle.Method.GetParameters()[0];
        parameter.Name.Should().Match(Name(parameter.ParameterType), because: puzzle.Date.ToString());

        static string Name(Type type)
        {
            if (type == typeof(string)) return "str";
            else if (type == typeof(Lines)) return "lines";
            else if (type == typeof(GroupedLines)) return "groups";
            else if (type == typeof(CharPixels)) return "chars";
            else if (type == typeof(CharGrid)) return "map";
            else if (type == typeof(Ints) || type == typeof(Longs)) return "numbers";
            else return "*";
        }
    }

    [TestCaseSource(nameof(Puzzles))]
    public void matching_return_type(AdventPuzzle puzzle) 
    {
        ReturnType(puzzle).Should().Be(puzzle.Answer.GetType());

        static Type ReturnType(AdventPuzzle puzzle)
            => puzzle.Method.ReturnType == typeof(char[])
            ? typeof(string)
            : puzzle.Method.ReturnType;
    }
}

public class Reports
{
    private static readonly FileInfo Durations_md = new("./../../../../../Durations.md");
    private static readonly FileInfo LoC_md = new("./../../../../../LoC.md");

    [Test]
    public void Durations_per_puzzle()
    {
        var puzzles = AdventPuzzles.Load().Where(p => !p.Date.Matches(new AdventDate(null, 25, 2))).ToArray();

        var distrubtion = new ItemCounter<O> { puzzles.Select(p => p.Order) };
        var factor = 40d / distrubtion.Max().Count;

        var sb = new StringBuilder();

        sb.AppendLine("|   Order |   # | Chart                                              |");
        sb.AppendLine("|--------:|----:|:---------------------------------------------------|");

        foreach (var dis in distrubtion.OrderBy(c => c.Item))
        {
            var bar = new string('⭐', (dis.Count * factor).Ceil());

            sb.AppendLine($"| {dis.Item.Formatted().Replace(' ', (char)160),7} | {dis.Count,3} | {bar} |");
        }

        sb.AppendLine();
        sb.AppendLine("|  Puzzle   |   Order |");
        sb.AppendLine("|:---------:|--------:|");

        foreach (var puzzle in puzzles)
        {
            sb.AppendLine($"| {puzzle.Date} | {puzzle.Order.Formatted(),7} |");
        }

        using var writer = new StreamWriter(Durations_md.FullName, new FileStreamOptions
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
        });

        writer.Write(sb);
    }


    [Test]
    public void LoC_per_day()
    {
        var days = LinesOfCode.Select(AdventDate.All);

        var distrubtion = new ItemCounter<int> { days.Select(p => (p.LoC *0.1).Ceil()) };
        var factor = 40d / distrubtion.Max().Count;
        var max = distrubtion.Keys.Max();

        var sb = new StringBuilder();

        sb.AppendLine("|  LoC |   # | Chart  |");
        sb.AppendLine("|-----:|----:|:-------|");

        for(var loc = 1; loc <= max; loc++) 
        {
            var dis = distrubtion[loc];
            var bar = new string('⭐', (dis * factor).Ceil());

            sb.AppendLine($"| {loc,3}0 | {dis,3} | {bar} |");
        }

        sb.AppendLine();
        sb.AppendLine("|   Day   | Loc |       Size |");
        sb.AppendLine("|:-------:|----:|-----------:|");

        foreach (var day in days)
        {
            sb.AppendLine($"| {day.Date} | {day.LoC,3} | {day.Size.ToString("0.00 kb", CultureInfo.InvariantCulture),10} |");
        }

        using var writer = new StreamWriter(LoC_md.FullName, new FileStreamOptions
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
        });

        writer.Write(sb);
    }


}
