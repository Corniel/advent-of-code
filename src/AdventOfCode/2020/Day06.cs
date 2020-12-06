using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public class Day06
    {
        private static readonly string a_z = "abcdefghijklmnopqrstuvwxyz";


        [Puzzle(2020, 06, Part.one)]
        public static int One(string input)
            => input
            .GroupedLines()
            .Sum(group => Characters.a_z
                .Count(ch => group.Any(member => member.Contains(ch))));
            
        [Puzzle(2020, 06, Part.two)]
        public static int Two(string input)
              => input
            .GroupedLines()
            .Sum(group => Characters.a_z
                .Count(ch => group.All(member => member.Contains(ch))));
    }
}