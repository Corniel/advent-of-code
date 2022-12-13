namespace Advent_of_Code.Rankings;

public sealed record DetailRanking<TValue>(Participant Participant, IReadOnlyCollection<Score<TValue>> Scores, int Position = 0) : IFormattable
    where TValue : struct, IComparable<TValue>, System.Numerics.IAdditionOperators<TValue, TValue, TValue>, IFormattable
{
    public TValue Score => Scores.Sum(s => s.Value);

    public decimal Rank => Scores.Select(s => (decimal)s.Rank).Average();

    public override string ToString() => ToString(null, null);

    public string ToString(string format, IFormatProvider formatProvider)
    {
        var sb = new StringBuilder();
        sb.Append($"{Position,3}) ");
        sb.Append($"{Score,5} ");

        for (var day = 1; day <= 25; day++)
        {
            if (day.Mod(5) == 1) sb.Append('|');
            Append(sb, Scores.FirstOrDefault(s => s.Date.Day == day && s.Date.Part == 1));
            Append(sb, Scores.FirstOrDefault(s => s.Date.Day == day && s.Date.Part == 2));
        }
        sb.Append(FormattableString.Invariant($" {Rank,5:0.0}"));
        sb.Append($"  {Participant.ToString(format, formatProvider)}");
        return sb.ToString();

        static void Append(StringBuilder sb, Score<TValue> score)
        {
            if (score is null) sb.Append('.');
            else if(score.Rank > 9) sb.Append('*');
            else sb.Append(score.Rank);
        }
    }
}

