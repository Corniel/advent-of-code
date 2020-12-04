using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day04
    {
        [Test]
        public void part_one_example()
        {
            var input = @"
ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
            Puzzle.HasAnswer(2, Day04.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 04);
            Puzzle.HasAnswer(228, Day04.One, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 04);
            Puzzle.HasAnswer(175, Day04.Two, with: input);
        }
    }
}
