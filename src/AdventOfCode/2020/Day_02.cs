using Advent_of_Code;
using SmartAss.Parsing;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_02
    {
        [Example(answer: 2, @"
            1-3 a: abcde
            1-3 b: cdefg
            2-9 c: ccccccccc")]
        [Puzzle(answer: 536, year: 2020, day: 02)]
        public int part_one(string input)
            => input.Lines(Password.Parse).Count(p => p.Policy.ValidForOne(p.Chars));

        [Example(answer: 1, @"
            1-3 a: abcde
            1-3 b: cdefg
            2-9 c: ccccccccc")]
        [Puzzle(answer: 558, year: 2020, day: 02)]
        public int part_two(string input)
            => input.Lines(Password.Parse).Count(p => p.Policy.ValidForTwo(p.Chars));

        private record PasswordPolicy(int Min, int Max, char Char)
        {
            public bool ValidForOne(string str)
            {
                var policy = this;
                var occurences = str.Count(ch => ch == policy.Char);
                return occurences >= Min && occurences <= Max;
            }
            public bool ValidForTwo(string str)
            {
                var min = str[Min - 1];
                var max = str[Max - 1];
                return (min == Char) ^ (max == Char);
            }
        }
        private record Password(string Chars, PasswordPolicy Policy)
        {
            public static Password Parse(string str)
            {
                    var split = str.Split(':');
                    var pol = split[0].Split('-', ' ');
                    var chars = split[1].Trim();
                    return new Password(chars,
                        new PasswordPolicy(
                            Min: pol[0].Int32(),
                            Max: pol[1].Int32(),
                            Char: pol[2].Char()));
            }
        }
    }
}