namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_13
{
    [Example(answer: 295, @"939;7,13,x,x,59,x,31,19")]
    [Puzzle(answer: 3269, @"1008713;13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23", O.μs)]
    public long part_one(string input)
    {
        var splits = input.StripChars("x").Lines();
        var departure = splits[0].Int32();
        var bus = splits[1]
           .CommaSeparated(s => new Bus(s.Int32(), GetOffset(s.Int32(), departure)))
           .OrderBy(b => b.Offset)
           .First();
        return bus.Period * bus.Offset;
    }

    [Example(answer: 1068781, @"939;7,13,x,x,59,x,31,19")]
    [Puzzle(answer: 672754131923874, @"1008713;13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23", O.μs)]
    public long part_two(Lines input)
    {
        var busses = input[1]
            .CommaSeparated((id, index) => new Bus(id.TryInt32(fallback: 0), index % id.TryInt32(fallback: 1)))
            .Where(b => b.Period != default);
        var bus = busses.First();

        foreach (var other in busses.Skip(1))
        {
            bus = bus.Merge(other);
        }
        return bus.Offset;
    }

    static long GetOffset(long bus, long depature) => (-depature).Mod(bus);

    private record Bus(long Period, long Offset)
    {
        public Bus Merge(Bus other)
        {
            for (var departure = Offset; /* oo */ ; departure += Period)
            {
                if (GetOffset(other.Period, departure) == other.Offset)
                {
                    return new Bus(Period * other.Period, departure);
                }
            }
            throw new InfiniteLoop();
        }
    }
}
