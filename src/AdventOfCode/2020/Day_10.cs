using Advent_of_Code;

namespace Advent_of_Code_2020
{
    public class Day_10
    {
        [Example(answer: 5 * 7, "16,10,15,5,1,11,7,19,6,12,4")]
        [Puzzle(answer: 2100, year: 2020, day: 10)]
        public long part_one(string input)
        {
            var unique = new UniqueNumbers(input.Int32s());

            var prev = 0;
            var d1 = 0;
            var d3 = 1;

            foreach(var num in unique)
            {
                var delta = num - prev;
                switch (delta)
                {
                    case 1: d1++; break;
                    case 2: break;
                    case 3: d3++; break;
                    default: throw new NoAnswer();
                }
                prev = num;
            }
            return d1 * d3;
        }

        [Example(answer: 8, "16,10,15,5,1,11,7,19,6,12,4")]
        [Example(answer: 19208, "28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3")]
        [Puzzle(answer: 16198260678656L, year: 2020, day: 10)]
        public long part_two(string input)
        {
            var unique = new UniqueNumbers(input.Int32s());
            unique.Add(unique.Maximum + 3);
            
            var size = 0;
            var prev = 0;

            long combinations = 1;
            var combos = new[] { 1, 1, 1, 2, 4, 7 };

            foreach (var num in unique)
            {
                size++;
                if (num - prev == 3) 
                {
                    combinations *= combos[size];
                    size = 0;
                }
                prev = num;
            }
           return combinations;
        }
    }
}