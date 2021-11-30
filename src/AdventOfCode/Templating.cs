namespace Advent_of_Code;

public class Templating
{
    public DirectoryInfo Generate(int year, int day)
    {
        var file = new FileInfo($@".\..\..\..\..\AdventOfCode\{year}\Day_{day:00}.cs");
        var input = new FileInfo($@".\..\..\..\..\AdventOfCode\{year}\Day_{day:00}.txt");

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
            writer.WriteLine();
        }
        return file.Directory;
    }

    private static string Template(int year, int day)
    {
        var path = "Advent_of_Code.Template.Day.cs";
        using var stream = typeof(Templating).Assembly.GetManifestResourceStream(path);
        if (stream is null) throw new FileNotFoundException(path);
        var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd()
            .Replace("@Year", year.ToString())
            .Replace("@Day", day.ToString("00"));
    }
}
