namespace Advent_of_Code.Rankings;

public static class Ranking
{
    public delegate int DetailScore(int rank, int participants);

    public static IReadOnlyList<DetailRanking<int>> Solving(Participants participants, int year, int solve)
        => Calculate(participants, year, (rank, participants) => Math.Max(0, participants + solve - rank));


    public static IReadOnlyList<DetailRanking<int>> Top_10(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => Math.Max(0, 11 - rank));

    public static IReadOnlyList<DetailRanking<int>> Default(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => 1 + participants - rank);

    public static IReadOnlyList<DetailRanking<int>> _50_percent(Participants participants, int year)
        => Calculate(participants, year, (rank, participants) => 1 + participants * 2 - rank);

    public static IReadOnlyList<DetailRanking<int>> Calculate(Participants participants, int year, DetailScore score)
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

        static void Extend(DetailRanking<int>[] active, AdventDate now, DetailScore score)
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

    public static List<OverallRanking> Overall(Participants participants)
    {
        var list = new List<OverallRanking>(participants
            .Where(p => p.Value.Solutions.Any())
            .Select(p => new OverallRanking(p.Value)));

        foreach(var date in AdventDate.AllAvailable())
        {
            var score = 10000 + list.Count(p => p.Solutions.Keys.Any(d => d.Year == date.Year));
            foreach (var participant in list
                .Where(p => p.Solutions.ContainsKey(date))
                .OrderBy(p => p.Solutions[date]))
            {
                participant.Score += score--;
            }
        }
        list.Sort();
        var pos = 1;
        foreach (var participant in list)
        {
            participant.Position = pos++;
        }
        return list;
    }
    
    public static List<TimeRanking> Time(Participants participants, int year)
    {
        var list = new List<TimeRanking>(participants
            .Where(p => p.Value.Solutions.Any(s => s.Key.Year == year))
            .Select(p => new TimeRanking(p.Value, year)));

        for (var day = 1; day <= 25; day++)
        {
            
            var one = new AdventDate(year, day, 1);
            var two = new AdventDate(year, day, 2);
            var score = list.Count;

            foreach (var participant in list
                .Where(p => p.Solutions.ContainsKey(one) && p.Solutions.ContainsKey(two))
                .OrderBy(p => p.Solutions[two] - p.Solutions[one]))
            {
                participant.Score += score--;
            }
        }
        list.Sort();
        var pos = 1;
        foreach (var participant in list)
        {
            participant.Position = pos++;
        }
        return list;
    }
}
