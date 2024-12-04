using Advent_of_Code.Http;
using Advent_of_Code.Rankings;

namespace Advent_of_Code;

public sealed class PuzzleInput(AdventDate date, DirectoryInfo root)
{
    public AdventDate Date { get; } = date;

    public DirectoryInfo Root { get; } = root;

    public FileInfo Location => new(Path.Combine(Root.FullName, Date.Year.ToString(), $"Day_{Date.Day:00}.txt"));

    public Uri Url => new($"https://adventofcode.com/{Date.Year}/day/{Date.Day}/input");

    public bool ShouldDownload()
    {
        if (!Location.Exists) return true;
        using var reader = Location.OpenText();
        return reader.ReadToEnd().Length < 3;
    }

    public async Task Download()
    {
        var session = Data.Session();

        var response = await AocClient.GetAsync(Url, KeyValuePair.Create(nameof(session), session));

        if (response.IsSuccessStatusCode)
        {
            if (!Location.Directory.Exists) { Location.Directory.Create(); }

            using var stream = Location.Open(FileMode.Create, FileAccess.Write);
            await response.Content.CopyToAsync(stream);

            Console.WriteLine($"Wrote {stream.GetStreamSize():S} to {Location}");
        }
        else
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
