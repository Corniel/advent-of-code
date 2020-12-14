using System;
using System.Linq;

namespace Advent_of_Code
{
    public static class Program
    {
        private static readonly int Failure = 1;
        private static readonly int Success = 0;

        public static int Main(string[] args)
        {
            if (args?.Length < 1) { return Usage(); }

            var puzzles = AdventPuzzles.Load();

            if (!AdventDate.TryParse(args[0], out var date)) { return InvalidDay(args[0]); }

            if (date.SpecifiesYearDay() && !puzzles.Contains(date))
            {
                return Generate(date);
            }
            else
            {
                var matching = puzzles.Matching(date);
                if (matching.Any())
                {

                    foreach (var puzzle in puzzles.Matching(date))
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
            var templating = new Templating();
            var location = templating.Generate(date.Year.Value, date.Day.Value);
            Console.WriteLine($"Template code generated at {location.FullName}");
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
}
