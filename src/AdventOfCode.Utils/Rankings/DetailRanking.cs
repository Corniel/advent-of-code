namespace Advent_of_Code.Rankings;

public static class DetailRanking
{
    public delegate int Score(int rank, int participants);

    public static IReadOnlyList<DetailRanking<int>> Top_10(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => Math.Max(0, 11 - rank));

    public static IReadOnlyList<DetailRanking<int>> Default(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => 1+ participants - rank);

    public static IReadOnlyList<DetailRanking<int>> _50_percent(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => 1 + participants * 2 - rank);

    public static IReadOnlyList<DetailRanking<int>> Calculate(Participants participants, int year, Score score)
    {
        var date = new AdventDate(year, null, null);
        var active = participants.Where(p => p.Value.Solutions.Any(s => s.Key.Year == date.Year))
            .Select(p => new DetailRanking<int>(p.Value, new List<Score<int>>()))
            .ToArray();

        foreach (var now in AdventDate.AllAvailable().Where(d => d.Year == date.Year))
        {
            Extend(active, now, score);
        }

        return active
            .OrderByDescending(r => r.Score)
            .Select((r, index) => r with { Position = index + 1 })
            .ToArray();

        static void Extend(DetailRanking<int>[] active, AdventDate now, Score score)
        {
            var rank = 1;
            foreach (var one in GetSolutions(active, now))
            {
                var editable = (List<Score<int>>)one.Scores;
                editable.Add(new(now, rank, score(rank++, active.Length)));
            }
        }

        static IOrderedEnumerable<DetailRanking<int>> GetSolutions(DetailRanking<int>[] active, AdventDate date) => active
            .Where(s => s.Participant.Solutions.ContainsKey(date))
            .OrderBy(s => s.Participant.Solutions[date]);
    }
}
