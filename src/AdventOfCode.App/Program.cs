using SmartAss.Parsing;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Advent_of_Code
{
    public static class Program
    {
        private static readonly int Failure = 1;
        private static readonly int Success = 0;

        public static int Main(string[] args)
        {
            if (args?.Length < 1) { return Usage(); }
            if (!AdventDay(args[0], out var year, out var day, out var part)) { return InvalidDay(args[0]); }
            if (!TestClass(year, day, out var testClass)) { return Generate(year, day); }
            if (!TestMethod(testClass, part, out var methodInfo)) { return NoMethod(year, day, part); }

            try
            {
                var input = Puzzle.Input(year, day);
                var test = Activator.CreateInstance(testClass);
                var stopWatch = Stopwatch.StartNew();
                var answer = methodInfo.Invoke(test, new object[] { input });
                stopWatch.Stop();
                Console.WriteLine($"answer: {answer}");
                Console.WriteLine($"duration: {stopWatch.Elapsed.TotalMilliseconds:0.000} ms ({stopWatch.ElapsedTicks:#,##0} ticks)");
            }
            catch(TargetInvocationException x)
            {
                if (x.InnerException is NoAnswer noAnswer)
                {
                    Console.WriteLine(noAnswer.Message);
                    return Failure;
                }
                else throw x.InnerException;
            }
            return Success;
        }

        private static int NoMethod(int year, int day, int part)
        {
            Console.WriteLine($"No test method found for {year}-{day:00} part {part}");
            return Failure;
        }

        private static bool AdventDay(string arg, out int year, out int day, out int part)
        {
            day = default;
            part = default;

            var args = arg.Seperate('-').ToArray();
            if (!int.TryParse(args[0], out year) ||
               !int.TryParse(args[1], out day) ||
               year < 2015 ||
               day < 0 || day > 25)
            {
                return false;
            }
            else if (args.Length == 2) { return true; }
            else if (args.Length == 3)
            {
                switch (args[2].ToUpperInvariant())
                {
                    case "1":
                    case "ONE": part = 1; return true;
                    case "2":
                    case "TWO": part = 2; return true;
                    default: return false;
                }
            }
            else { return false; }
        }

        private static bool TestClass(int year, int day, out Type testClass)
        {
            var fullName = $"Advent_of_Code_{year}.Day_{day:00}";
            testClass = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetExportedTypes())
                .FirstOrDefault(tp => tp.FullName == fullName);
            return !(testClass is null);
        }

        private static bool TestMethod(Type testClass, int part, out MethodInfo tesMethod)
        {
            tesMethod = testClass.GetMethod(part == 1 ? "part_one" : "part_two");
            return !(tesMethod is null);
        }

        private static int Generate(int year, int day)
        {
            var templating = new Templating();
            var location = templating.Generate(year, day);
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
    }
}
