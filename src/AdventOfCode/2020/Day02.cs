using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public class Day02
    {
        [Puzzle(2020, 02, Part.one)]
        public static int One(string input)
            => Password.Parse(input).Count(p => p.Policy.ValidForOne(p.Chars));

        [Puzzle(2020, 02, Part.two)]
        public static int Two(string input)
        => Password.Parse(input).Count(p => p.Policy.ValidForTwo(p.Chars));
    }

    public readonly struct PasswordPolicy
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
    public readonly struct Password
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
                        min: policy[0].Int(),
                        max: policy[1].Int(),
                        ch: policy[2].Char()));
            }
        }
    }
}