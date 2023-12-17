using Advent_of_Code.Diagnostics;
using Advent_of_Code.Rankings;
using SmartAss.Collections;
using System.ComponentModel.DataAnnotations;
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
    private static readonly FileInfo Solving_md = new("./../../../../../Solving.md");

    [Test]
    public void Durations_per_puzzle()
    {
        var puzzles = AdventPuzzles.Load().Where(p => !p.Date.Matches(new AdventDate(null, 25, 2))).ToArray();

        var distribution = new ItemCounter<O> { puzzles.Select(p => p.Order) };
        var factor = 40d / distribution.Max().Count;

        var sb = new StringBuilder();

        sb.AppendLine("|   Order |   # | Chart                                              |");
        sb.AppendLine("|--------:|----:|:---------------------------------------------------|");

        foreach (var dis in distribution.OrderBy(c => c.Item))
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

        var distribution = new ItemCounter<int> { days.Select(p => (p.LoC * 0.1).Ceil()) };
        var factor = 40d / distribution.Max().Count;
        var max = distribution.Keys.Max();

        var sb = new StringBuilder();

        sb.AppendLine("|  LoC |   # | Chart  |");
        sb.AppendLine("|-----:|----:|:-------|");

        for (var loc = 1; loc <= max; loc++)
        {
            var dis = distribution[loc];
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

    [TestCase("Corniel")]
    public void Solved_within(string player)
    {
        var participant = Data.Participants().Search(player);

        var solutions = participant.Solutions
            .Where(x => x.Key.Day == 25 ? x.Key.Part == 1 : x.Key.Part == 2)
            .OrderBy(x => x.Key)
            .ToArray();

        var distribution = new ItemCounter<SolvingType> { solutions.Select(kvp => GetSolvingType(kvp.Value - kvp.Key.AvailableFrom)) };
        var factor = 40d / distribution.Max().Count;

        var sb = new StringBuilder();

        sb.AppendLine("|   Order |   # | Chart                                              |");
        sb.AppendLine("|--------:|----:|:---------------------------------------------------|");

        foreach (var dis in distribution.OrderBy(c => c.Item))
        {
            var bar = new string('⭐', (dis.Count * factor).Ceil());

            sb.AppendLine($"| {Formatted(dis.Item).Replace(' ', (char)160),7} | {dis.Count,3} | {bar} |");
        }

        sb.AppendLine();
        sb.AppendLine("|   Day   |   Solved |");
        sb.AppendLine("|:-------:|---------:|");

        foreach (var kvp in solutions)
        {
            if (kvp.Key.Day == 25 ? kvp.Key.Part == 1 : kvp.Key.Part == 2)
            {
                var d = kvp.Value - kvp.Key.AvailableFrom;

                var display = d.TotalHours >= 1 ? $"{d.TotalHours:0}:{d.Minutes:00}:{d.Seconds:00}" : $"{d.Minutes:0}:{d.Seconds:00}";

                if (d > TimeSpan.FromDays(340))
                {
                    display = $"{d.TotalDays / 365:0} year";
                }
                else if (d > TimeSpan.FromDays(1))
                {
                    display = $"{d.TotalDays.Ceil()} days";
                }
                sb.AppendLine($"| {kvp.Key.YearDay} | {display,8} |");
            }
        }

        using var writer = new StreamWriter(Solving_md.FullName, new FileStreamOptions
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
        });

        writer.Write(sb);

        sb.Console();
    }

    static SolvingType GetSolvingType(TimeSpan t) => Types.First(type => t.TotalSeconds < (int)type);

    static string Formatted(SolvingType t)
    {
        return typeof(SolvingType).GetField(t.ToString()).GetCustomAttribute<DisplayAttribute>().Name;
    }

    enum SolvingType
    {
        [Display(Name = "< 15m")]
        Quarter = 15 * 60,
        [Display(Name = "< 30m")]
        HalfHour = 30 * 60,
        [Display(Name = "< 60m")]
        Hour = 60 * 60,
        [Display(Name = "< 2h")]
        TwoHour = 2 * 60 * 60,
        [Display(Name = "< 4h")]
        FourHour = 4 * 60 * 60,
        [Display(Name = "< 8h")]
        EightHour = 8 * 60 * 60,
        [Display(Name = "< 1d")]
        Day = 24 * 60 * 60,
        [Display(Name = "< 2d")]
        TwoDay = 2 * 24 * 60 * 60,
        [Display(Name = "< 1w")]
        Week = 7 * 24 * 60 * 60,
        [Display(Name = "< 1y")]
        Year = 340 * 24 * 60 * 60,
        [Display(Name = "> 1y")]
        Other = int.MaxValue,
    }

    static readonly SolvingType[] Types = Enum.GetValues<SolvingType>().Order().ToArray();
}
