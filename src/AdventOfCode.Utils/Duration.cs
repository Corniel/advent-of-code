namespace Advent_of_Code;

internal static class Duration
{
    public static string Formatted(this O o) => o switch
    {
        O.ns10 => "10 ns",
        O.ns100 => "100 ns",
        O.μs => "1 μs",
        O.μs10 => "10 μs",
        O.μs100 => "100 μs",
        O.ms =>   "1 ms",
        O.ms10 => "10 ms",
        O.ms100 =>"100 ms",
        O.s => "1 sec",
        O.s10 => "10 sec",
        O.s100 => "100 sec",
        O.s1000 => "1000 sec",
        _ => "?",
    };

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

    public static TimeSpan? TryParse(string str)
    {
        if (str.Split(' ') is { Length: 2} parts 
            && double.TryParse(parts[0], CultureInfo.InvariantCulture, out var number))
        {
            return parts[1] switch
            {
                "ns" => TimeSpan.FromMicroseconds(number / 1000),
                "μs" or "μs" => TimeSpan.FromMicroseconds(number),
                "ms" => TimeSpan.FromMilliseconds(number),
                "s" => TimeSpan.FromSeconds(number),
                _ => null,
            };
        }
        else return null;
    }
}
