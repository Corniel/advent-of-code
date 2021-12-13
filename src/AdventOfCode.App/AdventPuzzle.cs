using System.Reflection;

namespace Advent_of_Code;

public sealed class AdventPuzzle
{
    public AdventPuzzle(AdventDate date, MethodInfo method)
    {
        Date = date;
        Method = method;
        Input = Method.GetCustomAttributes<PuzzleAttribute>().FirstOrDefault(a => a.GetType() == typeof(PuzzleAttribute)).Input;
    }

    public bool Matches(AdventDate date) => Date.Matches(date);

    public AdventDate Date { get; }
    private MethodInfo Method { get; }
    private string Input { get; }

    public TimeSpan Run(bool log = true)
    {
        var test = Activator.CreateInstance(Method.DeclaringType);

        object answer = null;
        var sw = new Stopwatch();
        try
        {
            sw.Start();
            answer = Method.Invoke(test, new object[] { Input });
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
            Console.Write(Date);
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

    public override string ToString() => $"{Date} {Method.Name}";
}
