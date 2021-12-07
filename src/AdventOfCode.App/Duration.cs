namespace Advent_of_Code;

internal static class Duration
{
    public static string Formatted(this TimeSpan ts)
    {
        if (ts.Ticks < 10_000)
        {
            return $"{(ts.Ticks / 10m):#,##0.0} μs";
        }
        else if(ts.TotalMilliseconds < 10)
        {
            return $"{(ts.TotalMilliseconds):#,##0.000} ms";
        }
        else if (ts.TotalMilliseconds < 100)
        {
            return $"{(ts.TotalMilliseconds):#,##0.00} ms";
        }
        else if (ts.TotalMilliseconds < 1000)
        {
            return $"{(ts.TotalMilliseconds):#,##0.0} ms";
        }
        else if (ts.TotalSeconds < 10)
        {
            return $"{(ts.TotalSeconds):#,##0.000} s";
        }
        else return $"{(ts.TotalSeconds):#,##0.00} s";
    }
}
