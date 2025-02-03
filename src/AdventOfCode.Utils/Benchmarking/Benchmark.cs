using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace Advent_of_Code.Benchmarking;

public class Benchmark
{
    protected Benchmark() { }

    public AdventPuzzle Puzzle { get; protected init; }

    public override string ToString() => $"Benchmark {Puzzle.Date}";

    public static IEnumerable<BenchmarkResult> Run(IEnumerable<AdventPuzzle> puzzles)
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(BenchmarkDotNet.Loggers.ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        foreach (var puzzle in puzzles)
        {
            var summary = BenchmarkRunner.Run(Typed(puzzle), config);
            var column = summary.Table.Columns.Single(c => c.Header == "Mean");
            yield return new BenchmarkResult(puzzle, string.Join("; ", column.Content));
        }
    }

    private static Type Typed(AdventPuzzle puzzle)
        => puzzle.Date.Part == 1
        ? typeof(Benchmark_One<>).MakeGenericType(puzzle.Method.DeclaringType)
        : typeof(Benchmark_Two<>).MakeGenericType(puzzle.Method.DeclaringType);
}

[InProcess]
public abstract class Benchmark<T> : Benchmark where T : class, new()
{
    private readonly T Instance = new();

    protected abstract int Part { get; }

    protected Benchmark() => Puzzle = AdventPuzzles.Load([typeof(T)]).Single(p => p.Date.Part == Part);

    [Benchmark]
    public object Solve() => Puzzle.Method.Invoke(Instance, Puzzle.Input);
}

public class Benchmark_One<T> : Benchmark<T> where T : class, new()
{
    protected override int Part => 1;
}

public class Benchmark_Two<T> : Benchmark<T> where T : class, new()
{
    protected override int Part => 2;
}
