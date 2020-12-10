using Advent_of_Code;
using NUnit.Framework;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_10
    {
        [Example(answer: 5 * 7, @"16
            10
            15
            5
            1
            11
            7
            19
            6
            12
            4")]
        [Puzzle(answer: 2100, year: 2020, day: 10)]
        public void part_one(long answer, string input)
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

            var outcome = d1 * d3;
            Assert.That(outcome, Is.EqualTo(answer));
        }

        [Example(answer: 8, @"16
            10
            15
            5
            1
            11
            7
            19
            6
            12
            4")]
        [Example(answer: 19208, @"28
            33
            18
            42
            31
            14
            46
            20
            48
            47
            24
            23
            49
            45
            19
            38
            39
            11
            1
            32
            25
            35
            8
            17
            7
            9
            4
            2
            34
            10
            3")]
        [Puzzle(answer: 16198260678656L, year: 2020, day: 10)]
        public void part_two(long answer, string input)
        {
            var unique = new UniqueNumbers(input.Int32s());
            unique.Add(0);

            var combinations = 1L;
            var prev = 0;
            var buffer = UniqueNumbers.Empty;

            foreach (var num in unique)
            {
                var delta = num - prev;
                if (delta == 3)
                {
                    var extra = Conbinations(buffer);
                    combinations *= extra;
                    buffer.Clear();
                    buffer.Add(num);
                }
                else
                {
                    buffer.Add(num);
                }
                prev = num;
            }
            combinations *= Conbinations(buffer);

            Assert.That(combinations, Is.EqualTo(answer));
        }

        [TestCase(1, "1")]
        [TestCase(1, "1,2")]
        [TestCase(2, "1,2,3")]
        [TestCase(4, "1,2,3,4")]
        [TestCase(7, "1,2,3,4,5")]
        [TestCase(2, "1,3,4")]
        public void has_combinations(long combinations, string input)
        {
            var numbers = new UniqueNumbers(input.CommaSeperated().Select(Parser.Int32));
            Assert.AreEqual(combinations, Conbinations(numbers));
        }

        private long Conbinations(UniqueNumbers buffer)
        {
            if (buffer.Count < 3)
            {
                return 1;
            }
            else
            {
                long combinations = 0;
                var max = 1;

                for (var s = 1; s < buffer.Count - 1; s++)
                {
                    max <<= 2;
                    max |= 1;
                }
                for (var instruction = 1; instruction <= max; instruction++)
                {
                    combinations += Conbination(buffer, instruction);
                }
                return combinations;
            }
        }

        private int Conbination(UniqueNumbers buffer, int instruction)
        {
            var pos = buffer.Minimum;
            var end = buffer.Maximum;

            while (instruction != 0)
            {
                var step = instruction & 3;
                instruction /= 4;
                if (step == 0) { return 0; }
                else
                {
                    pos += step;
                    if (!buffer.Contains(pos) || pos > end) { return 0; }
                    else if (pos == end) { return instruction == 0 ? 1 : 0;  }
                }
            }
            return 0;
        }
    }
}