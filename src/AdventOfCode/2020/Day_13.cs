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
            var splits = input.StripChars("x").Lines().ToArray();
            var departure = splits[0].Int32();
            var bus = splits[1].CommaSeperated()
               .Select(s => new Bus(s.Int32(), Offset(s.Int32(), departure)))
               .OrderBy(b => b.Offset)
               .First();
            return bus.Id * bus.Offset;
        }

        [Example(answer: 1068781, @"939;7,13,x,x,59,x,31,19")]
        [Puzzle(answer: 672754131923874, input: @"1008713;13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23")]
        public long part_two(string input)
        {
            var bus = new Bus(1, 0);
            var busses = input.Lines().ToArray()[1].CommaSeperated()
                .Select((id, index) => new Bus(id.Int32(fallback: 0), index % id.Int32(fallback: 1)))
                .Where(b => b.Id != default);

            foreach (var other in busses)
            {
                bus = Merge(bus, other);
            }
            return bus.Offset;
        }

        private static Bus Merge(Bus bus, Bus other)
        {
            long offset = -1;
            for(var departure = bus.Offset; /* oo */ ; departure+= bus.Id)
            {
                if (Offset(other.Id, departure) == other.Offset)
                {
                    if (offset == -1) { offset = departure; }
                    else { return new Bus(departure - offset, offset); }
                }
            }
            throw new NoAnswer();
        }
        private static long Offset(long bus, long depature) => (-depature).Mod(bus);
        private record Bus(long Id, long Offset);
    }
}