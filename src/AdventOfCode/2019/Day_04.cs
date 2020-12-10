using Advent_of_Code;
using System;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_04
    {
        [Puzzle(answer: 1178,"235741-706948")]
        public int part_one(string input)
            => CountValidPasswords(input, PasswordForOne);

        [Puzzle(answer: 763,"235741-706948")]
        public int part_two(string input)
            => CountValidPasswords(input, PasswordForTwo);

        private static int CountValidPasswords(string input, Func<int, bool> validator)
        {
            var boundries = input.Split('-', StringSplitOptions.TrimEntries)
                .Select(str => int.Parse(str))
                .ToArray();

            return Enumerable.Range(boundries[0], 1 + boundries[1] - boundries[0])
                .Count(validator);
        }

        private static bool PasswordForOne(int password)
        {
            if (password > 999_999) return false;
            var adjacent = false;

            var current = password % 10;
            password /= 10;

            // we go from right to left.
            for (var digit = 1; digit <= 6; digit++)
            {
                var previous = password % 10;

                // decrease
                if (previous > current) return false;

                adjacent |= previous == current;

                current = previous;
                password /= 10;
            }
            return adjacent;
        }

        private static bool PasswordForTwo(int password)
        {
            if (password > 999_999) return false;
            var adjacent = false;

            var group = 1;
            var current = password % 10;
            password /= 10;

            // we go from right to left.
            for (var digit = 1; digit <= 6; digit++)
            {
                var previous = password % 10;

                // decrease
                if (previous > current) return false;

                if (previous == current)
                {
                    group++;
                }
                else
                {
                    adjacent |= group == 2;
                    group = 1;
                }

                current = previous;
                password /= 10;
            }
            return adjacent;
        }
    }
}