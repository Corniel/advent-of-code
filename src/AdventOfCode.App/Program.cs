using Advent_of_Code.Benchmarking;
using Advent_of_Code.Rankings;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code;

public static class Program
{
    private static readonly int Failure = 1;
    private static readonly int Success = 0;

    public static async Task<int> Main(string[] args)
    {
        if (args?.Length < 1) { return Usage(); }

        Console.SetOut(new MultiWriter(Console.Out));

        var puzzles = AdventPuzzles.Load(typeof(Now.Dummy).Assembly);

        if (!AdventDate.TryParse(args[0], out var date))
        {
            return InvalidDay(args[0]);
        }
        else if (date.SpecifiesYearDay() && !puzzles.Contains(date))
        {
            return Generate(date);
        }
        else if (args.Length == 2 && (args[1] == "-benchmark" || args[1] == "-b"))
        {
            return Benchmark(puzzles, date);
        }
        else if (args.Length == 2 && args[1] == "-loc")
        {
            return CodeSize(date);
        }
        else if (args.Length >= 2 && (args[1] == "-rank" || args[1] == "-r"))
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
        foreach (var benchmark in puzzles
            .Matching(date)
            .Where(puzzle => !puzzle.Date.Matches(new AdventDate(default, 25, 2)))
            .Select(puzzle => new Benchmark(puzzle)))
        {
            benchmark.Run(O.s);
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
            .ToArray();

        foreach (var file in files)
        {
            Console.WriteLine(file);
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

    private sealed class MultiWriter(TextWriter writer) : TextWriter
    {
        private readonly TextWriter Writer = writer;
        private readonly StreamWriter Stream = new(@"C:\TEMP\aoc.log");

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char[] buffer)
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
}
