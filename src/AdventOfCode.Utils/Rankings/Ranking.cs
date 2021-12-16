namespace Advent_of_Code.Rankings;

public static class Ranking
{
    public static List<DefaultRanking> Default(Participants participants, int year)
    {
        var list = new List<DefaultRanking>(participants
            .Where(p => p.Value.Solutions.Any(s => s.Key.Year == year))
            .Select(p => new DefaultRanking(p.Value, year)));

        for (var day = 1; day <= 25; day++)
        {
            foreach (var part in new[] { 1, 2 })
            {
                var date = new AdventDate(year, day, part);
                var score = list.Count;
                foreach (var participant in list
                    .Where(p => p.Solutions.ContainsKey(date))
                    .OrderBy(p => p.Solutions[date]))
                {
                    participant.Score += score--;
                }
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
