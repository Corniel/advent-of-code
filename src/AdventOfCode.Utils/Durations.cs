namespace Advent_of_Code;

public sealed class Durations : List<TimeSpan>
{
    public TimeSpan Min => this.Min();

    public TimeSpan Total => TimeSpan.FromTicks(this.Sum(duration => duration.Ticks));

    public TimeSpan Average => TimeSpan.FromTicks(this.Sum(duration => duration.Ticks) / Count);

    public TimeSpan Median
    {
        get
        {
            Sort();
            var median = (Count - 1) / 2;
            return Count % 2 == 1
                ? this[median]
                : TimeSpan.FromTicks((this[median].Ticks + this[median + 1].Ticks) / 2);
        }
    }

    public override string ToString() => $"Count: {Count,3}, Min: {Min.Formatted(),8}, Median: {Median.Formatted(),8}, Avg: {Average.Formatted(),8}";
}
