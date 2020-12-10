using Advent_of_Code;
using NUnit.Framework;
using System.Collections.Generic;
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
            => Password.Parse(input).Count(p => p.Policy.ValidForOne(p.Chars));

        [Example(answer: 1, @"
            1-3 a: abcde
            1-3 b: cdefg
            2-9 c: ccccccccc")]
        [Puzzle(answer: 558, year: 2020, day: 02)]
        public int part_two(string input)
            => Password.Parse(input).Count(p => p.Policy.ValidForTwo(p.Chars));

        private readonly struct PasswordPolicy
        {
            public PasswordPolicy(int min, int max, char ch)
            {
                Min = min;
                Max = max;
                Char = ch;
            }

            public int Min { get; }
            public int Max { get; }
            public char Char { get; }

            public bool ValidForOne(string str)
            {
                var policy = this;
                var occurences = str.Count(ch => ch == policy.Char);
                return occurences >= Min
                    && occurences <= Max;
            }

            public bool ValidForTwo(string str)
            {
                var min = str[Min - 1];
                var max = str[Max - 1];
                return (min == Char) ^ (max == Char);
            }

        }
        private readonly struct Password
        {
            public Password(string chars, PasswordPolicy policy)
            {
                Chars = chars;
                Policy = policy;
            }

            public string Chars { get; }
            public PasswordPolicy Policy { get; }

            public static IEnumerable<Password> Parse(string str)
            {
                foreach (var line in Parser.Lines(str))
                {
                    var split = line.Split(':');
                    var policy = split[0].Split('-', ' ');
                    var chars = split[1].Trim();
                    yield return new Password(chars,
                        new PasswordPolicy(
                            min: policy[0].Int32(),
                            max: policy[1].Int32(),
                            ch: policy[2].Char()));
                }
            }
        }
    }
}