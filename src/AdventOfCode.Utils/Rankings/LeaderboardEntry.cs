using Advent_of_Code.Rankings.Json;
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

