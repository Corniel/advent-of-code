using Advent_of_Code.Http;
using Advent_of_Code.Rankings.Json;
using Qowaiv.IO;
using System.Text.RegularExpressions;

namespace Advent_of_Code.Rankings;


public sealed partial record LeaderboardEntry(AdventDate Date, int Pos, TimeSpan Time)
{
    public override string ToString() => $"{Date}, {Pos,3}) {Time.Hours:0}:{Time.Minutes:00}:{Time.Seconds:00}";
    public static IEnumerable<LeaderboardEntry> Read(int year, string text)
    {
        var part = 2;
        var matches = Entry().Matches(text);

        foreach (var match in matches.Cast<Match>())
        {
            if (match.Success && TimeSpan.TryParse(match.Groups[nameof(Time)].Value, out var time))
            {
                var day = match.Groups[nameof(Day)].Value.Int32();
                var pos = match.Groups[nameof(Pos)].Value.Int32();

                yield return new(new AdventDate(year, day, part), pos, time);

                if (pos == 100) part--;
            }
        }
    }

    [GeneratedRegex(">\\s*(?<Pos>[1-9][0-9]{0,2})\\)</span>.+?time\">Dec (?<Day>[0-2][0-9])\\s+(?<Time>[0-9][0-9]:[0-9][0-9]:[0-9][0-9])</span>")]
    private static partial Regex Entry();
}

public class Leaderboard
{
    static FileInfo FileLocation(AdventDate date) => new(Path.Combine(Data.Location.FullName, $"{date.Year}/day_{date.Day:00}_leaderboard.html"));

    [Test]
    public void Fetch_100_2022()
    {
        var reference = new AdventDate(2022, null, 2);

        var enties = new List<LeaderboardEntry>();

        foreach (var date in AdventDate.AllAvailable().Where(d => d.Matches(reference)))
        {
            using var reader = FileLocation(date).OpenText();
            enties.AddRange(LeaderboardEntry.Read(date.Year.Value, reader.ReadToEnd())
                .Where(e => e.Date.Matches(reference) && e.Pos == 100));
        }
        var factor = 42d / enties.Max(e => e.Time.Ticks);

        foreach(var entry in enties)
        {
            var stars = new string('*', (int)(entry.Time.Ticks * factor));

            $"{entry.Date.Year}\t{entry.Date.Day}\t{entry.Date.Part}\t{entry.Pos}\t{entry.Time}\t{stars}".Console();
        }
    }

    [Test]
    public void FetchAll()
    {
        foreach (var date in AdventDate.AllAvailable().Where(d => d.Part == 1))
        {
            using var reader = FileLocation(date).OpenText();
            foreach (var entry in LeaderboardEntry.Read(date.Year.Value, reader.ReadToEnd()))
            {
                $"{entry.Date.Year}\t{entry.Date.Day}\t{entry.Date.Part}\t{entry.Pos}\t{entry.Time}".Console();
            }
        }
    }

    [Test]
    public void Get_2021()
    {
        using var reader = FileLocation(new AdventDate(2021, 23, null)).OpenText();
        var lines = LeaderboardEntry.Read(2021, reader.ReadToEnd()).ToArray();
        lines.Should().HaveCount(200);
    }

    [Test]
    public static Task Download() => Task.WhenAll(AdventDate.AllAvailable().Where(d => d.Part == 1).Select(Download).ToArray());

    static async Task Download(AdventDate date)
    {
        var file = FileLocation(date);
        var url = new Uri($"https://adventofcode.com/{date.Year}/leaderboard/day/{date.Day}");

        if (!file.Exists || file.GetStreamSize() < 1.KB())
        {
            Console.WriteLine($"Year: {date.Year}, Day: {date.Day:00}, download leaderboard");

            var response = await AocClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                using var stream = file.OpenWrite();
                await response.Content.CopyToAsync(stream);
            }
        }
    }
}
