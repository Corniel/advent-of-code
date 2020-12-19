using Advent_of_Code;
using SmartAss.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    public class Day_19
    {
        [Example(answer: 2, @"
            0: 4 1 5
            1: 2 3 | 3 2
            2: 4 4 | 5 5
            3: 4 5 | 5 4
            4: ""a""
            5: ""b""

            ababbb
            bababa
            abbbab
            aaabbb
            aaaabbb")]
        [Puzzle(answer: 239, year: 2020, day: 19)]
        public int part_one(string input)
        {
            var blocks = input.GroupedLines().ToArray();
            var patterns = Patterns.Parse(blocks[0]);
            return patterns.Matches(blocks[1]);
        }

        [Example(answer: 12, @"
            42: 9 14 | 10 1
            9: 14 27 | 1 26
            10: 23 14 | 28 1
            1: ""a""
            11: 42 31
            5: 1 14 | 15 1
            19: 14 1 | 14 14
            12: 24 14 | 19 1
            16: 15 1 | 14 14
            31: 14 17 | 1 13
            6: 14 14 | 1 14
            2: 1 24 | 14 4
            0: 8 11
            13: 14 3 | 1 12
            15: 1 | 14
            17: 14 2 | 1 7
            23: 25 1 | 22 14
            28: 16 1
            4: 1 1
            20: 14 14 | 1 15
            3: 5 14 | 16 1
            27: 1 6 | 14 18
            14: ""b""
            21: 14 1 | 1 14
            25: 1 1 | 1 14
            22: 14 14
            8: 42
            26: 14 22 | 1 20
            18: 15 15
            7: 14 5 | 1 21
            24: 14 1

            abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
            bbabbbbaabaabba
            babbbbaabbbbbabbbbbbaabaaabaaa
            aaabbbbbbaaaabaababaabababbabaaabbababababaaa
            bbbbbbbaaaabbbbaaabbabaaa
            bbbababbbbaaaaaaaabbababaaababaabab
            ababaaaaaabaaab
            ababaaaaabbbaba
            baabbaaaabbaaaababbaababb
            abbbbabbbbaaaababbbbbbaaaababb
            aaaaabbaabaaaaababaa
            aaaabbaaaabbaaa
            aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
            babaaabbbaaabaababbaabababaaab
            aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba")]
        [Puzzle(answer: 405, year: 2020, day: 19)]
        public long part_two(string input)
        {
            var blocks = input.GroupedLines().ToArray();
            var patterns = Patterns.Parse(blocks[0]);
            patterns[08] = $"({patterns[42]})+";
            // Recursion is not supported for Regex in .NET (4 repetitions turns out to be enough).
            patterns[11] = $"(({patterns[42]}{patterns[31]})" +
                $"|({patterns[42]}{{2}}{patterns[31]}{{2}})" +
                $"|({patterns[42]}{{3}}{patterns[31]}{{3}})" +
                $"|({patterns[42]}{{4}}{patterns[31]}{{4}}))";
            return patterns.Matches(blocks[1]);
        }
        private class Patterns : Dictionary<int, object>
        {
            public int Matches(IEnumerable<string> messages)
            {
                var regex = new Regex($"^{this[0]}$");
                return messages.Count(message => regex.IsMatch(message));
            }
            public static Patterns Parse(IEnumerable<string> lines)
            {
                var patterns = new Patterns();
                foreach (var line in lines)
                {
                    var split = line.Seperate(':');
                    var id = split[0].Int32();
                    var pattern = split[1];

                    if (pattern[1] == 'a' || pattern[1] == 'b') { patterns[id] = pattern[1]; }
                    else
                    {
                        var piped = pattern.Seperate('|');
                        patterns[id] = piped.Length == 1
                            ? Combined.Parse(pattern, patterns)
                            : new Or(Combined.Parse(piped[0], patterns), Combined.Parse(piped[1], patterns));
                    }
                }
                return patterns;
            }
        }
        private record Reference(int Id, Patterns Patterns) { public override string ToString() => $"{Patterns[Id]}"; }
        private record Or(object Left, object Right) { public override string ToString() => $"({Left}|{Right})"; }
        private record Combined(object[] Sequance)
        {
            public override string ToString() => string.Concat(Sequance.Select(s => s.ToString()));
            public static Combined Parse(string str, Patterns patterns)
                => new Combined(str.SpaceSeperated(r => new Reference(r.Int32(), patterns)).ToArray());
        }
    }
}