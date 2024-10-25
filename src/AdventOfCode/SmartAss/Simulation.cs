#nullable enable

namespace SmartAss;

public static class Simulation
{
    /// <remarks>
    /// The end state is after the first repetition.
    /// </remarks>
    public static SimulationPeriod WithPeriod<TState, THash>(
        TState initial,
        Func<TState, long, TState> simulate,
        Func<TState, THash?> getHash,
        long simulations,
        out TState repetition) where THash : notnull
    {
        repetition = simulate(initial, 0);
        var lookup = new Dictionary<THash, long> { [getHash(repetition)!] = 0 };

        for (long simulation = 1; simulation < simulations; simulation++)
        {
            repetition = simulate(repetition, simulation);
            if (getHash(repetition) is { } hash && !lookup.TryAdd(hash, simulation))
            {
                var first = lookup[hash];
                var period = simulation - first;
                var periods = (simulations - first) / period;
                var remaining = simulations - periods * period - first - 1;

                return new SimulationPeriod(
                    Offset: first,
                    Period: period,
                    Periods: periods,
                    Remaining: remaining);
            }
        }
        return default;
    }
}

public record struct SimulationPeriod(long Offset, long Period, long Periods, long Remaining);
