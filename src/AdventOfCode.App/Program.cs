using Advent_of_Code.Rankings;
using System.IO;

namespace Advent_of_Code;

public static class Program
{
    private static readonly int Failure = 1;
    private static readonly int Success = 0;

    public static int Main(string[] args)
    {
        if (args?.Length < 1) { return Usage(); }

        var puzzles = AdventPuzzles.Load();

        if (!AdventDate.TryParse(args[0], out var date))
        {
            return InvalidDay(args[0]); 
        }
        else if (date.SpecifiesYearDay() && !puzzles.Contains(date))
        {
            return Generate(date);
        }
        else if (args.Length == 2 && args[1] == "-rank")
        {
            return Rank(puzzles, date);
        }
        else if (args.Length == 2 && args[1] == "-size")
        {
            return CodeSize(date);
        }
        else
        {
            var matching = puzzles.Matching(date);
            if (matching.Any())
            {
                foreach (var puzzle in matching)
                {
                    puzzle.Run();
                }
                return Success;
            }
            else return NoMethod(date);
        }
    }
    private static int Generate(AdventDate date)
    {
        var location = Templating.Generate(date.Year.Value, date.Day.Value);
        Console.WriteLine($"Template code generated at {location.FullName}");
        return Success;
    }

    private static int Rank(AdventPuzzles puzzles, AdventDate date)
    {
        var sw = Stopwatch.StartNew();
        var total = 0;

        var durations = puzzles.Matching(date)
            .Where(puzzle => !puzzle.Date.Matches(new AdventDate(default, 25, 2)))
            .ToDictionary(puzzle => puzzle, puzzle => new Durations());

        foreach (var puzzle in durations.Keys)
        {
            while (durations[puzzle].Count < 100 && durations[puzzle].Total < TimeSpan.FromSeconds(1))
            {
                durations[puzzle].Add(puzzle.Run(false));
                Console.Write($"\r{total++}: {sw.ElapsedMilliseconds:#,##0} ms");
            }
        }

        var ranking = durations.OrderBy(kvp => kvp.Value.Median).Select(kvp => kvp.Key).ToArray();

        var swapped = true;

        while (swapped)
        {
            swapped = false;
            var first = ranking[0];
            durations[first].Add(first.Run(false));
            Console.Write($"\r{total++}: {sw.ElapsedMilliseconds:#,##0} ms");

            for (var pos = 1; pos < ranking.Length; pos++)
            {
                var p0 = ranking[pos - 1];
                var p1 = ranking[pos];
                var d0 = durations[p0];
                var d1 = durations[p1];

                if (d0.Median.TotalMilliseconds * 1.15 > d1.Median.TotalMilliseconds)
                {
                    durations[p0].Add(p0.Run(false));
                    Console.Write($"\r{total++}: {sw.ElapsedMilliseconds:#,##0} ms");
                    durations[p1].Add(p1.Run(false));
                    Console.Write($"\r{total++}: {sw.ElapsedMilliseconds:#,##0} ms");

                    if (durations[p0].Median > durations[p1].Median)
                    {
                        swapped = true;
                        ranking[pos - 1] = p1;
                        ranking[pos - 0] = p0;
                    }
                }
            }
        }

        Console.WriteLine();
        for (var pos = 0; pos < ranking.Length; pos++)
        {
            var puzzle = ranking[pos];
            var ds = durations[puzzle];
            Console.WriteLine($"{(pos + 1),3} {ds}, {puzzle}");
        }
        return Success;
    }


    private static int CodeSize(AdventDate date)
    {
        var files = AdventDate.AllAvailable()
            .Where(d => d.Part == 1 && d.Matches(date))
            .Select(d => new CodeFile(
                Location: new(Path.Combine($"./../../../../AdventOfCode/{d.Year}/Day_{d.Day:00}.cs")),
                Date: new AdventDate(d.Year, d.Day, null)))
            .Where(code => code.Exists)
            .OrderBy(code => code.LoC)
            .ThenBy(code=> code.Size)
            .ToArray();

        var rank = 0;

        foreach (var file in files)
        {
            Console.WriteLine($"{++rank,3} {file}");
        }
        return Success;
    }

    private static int Usage()
    {
        Console.WriteLine("usage: advent-of-code [year-day-part]");
        return Failure;
    }

    private static int InvalidDay(string arg)
    {
        Console.WriteLine($"'{arg}' is not a valid advent day");
        return Failure;
    }

    private static int NoMethod(AdventDate date)
    {
        Console.WriteLine($"No test method found for {date}");
        return Failure;
    }
}
