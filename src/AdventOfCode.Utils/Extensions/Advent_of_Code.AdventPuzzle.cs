using System.Reflection;

namespace Advent_of_Code;

public static class AdventPuzzleExtensions
{

    public static TimeSpan Run(this AdventPuzzle puzzle, bool log = true)
    {
        var test = Activator.CreateInstance(puzzle.Method.DeclaringType);

        object answer = null;
        var sw = new Stopwatch();
        try
        {
            sw.Start();
            answer = puzzle.Method.Invoke(test, puzzle.Input);
            sw.Stop();
        }
        catch (TargetInvocationException x)
        {
            if (x.InnerException is NoAnswer) Do.Nothing();
            else throw x.InnerException;
        }

        if (log)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(puzzle.Date);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.White;
            if (answer is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("no answer");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(answer);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(", ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;

            if (answer != null)
            {
                Console.WriteLine($"duration: {sw.Elapsed.Formatted()}");
            }
        }
        return sw.Elapsed;
    }
}
