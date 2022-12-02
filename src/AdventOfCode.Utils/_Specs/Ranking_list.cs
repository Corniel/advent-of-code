using Advent_of_Code.Rankings;
using System.Threading.Tasks;

namespace Specs.Utils.Ranking_list;

public class Rankings
{
    [Test]
    public void Overall()
    {
        foreach (var rank in Ranking.Overall(Data.Participants()))
        {
            Console.WriteLine(rank);
        }
    }

    [TestCaseSource(nameof(Years))]
    public void Default(int year)
    {
        foreach (var rank in Ranking.Default(Data.Participants(), year))
        {
            Console.WriteLine(rank);
        }
    }

    [TestCaseSource(nameof(Years))]
    public void TJIP(int year)
    {
        TJIP_Ignore.TryGetValue(year, out var exclude);
        exclude ??= Array.Empty<string>();
        var partipants = new Participants(Data.Participants()
            .Where(p 
                => p.Value.Boards.Any(b => b.Name == "TJIP") 
                && !exclude.Any(name => p.Value.Matches(name))));

        foreach (var rank in Ranking.Default(partipants, year))
        {
            Console.WriteLine(rank.ToString("name-only", CultureInfo.CurrentCulture));
        }
    }
    private static readonly Dictionary<int, string[]> TJIP_Ignore = new()
    {
        [2022] = "Paul Antal;Jurgen Heeffer;Baljinnyam Sereeter;Jeff-vD;Ralph Hendriks;Fred Hoogduin;Martijn van Maasakkers".Split(';'),
    };

    [Test]
    public async Task Update_ranking_files()
    {
        foreach (var file in await Data.DownloadRankingFiles())
        {
            Console.WriteLine(file);
        }
    }

    public static IEnumerable<int> Years
        => Data.RankingFiles()
        .Where(f => f.Local.Exists)
        .Select(f => f.Year)
        .Distinct();
}
