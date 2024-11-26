using Advent_of_Code.Benchmarking;
using Advent_of_Code.Diagnostics;
using Advent_of_Code.Rankings;
using SmartAss;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code;

public static class Program
{
    private static readonly int Failure = 1;
    private static readonly int Success = 0;
    private static AdventPuzzles Puzzles = new();

    public static async Task<int> Main(string[] args)
    {
        Puzzles = AdventPuzzles.Load(typeof(Now.Dummy).Assembly);

        if (args is not { Length: > 0 }) return Usage();

        var arg0 = args[0];
        args = args[1..];

        if (!AdventDate.TryParse(arg0, out var date)) return InvalidDay(arg0);

        if (Matches.New(date, args)) return Generate(date);

        if (Matches.Benchmark(date, args)) return Benchmarks(Puzzles, date);

        Console.SetOut(new MultiWriter(Console.Out));

        if (Matches.BenchmarkClassic(date, args)) return BenchmarksClassic(date);

        if (Matches.LoC(date, args)) return Loc(date, args);

        if (Matches.Rank(date, args)) return await Rankings(date, args[1..].FirstOrDefault());

        if (Matches.Run(date, args)) return RunPuzzle(date, args);

        return NoMethod(date, args);
    }

    private static async Task<int> Rankings(AdventDate date, string? list)
    {
        var year = date.Year!.Value;
        Data.Location = new DirectoryInfo("../../../../AdventOfCode.Utils/Rankings/Data");

        foreach (var file in await Data.DownloadRankingFiles())
        {
            Console.WriteLine(file);
        }

        var participants = list?.ToUpperInvariant() switch
        {
            "TJIP" => Data.Tjip(year),
            "TWEAKERS" => Data.Tweakers(),
            _ => Data.Participants(),
        };

        Console.WriteLine(Ranking.Solving(year, participants.Values));

        return Success;
    }

    private static int Generate(AdventDate date)
    {
        var location = Templating.Generate(date.Year!.Value, date.Day!.Value);
        Console.WriteLine($"Template code generated at {location.FullName}");
        return Success;
    }

    private static int Benchmarks(AdventPuzzles puzzles, AdventDate date)
    {
        var selection = puzzles
            .Matching(date)
            .Where(puzzle => !puzzle.Date.Matches(new AdventDate(default, 25, 2)))
            .ToArray();

        foreach (var result in Benchmark.Run(selection))
        {
            result.Console();
        }
        return Success;
    }

    private static int BenchmarksClassic(AdventDate date)
    {
        foreach (var benchmark in Puzzles
            .Matching(date)
            .Where(puzzle => !puzzle.Date.Matches(new AdventDate(default, 25, 2)))
            .Select(puzzle => new BenchmarkClassic(puzzle)))
        {
            benchmark.Run(O.s);
        }
        return Success;
    }

    private static int Loc(AdventDate date, string[] _)
    {
        foreach (var file in LinesOfCode.Select(date))
        {
            Console.WriteLine(file);
        }
        return Success;
    }

    private static int RunPuzzle(AdventDate date, string[] _)
    {
        foreach (var puzzle in Puzzles.Matching(date))
        {
            puzzle.Run();
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

    private static int NoMethod(AdventDate date, string[] args)
    {
        Console.Write($"No test method found for {date}");
        if (args.Any())
        {
            Console.Write($" with args {string.Join(' ', args)}");
        }
        Console.WriteLine();
        return Failure;
    }

    private sealed class MultiWriter(TextWriter writer) : TextWriter
    {
        private readonly TextWriter Writer = writer;
        private readonly StreamWriter Stream = new(@"C:\TEMP\aoc.log");

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char[]? buffer)
        {
            Writer.Write(buffer);
            Stream.Write(buffer);
            Stream.Flush();
        }

        public override void Flush()
        {
            Writer.Flush();
            Stream.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stream.Dispose();
            }
        }
    }

    static class Matches
    {
        public static bool New(AdventDate date, string[] _)
            => date.SpecifiesYearDay && !Puzzles.Contains(date);

        public static bool Benchmark(AdventDate _, string[] args)
            => Arg(args, "b", "benchmark");

        public static bool BenchmarkClassic(AdventDate _, string[] args)
            => Arg(args, "bc");

        public static bool LoC(AdventDate _, string[] args) => Arg(args, "LoC");

        public static bool Rank(AdventDate date, string[] args)
            => date.Year.HasValue
            && !date.Day.HasValue
            && !date.Part.HasValue
            && Arg(args, "r", "rank");

        public static bool Run(AdventDate date, string[] args)
            => Puzzles.Matching(date).Any() && args.Length == 0;

        static bool Arg(string[] parameters, params string[] args)
            => parameters.FirstOrDefault() is { } par
            && args
                .Select(arg => $"-{arg}")
                .Any(arg => arg.Equals(par, StringComparison.OrdinalIgnoreCase));
    }
}
