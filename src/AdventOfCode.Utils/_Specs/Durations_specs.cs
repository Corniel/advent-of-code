using SmartAss.Collections;

namespace Specs.Durations_specs;

public class Reports
{
    private static readonly FileInfo MarkDown = new("./../../../../../Durations.md");

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

            sb.AppendLine($"| {dis.Item.Formatted(),7} | {dis.Count,3} | {bar,-50} |");
        }

        sb.AppendLine();
        sb.AppendLine("|  Puzzle   |   Order |");
        sb.AppendLine("|:---------:|--------:|");

        foreach (var puzzle in puzzles)
        {
            sb.AppendLine($"| {puzzle.Date} | {puzzle.Order.Formatted(),7} |");
        }

        using var writer = new StreamWriter(MarkDown.FullName, new FileStreamOptions
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
        });

        writer.Write(sb);
    }
}
