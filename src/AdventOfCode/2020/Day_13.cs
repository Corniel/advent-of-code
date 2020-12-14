using Advent_of_Code;
using SmartAss.Parsing;
using System;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_13
    {
        [Example(answer: 295, @"939;7,13,x,x,59,x,31,19")]
        [Puzzle(answer: 3269, input: @"1008713;13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23")]
        public long part_one(string input)
        {
            var splits = input.StripChars("x").Lines();
            var departure = splits[0].Int32();
            var bus = splits[1]
               .CommaSeperated(s => new Bus(s.Int32(), Offset(s.Int32(), departure)))
               .OrderBy(b => b.Offset)
               .First();
            return bus.Period * bus.Offset;
        }

        [Example(answer: 1068781, @"939;7,13,x,x,59,x,31,19")]
        [Puzzle(answer: 672754131923874, input: @"1008713;13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23")]
        public long part_two(string input)
        {
            var busses = input.Lines()[1]
                .CommaSeperated((id, index) => new Bus(id.Int32(fallback: 0), index % id.Int32(fallback: 1)))
                .Where(b => b.Period != default);
            var bus = busses.First();

            foreach (var other in busses.Skip(1))
            {
                bus = Merge(bus, other);
            }
            return bus.Offset;
        }

        private static Bus Merge(Bus bus, Bus other)
        {
            for (var departure = bus.Offset; /* oo */ ; departure += bus.Period)
            {
                if (Offset(other.Period, departure) == other.Offset)
                {
                    return new Bus(bus.Period * other.Period, departure);
                }
            }
            throw new NoAnswer();
        }
        private static long Offset(long bus, long depature) => (-depature).Mod(bus);
        private record Bus(long Period, long Offset);
    }
}