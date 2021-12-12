using System.Reflection;

namespace Advent_of_Code;

public static class Puzzle
{
    public static string Input(int year, int day, int? example = null, Assembly assembly = null)
    {
        assembly ??= typeof(PuzzleAttribute).Assembly;
        var path = $"Advent_of_Code._{year}.Day_{day:00}{(example.HasValue ? $"_{example}" : "")}.txt";
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream is null) return new FileNotFoundException(path).ToString();
        var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
