using Advent_of_Code.Http;
using Advent_of_Code.Rankings;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Qowaiv.IO;

namespace Specs.Leaderboard_specs;

public class Leaderboard
{
    public static IEnumerable<int> Years
       => Data.RankingFiles()
       .Where(f => f.Local.Exists)
       .Select(f => f.Year)
        .Where(y => y >= 2017)
       .Distinct();

    static FileInfo FileLocation(AdventDate date) => new(Path.Combine(Data.Location.FullName, $"{date.Year}/day_{date.Day:00}_leaderboard.html"));

    [TestCase("Corniel")]
    public void TODO_list(string name)
    {
        var entries = FetchEntries(new(null, null, 2));
        var participant = Data.Participants().Search(name)!;

        var sovled = participant.Solutions.Select(s => s.Key.YearDay).ToHashSet();

        foreach (var entry in entries.Where(e => !sovled.Contains(e.Date.YearDay)) .OrderBy(e => e.Time))
        {
            $"- [ ] {entry.Date.Year}-{entry.Date.Day:00} {entry.Time.TotalMinutes,3:0}:{entry.Time.Seconds:00}".Console();
        }
    }

    [Test]
    public void Fetch_all() => Fetch(new AdventDate(null, null, 2));

    [TestCaseSource(nameof(Years))]
    public void Fetch_100(int year) => Fetch(new AdventDate(year, null, 2));

    [Test]
    public static Task Download_missing() => Task.WhenAll(AdventDate.AllAvailable().Where(d => d.Part == 1).Select(Download).ToArray());

    static void Fetch(AdventDate filter)
    {
        var enties = FetchEntries(filter).Where(d => d.Date.Year >= 2017).ToArray();
        var factor = 50d / enties.Max(e => e.Time.Ticks);

        foreach (var entry in enties)
        {
            var n = (entry.Time.Ticks * factor).Ceil();

            var stars = new string('*', n);

            $"{entry.Date.Year}\t{entry.Date.Day}\t{entry.Date.Part}\t{entry.Pos}\t{entry.Time}\t{stars}".Console();
        }
    }

    private static IReadOnlyCollection<LeaderboardEntry> FetchEntries(AdventDate filter)
    {
        var enties = new List<LeaderboardEntry>();

        foreach (var date in AdventDate.AllAvailable().Where(d => d.Matches(filter)))
        {
            using var reader = FileLocation(date).OpenText();
            enties.AddRange(LeaderboardEntry.Read(date.Year.Value, reader.ReadToEnd())
                .Where(e => e.Date.Matches(filter) && e.Pos == 100));
        }

        return enties;
    }

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

