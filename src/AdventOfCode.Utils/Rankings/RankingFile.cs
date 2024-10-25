using Qowaiv;

namespace Advent_of_Code.Rankings;

public record RankingFile(int Year, Board Board)
{
    public FileInfo Local
        => new(Path.Combine(Data.Location.FullName, $"{Year}/{Board.Id}.json"));

    public Uri Remote => new($"https://adventofcode.com/{Year}/leaderboard/private/view/{Board.Id}.json");

    public Stream OpenRead() => Local.OpenRead();

    public bool MayCheckForUpdate
        => !Local.Exists || LastYear()
        ? Clock.UtcNow() - Local.LastWriteTimeUtc > TimeSpan.FromMinutes(15)
        : Clock.UtcNow() - Local.LastWriteTimeUtc > TimeSpan.FromDays(1);

    private bool LastYear() => Clock.UtcNow() - new Date(Year, 12, 25) < TimeSpan.FromDays(35);

    public override string ToString() => $"Year: {Year}, ID: {Board.Id,7}, Updated: {Local.LastAccessTimeUtc:yyyy-MM-dd HH:mm}, Name: {Board.Name}";
}
