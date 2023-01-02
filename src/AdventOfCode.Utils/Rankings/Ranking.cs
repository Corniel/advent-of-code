namespace Advent_of_Code.Rankings;

public static class Ranking
{
    public static SolvingRanking Solving(int year, IEnumerable<Participant> participants, DateTime? reference = null)
        => SolvingRanking.Create(year, participants, reference);

    public delegate int DetailScore(int rank, int participants);

    public static List<OverallRanking> Overall(Participants participants)
    {
        var list = new List<OverallRanking>(participants
            .Where(p => p.Value.Solutions.Any())
            .Select(p => new OverallRanking(p.Value)));

        foreach (var date in AdventDate.AllAvailable())
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
}
