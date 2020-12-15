using Advent_of_Code;
using SmartAss.Parsing;
using System.Collections.Generic;

namespace Advent_of_Code_2020
{
    public class Day_15
    {
        [Example(answer: 436, @"0,3,6")]
        [Puzzle(answer: 620, input: "0,12,6,13,20,1,17")]
        public int part_one(string input) => MemoryGame(input.Int32s(), 2020);

        [Puzzle(answer: 110871, input: "0,12,6,13,20,1,17")]
        public int part_two(string input) => MemoryGame(input.Int32s(), 30000000);

        private static int MemoryGame(IEnumerable<int> starting, int rounds)
        {
            var round = 1;
            var previous = 0;
            var recents = new int[rounds];
            var befores = new int[rounds];

            foreach (var n in starting)
            {
                recents[n] = round++;
                previous = n;
            }
            while (round <= rounds)
            {
                var before = befores[previous];
                var number = before == 0 ? 0: round - before - 1;
                befores[number] = recents[number];
                recents[number] = round++;
                previous = number;
            }
            return previous;
        }
    }
}