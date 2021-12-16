﻿using Advent_of_Code.Rankings;
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
    public void Time(int year)
    {
        foreach (var rank in Ranking.Time(Data.Participants(), year))
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
