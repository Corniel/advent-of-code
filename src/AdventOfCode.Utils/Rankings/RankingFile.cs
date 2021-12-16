using Qowaiv;

namespace Advent_of_Code.Rankings;

public record RankingFile(int Year, Board Board)
{
    public FileInfo Local
        => new(Path.Combine(
            typeof(RankingFile).Assembly.Location,
            $"../../../../Rankings/Data/{Year}/{Board.Id}.json"));

    public Uri Remote => new($"https://adventofcode.com/{Year}/leaderboard/private/view/{Board.Id}.json");

    public Stream OpenRead() => Local.OpenRead();

    public bool MayCheckForUpdate
        => !Local.Exists 
        || Year == Clock.Today().Year
            ? Clock.UtcNow() - Local.LastWriteTimeUtc > TimeSpan.FromMinutes(15)
            : Clock.UtcNow() - Local.LastWriteTimeUtc > TimeSpan.FromDays(1);

    public override string ToString() => $"Year: {Year}, Board: {Board}";
}
