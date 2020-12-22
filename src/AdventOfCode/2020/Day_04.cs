using Advent_of_Code;
using SmartAss.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    public class Day_04
    {
        [Example(answer: 2, year: 2020, day: 04, example: 1)]
        [Puzzle(answer: 228, year: 2020, day: 04)]
        public int part_one(string input)
            => Passport.Parse(input).Count(p => p.IsValid());

        [Puzzle(answer: 175, year: 2020, day: 04)]
        public int part_two(string input)
            => Passport.Parse(input).Count(p => p.StrictValid());

        public class Passport : Dictionary<string, string>
        {
            private bool duplicate;

            public bool byr => TryGetValue(nameof(byr), out var str)
                && int.TryParse(str, out var year)
                && year >= 1920 && year <= 2002;

            public bool iyr => TryGetValue(nameof(iyr), out var str)
                && int.TryParse(str, out var year)
                && year >= 2010 && year <= 2020;

            public bool hgt => TryGetValue(nameof(hgt), out var str)
               && int.TryParse(str.Substring(0, str.Length - 2), out var length)
               && ((str.EndsWith("cm") && length >= 150 && length <= 193) ||
                (str.EndsWith("in") && length >= 59 && length <= 76));

            public bool eyr => TryGetValue(nameof(eyr), out var str)
              && int.TryParse(str, out var year)
              && year >= 2020 && year <= 2030;

            public bool ecl => TryGetValue(nameof(ecl), out var str)
              && (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }).Contains(str);

            public bool hcl => TryGetValue(nameof(hcl), out var str)
              && Regex.IsMatch(str, "^#[0-9a-f]{6}$");

            public bool pid => TryGetValue(nameof(pid), out var str)
              && Regex.IsMatch(str, "^[0-9]{9}$");

            public bool StrictValid() => IsValid()
                && byr
                && iyr
                && eyr
                && hgt
                && hcl
                && ecl
                && pid;

            public bool IsValid() =>
                !duplicate
                && ContainsKey(nameof(byr))
                && ContainsKey(nameof(iyr))
                && ContainsKey(nameof(eyr))
                && ContainsKey(nameof(hgt))
                && ContainsKey(nameof(hcl))
                && ContainsKey(nameof(ecl))
                && ContainsKey(nameof(pid))
                && ((ContainsKey("cid") && Count == 8) || Count == 7);

            public static IEnumerable<Passport> Parse(string str)
            {
                foreach (var lines in str.GroupedLines())
                {
                    var passport = new Passport();

                    foreach (var line in lines)
                    {
                        foreach (var block in line.SpaceSeperated())
                        {
                            var kvp = block.Seperate(':');
                            var key = kvp[0];
                            var value = kvp[1];
                            passport.duplicate = passport.ContainsKey(key);
                            passport[key] = value;
                        }
                    }
                    yield return passport;
                }
            }
        }
    }
}