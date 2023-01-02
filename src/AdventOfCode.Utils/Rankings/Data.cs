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
        foreach (var ignore in Ignore())
        {
            if (participants.Values.FirstOrDefault(p => p.Matches(ignore)) is { } remove)
            {
                participants.Remove(remove.Id);
            }
        }
        return participants;
    }

    public static Participants Tjip(int year)
    { 
        TJIP_Ignore.TryGetValue(year, out var exclude);
        exclude ??= Array.Empty<string>();
        var partipants = new Participants(Participants()
            .Where(p
                => p.Value.Boards.Any(b => b.Name == "TJIP")
                && !exclude.Any(name => p.Value.Matches(name))));
        return partipants;
    }

    static readonly Dictionary<int, string[]> TJIP_Ignore = new()
    {
        [2014] = "Paul Antal".Split(';'),
        [2021] = "Martijn van Maasakkers;Ralph Hendriks".Split(';'),
        [2022] = "Paul Antal;Jurgen Heeffer;Baljinnyam Sereeter;Jeff-vD;Ralph Hendriks;Fred Hoogduin;Martijn van Maasakkers".Split(';'),
    };

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
    static IEnumerable<string> Ignore()
    {
        using var reader = new StreamReader(Path.Combine(Location.FullName, "ignore.txt"));
        return reader.ReadToEnd().Lines();
    }

    public static IEnumerable<RankingFile> RankingFiles()
        => AdventDate
            .AllAvailable()
            .Select(d => d.Year.Value)
            .Distinct()
            .SelectMany(year => Boards().Values
                .Select(board => new RankingFile(year, board)));

    public static async Task<IEnumerable<RankingFile>> DownloadRankingFiles()
    {
        var updated = new List<RankingFile>();
        var session = Session();

        foreach (var file in RankingFiles().Where(f => f.MayCheckForUpdate))
        {
            var response = await AocClient.GetAsync(file.Remote, KeyValuePair.Create(nameof(session), session));
            if (response.IsSuccessStatusCode)
            {
                if (!file.Local.Directory.Exists) { file.Local.Directory.Create(); }
                var stream = await response.Content.ReadAsStreamAsync();
                try
                {
                    using var local = new FileStream(file.Local.FullName, new FileStreamOptions { Access = FileAccess.Write, Mode = FileMode.Create });
                    var json = JsonDocument.Parse(stream);
                    var writer = new Utf8JsonWriter(local, new JsonWriterOptions() { Indented = true });
                    json.WriteTo(writer);
                    writer.Flush();
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
        return updated;

        static string Session()
        {
            using var reader = new StreamReader(Path.Combine(Location.FullName, "cookie.session.txt"));
            return reader.ReadLine();
        }
    }

    public static DirectoryInfo Location { get; set; } = new(Path.Combine(typeof(Data).Assembly.Location, "../../../../Rankings/Data"));
}
