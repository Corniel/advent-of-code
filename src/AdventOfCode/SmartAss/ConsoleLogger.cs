﻿using System.Globalization;

namespace SmartAss;

/// <summary>Simplifies the usage of <see cref="Console.WriteLine"/>.</summary>
/// <remarks>
/// All run with <see cref="CultureInfo.InvariantCulture"/>.
/// </remarks>
public static class ConsoleLogger
{
    public static Grid<bool> Console(this Grid<bool> grid)
    {
        WriteLine(grid?.ToString(c => c ? '█' : '░'));
        return grid;
    }

    public static Grid<char> Console(this Grid<char> grid)
    {
        WriteLine(grid?.ToString(c => c));
        return grid;
    }

    public static Grid<int> Console(this Grid<int> grid)
    {
        WriteLine(grid?.ToString(c => c.ToString()[0]));
        return grid;
    }

    public static HashSet<Point> Console(this HashSet<Point> grid)
    {
        var min = new Point(int.MaxValue,int.MaxValue);
        var max = new Point(int.MinValue, int.MinValue);

        foreach (var point in grid)
        {
            if (point.X < min.X) min = new(point.X, min.Y);
            else if (point.X > max.X) max = new(point.X, max.Y);

            if (point.Y < min.Y) min = new(min.X, point.Y);
            else if (point.Y > max.Y) max = new(max.X, point.Y);
        }
        var offset = Point.O - min;
        max += offset;

        var map = new Grid<bool>(max);

        foreach (var point in grid)
        {
            map[point + offset] = true;
        }

        WriteLine();
        if (offset != Vector.O) WriteLine($"Offset: {offset}");
        map.Console();

        return grid;
    }

    public static List<T> Console<T>(this List<T> list, string seperator = ", ")
    {
        WriteLine(string.Join(seperator, list));
        return list;
    }

    public static T Console<T>(this T obj)
    {
        WriteLine(obj?.ToString() ?? "{null}");
        return obj;
    }

    static void WriteLine(object obj = null) 
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();
        System.Console.WriteLine(obj);
        using var writer = new StreamWriter("c:/TEMP/aoc.log", true);
        writer.WriteLine(obj);
        writer.Flush();
    }
}