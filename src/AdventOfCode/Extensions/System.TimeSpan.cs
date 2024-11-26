namespace System;

public static class AoCTimeSpanExtensions
{
    public static O O(this TimeSpan duration)
        => duration.Ticks == 0
        ? Advent_of_Code.O.ns10
        : (O)Math.Log10(duration.TotalNanoseconds);
}
