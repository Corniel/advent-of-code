using Advent_of_Code.Benchmarking;

namespace Specs.Benchmarks;

public class Solves
{
    [Test]
    public void day()
    {
        var benchmark = new Benchmark_One<Advent_of_Code_2024.Day_01>();
        benchmark.Solve().Should().Be(1873376);
    }
}
