namespace Advent_of_Code.Benchmarking;

public sealed class Benchmark(AdventPuzzle puzzle)
{
    private readonly AdventPuzzle Puzzle = puzzle;
    private readonly Durations Durations = new();
    
    public void Run(O limit)
    {
        if (Puzzle.Order >= limit)
        {
            Durations.Add(TimeSpan.FromMicroseconds(Math.Pow(10, (int)puzzle.Order - 3)));
            Log(true);
        }
        else
        {
            while (Durations.Count < 400 && Durations.Total < TimeSpan.FromSeconds(1))
            {
                Durations.Add(puzzle.Run(false));
                Log();
            }
            var o = Durations.Median.O();
            if (Durations.Exists(d => d.O() < o))
            {
                while (Durations.Count < 800 && Durations.Total < TimeSpan.FromSeconds(1.5))
                {
                    Durations.Add(puzzle.Run(false));
                    Log();
                }
            }
            Log(true);
        }
    }

    private void Log(bool final = false)
    {
        Console.Write($"\r{Durations}, {Puzzle}");

        if (final && Durations.Min.O() != Puzzle.Order)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" O = {Durations.Min.O()} not {puzzle.Order}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        if (final)
        {
            Console.WriteLine();
        }
    }
}
