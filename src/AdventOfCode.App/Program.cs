using Advent_of_Code.Rankings;
using System.IO;
using System.Threading.Tasks;

namespace Advent_of_Code;

public static class Program
{
    private static readonly int Failure = 1;
    private static readonly int Success = 0;

    public static async Task<int> Main(string[] args)
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
        else if (args.Length == 2 && args[1] == "-benchmark" || args[1] == "-b")
        {
            return Benchmark(puzzles, date);
        }
        else if (args.Length == 2 && args[1] == "-loc")
        {
            return CodeSize(date);
        }
        else if (args.Length >= 2 && args[1] == "-rank" || args[1] == "-r")
        {
            return await Rankings(date.Year.Value, args.Length == 2 ? "" : args[2]);
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

    private static async Task<int> Rankings(int year, string list)
    {
        Console.Clear();
        Data.Location = new DirectoryInfo("../../../../AdventOfCode.Utils/Rankings/Data");

        foreach (var file in await Data.DownloadRankingFiles())
        {
            Console.WriteLine(file);
        }

        var participants = list.ToUpperInvariant() == "TJIP"
            ? Data.Tjip(year)
            : Data.Participants();

        Console.WriteLine(Ranking.Solving(year, participants.Values));

        return Success;
    }

    private static int Generate(AdventDate date)
    {
        var location = Templating.Generate(date.Year.Value, date.Day.Value);
        Console.WriteLine($"Template code generated at {location.FullName}");
        return Success;
    }

    private static int Benchmark(AdventPuzzles puzzles, AdventDate date)
    {
        var limit = O.s;
        var sw = Stopwatch.StartNew();
        var total = 0;

        var durations = puzzles.Matching(date)
            //.Where(p => p.Order == O.Unknown)
            .Where(puzzle => !puzzle.Date.Matches(new AdventDate(default, 25, 2)))
            .ToDictionary(puzzle => puzzle, puzzle => new Durations());

        foreach (var puzzle in durations.Keys)
        {
            if (puzzle.Order < limit)
            {
                while (durations[puzzle].Count < 100 && durations[puzzle].Total < TimeSpan.FromSeconds(1))
                {
                    durations[puzzle].Add(puzzle.Run(false));
                    Console.Write($"\r{total++}: {sw.ElapsedMilliseconds:#,##0} ms");
                }
            }
            else
            {
                durations[puzzle].Add(TimeSpan.FromMicroseconds(Math.Pow(10, (int)puzzle.Order - 3)));
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

                if (p0.Order >= limit || p1.Order >= limit) continue;

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
            Console.Write($"{(pos + 1),3} {ds}, {puzzle}");
            if (ds.Median.O() != puzzle.Order)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" O = {ds.Median.O()} not {puzzle.Order}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }
        return Success;
    }

    private static int CodeSize(AdventDate date)
    {
        var files = AdventDate.AllAvailable()
            .Where(d => d.Part == 1 && d.Matches(date))
            .Select(d => new LinesOfCode(
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
