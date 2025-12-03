namespace Advent_of_Code;

public static class Templating
{
    private static readonly DirectoryInfo Root = new(Path.Combine(typeof(Templating).Assembly.Location, "../../../../../"));

    public static DirectoryInfo Generate(int year, int day)
    {
        Console.WriteLine(Root.FullName);
        var file = new FileInfo(Path.Combine(Root.FullName, $"AdventOfCode.Now/{year}/Day_{day:00}.cs"));
        var input = new FileInfo(Path.Combine(Root.FullName, $"AdventOfCode.Now/{year}/Day_{day:00}.txt"));
        var example = new FileInfo(Path.Combine(Root.FullName, $"AdventOfCode.Now/{year}/Day_{day:00}_1.txt"));

        if (!file.Directory.Exists)
        {
            file.Directory.Create();
        }
        if (!file.Exists)
        {
            using var writer = new StreamWriter(file.FullName);
            writer.Write(Template(year, day));
        }
        if (!input.Exists)
        {
            using var writer = new StreamWriter(input.FullName);
        }
        if (!input.Exists)
        {
            using var writer = new StreamWriter(example.FullName);
        }
        return file.Directory;
    }

    private static string Template(int year, int day)
    {
        var path = "Advent_of_Code.Template.Day.cs";

        using var stream = typeof(Templating).Assembly.GetManifestResourceStream(path)
            ?? throw new FileNotFoundException(path);

        var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd()
            .Replace("@Year", year.ToString())
            .Replace("@Day", day.ToString("00"));
    }
}
