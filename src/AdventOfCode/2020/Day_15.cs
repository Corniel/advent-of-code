using Advent_of_Code;
using SmartAss.Parsing;
using System.Collections.Generic;

namespace Advent_of_Code_2020
{
    public class Day_15
    {
        [Example(answer: 436, @"0,3,6")]
        [Puzzle(answer: 620, input: "0,12,6,13,20,1,17")]
        public long part_one(string input) => MemoryGame(input.Int32s(), 2020);

        [Puzzle(answer: 110871, input: "0,12,6,13,20,1,17")]
        public long part_two(string input) => MemoryGame(input.Int32s(), 30000000);

        private static long MemoryGame(IEnumerable<int> starting, int repeats)
        {
            var round = 1;
            long previous = 0;
            var recents = new Dictionary<long, int>();
            var befores = new Dictionary<long, int>();

            foreach (var n in starting)
            {
                recents[n] = round++;
                previous = n;
            }
            while (round <= repeats)
            {
                var number = befores.TryGetValue(previous, out var before) ? round - before - 1 : 0;
                if (recents.TryGetValue(number, out var recent)) { befores[number] = recent; }
                recents[number] = round;
                previous = number;
                round++;
            }
            return previous;
        }
    }
}