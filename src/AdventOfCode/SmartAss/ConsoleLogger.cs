using System.Globalization;

namespace SmartAss;

/// <summary>Simplifies the usage of <see cref="Console.WriteLine"/>.</summary>
/// <remarks>
/// All run with <see cref="CultureInfo.InvariantCulture"/>.
/// </remarks>
public static class ConsoleLogger
{
    public static Grid<bool> ToConsole(this Grid<bool> grid)
    {
        WriteLine(grid?.ToString(c => c ? '█' : '░'));
        return grid;
    }

    public static Grid<char> ToConsole(this Grid<char> grid)
    {
        WriteLine(grid?.ToString(c => c));
        return grid;
    }

    public static Grid<int> ToConsole(this Grid<int> grid)
    {
        WriteLine(grid?.ToString(c => c.ToString()[0]));
        return grid;
    }

    public static T ToConsole<T>(this T obj)
    {
        WriteLine(obj?.ToString());
        return obj;
    }

    static void WriteLine(object obj) 
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();
        Console.WriteLine(obj ?? "{null}");
    }
}
