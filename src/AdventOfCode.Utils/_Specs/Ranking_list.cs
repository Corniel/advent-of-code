using Advent_of_Code.Rankings;

namespace Specs.Ranking_list; 

public class Rankings 
{
    [TestCaseSource(nameof(Years))]
    public void All(int year)
    {
        Ranking.Solving(year, Data.Participants().Values).Console();
    }

    [TestCaseSource(nameof(Years))]
    public void TJIP(int year)
    {
        var drieKoningen = new DateTime(year + 1, 01, 06, 05, 00, 00, DateTimeKind.Utc);
        Ranking.Solving(year, Data.Tjip(year).Values, drieKoningen).Console();
    }

    [Test]
    public void Overall()
    {
        foreach (var rank in Ranking.Overall(Data.Participants()))
        {
            Console.WriteLine(rank);
        }
    }

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
