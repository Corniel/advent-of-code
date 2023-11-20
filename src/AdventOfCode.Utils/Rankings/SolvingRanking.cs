using Qowaiv;

namespace Advent_of_Code.Rankings;

public sealed class SolvingRanking(IReadOnlyList<SolvingRankingParticipant> participants)
{
    public IReadOnlyList<SolvingRankingParticipant> Participants { get; } = participants;

    public override string ToString()
    {
        if(!Participants.Any()) return "* No participants *";

        using var _ = CultureInfo.InvariantCulture.Scoped();

        var rank_length = Math.Max(3, Size(Participants.Count));
        var score_length = Size(Participants.Count * 50) + 3;
        var avg_length = Size(Participants.Count) + 3;
        var name_length = Participants.Select(p => p.Name.Length).Max();
        var days = Participants.SelectMany(p => p.Results.Select(r => r.Date.Day)).Max().Value;

        var sb = new StringBuilder();

        AppendHeader(rank_length, score_length, avg_length, name_length, days, sb);
        AppendHeaderLine(rank_length, score_length, avg_length, name_length, days, sb);

        var pos = 0;
        foreach (var p in Participants)
        {
            Append(p, ++pos, rank_length, score_length, avg_length, name_length, days, sb);
        }
        return sb.ToString();
    }

    static void AppendHeader(int rank_length, int score_length, int avg_length, int name_length, int days, StringBuilder sb)
    {
        sb.Append("| ").AppendFormatted("Pos", rank_length);
        sb.Append(" | ").AppendFormatted("Score", score_length).Append(' ');

        for (var day = 1; day <= days; day++)
        {
            if (day % 5 == 1) sb.Append('|');
            sb.Append($"{day:00}");
        }
        sb.Append("| ");
        sb.AppendFormatted("Rank", avg_length).Append(" | ");
        sb.AppendFormatted("Participant", -name_length).Append(" | ");
        sb.AppendFormatted("Last solved", 16).Append(" |");
        sb.AppendLine();
    }

    static void AppendHeaderLine(int rank_length, int score_length, int avg_length, int name_length, int days, StringBuilder sb)
    {
        sb.Append('|');
        sb.Append('-', rank_length + 1).Append(":|");
        sb.Append('-', score_length + 1).Append(':');

        for (var day = 1; day <= days; day++)
        {
            if (day.Mod(5) == 1) sb.Append('|');
            sb.Append("--");
        }
        sb.Append('|');
        sb.Append('-', avg_length + 1).Append(":|");
        sb.Append('-', name_length + 2).Append('|');
        sb.Append('-', 18).Append('|');
        sb.AppendLine();
    }

    static void Append(SolvingRankingParticipant p, int pos, int rank_length, int score_length, int avg_length, int name_length, int days, StringBuilder sb)
    {
        sb.Append("| ");
        sb.AppendFormatted(pos, rank_length).Append(" | ");
        sb.AppendFormatted(p.Score, score_length, $"0.{new string('0', score_length - 3)}").Append(' ');

        for (var day = 1; day <= days; day++)
        {
            if (day.Mod(5) == 1) sb.Append('|');
            Append(sb, p.Results.FirstOrDefault(s => s.Date.Day == day && s.Date.Part == 1));
            Append(sb, p.Results.FirstOrDefault(s => s.Date.Day == day && s.Date.Part == 2));
        }
        sb.Append("| ");
        sb.AppendFormatted(p.Rank, avg_length, "0.00").Append(" | ");
        sb.AppendFormatted(p.Name, -name_length).Append(" | ");
        sb.Append($"{p.Last:yyyy-MM-dd HH:mm} |");
        sb.AppendLine();

        static void Append(StringBuilder sb, SolvingRankingResult result)
        {
            if (result is null) sb.Append('.');
            else if (result.Rank > 9) sb.Append('+');
            else sb.Append(result.Rank);
        }
    }

    static int Size(int number) => (int)Math.Log10(number) + 1;


    public static SolvingRanking Create(int year, IEnumerable<Participant> participants, DateTime? reference = null)
    {
        var date = new AdventDate(year, null, null);
        reference ??= Clock.UtcNow();

        var ranked = new List<SolvingRankingParticipant>();
        
        foreach (var participant in participants) 
        {
            var results = participant.Solutions.Where(kvp => date.Matches(kvp.Key) && kvp.Value <= reference)
                .Select(kvp => new SolvingRankingResult(kvp.Key, kvp.Value))
                .ToArray();

            if (results.NotEmpty())
            {
                ranked.Add(new(participant, results));
            }
        }

        var total = ranked.Count;
        var factor = (decimal)Math.Pow(10, Size(50 * total));

        foreach (var puzzle in ranked.SelectMany(p => p.Results.Select(r => r.Date)).Distinct())
        {
            var rank = 0;
            
            foreach(var result in ranked.Select(r => r.Result(puzzle)).OfType<SolvingRankingResult>().OrderBy(r => r.Solved))
            {
                result.Score = (total - rank) / factor;
                result.Rank = ++rank;
            }
        }

        ranked.Sort();

        return new(ranked);
    }
}
