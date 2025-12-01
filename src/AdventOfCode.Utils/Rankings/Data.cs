using Advent_of_Code.Http;
using System.Text.Json;

namespace Advent_of_Code.Rankings;

public static class Data
{
    public static Participants Participants()
    {
        var participants = new Participants();
        var aliases = Aliases();

        foreach (var file in RankingFiles().Where(file => file.Local.Exists))
        {
            using var stream = file.OpenRead();
            var members = JsonSerializer.Deserialize<Json.RankingList>(stream);

            foreach (var member in members.Members.Values)
            {
                if (!participants.TryGetValue(member.Id, out var participant))
                {
                    _ = aliases.TryGetValue(member.Name?.Trim() ?? string.Empty, out var alias)
                        || aliases.TryGetValue(member.Id.ToString(), out alias);

                    participant = new(member.Id, member.Name ?? $"#{member.Id}", alias);
                    participants[member.Id] = participant;
                }
                participant.Boards.Add(file.Board);
                foreach (var day in member.Days)
                {
                    participant.Solutions[new AdventDate(file.Year, day.Key, 1)] = day.Value.One.Time;
                    if (day.Value.Two is { })
                    {
                        participant.Solutions[new AdventDate(file.Year, day.Key, 2)] = day.Value.Two.Time;
                    }
                }
            }
        }
        
        return participants;
    }

    public static Participants Tweakers() => Custom("Tweakers", 0);

    public static Participants Custom(string name, int year)
    {
        var partipants = Participants();

        var file = new FileInfo(Path.Combine(Location.FullName, "boards", $"{name}_{year}.txt"));

        if (file.Exists)
        {
            var custom = CustomBoard.Load(file, partipants);
            return custom.Participants;
        }
        else
        {
            name = name.TrimEnd('*');
            return new Participants(partipants
                .Where(p => p.Value.Boards.Any(b => b.Name == name)));
        }
    }

    static Dictionary<int, Board> Boards()
    {
        using var reader = new StreamReader(Path.Combine(Location.FullName, "boards.txt"));
        var boards = new Dictionary<int, Board>();
        foreach (var split in reader.ReadToEnd().Lines(line => line.Separate(':')))
        {
            boards[split[0].Int32()] = new Board(split[0].Int32(), split[1].Trim());
        }
        return boards;
    }

    static Dictionary<string, string> Aliases()
    {
        using var reader = new StreamReader(Path.Combine(Location.FullName, "aliases.txt"));
        var aliases = new Dictionary<string, string>();
        foreach (var split in reader.ReadToEnd().Lines(line => line.Separate(':')))
        {
            aliases[split[0]] = split[1].Trim();
        }
        return aliases;
    }

    public static IEnumerable<RankingFile> RankingFiles()
        => AdventDate
            .AllAvailable()
            .Select(d => d.Year.Value)
            .Distinct()
            .SelectMany(year => Boards().Values
                .Select(board => new RankingFile(year, board)));

    public static async Task<IReadOnlyCollection<RankingFile>> DownloadRankingFiles()
    {
        var updated = new List<RankingFile>();
        var session = Session();

        foreach (var file in RankingFiles())
        {
            if (file.MayCheckForUpdate)
            {
                await CheckFroUpdate(updated, session, file);
            }
            else updated.Add(file);
        }
        return updated;

        static async Task CheckFroUpdate(List<RankingFile> updated, string session, RankingFile file)
        {
            var response = await AocClient.GetAsync(file.Remote, KeyValuePair.Create(nameof(session), session));
            if (response.IsSuccessStatusCode)
            {
                if (!file.Local.Directory.Exists) { file.Local.Directory.Create(); }
                var stream = await response.Content.ReadAsStreamAsync();
                try
                {
                    using var local = new FileStream(file.Local.FullName, new FileStreamOptions { Access = FileAccess.Write, Mode = FileMode.Create });
                    var json = await JsonDocument.ParseAsync(stream);
                    var writer = new Utf8JsonWriter(local, new JsonWriterOptions() { Indented = true });
                    json.WriteTo(writer);
                    await writer.FlushAsync();
                    updated.Add(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(file);
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine(file);
                Console.WriteLine($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }

    public static string Session()
    {
        using var reader = new StreamReader(Path.Combine(Location.FullName, "cookie.session.txt"));
        return reader.ReadLine();
    }

    public static DirectoryInfo Location { get; } = new(Path.Combine(typeof(Data).Assembly.Location, "../../../../../AdventOfCode.Utils/Rankings/Data"));
}
