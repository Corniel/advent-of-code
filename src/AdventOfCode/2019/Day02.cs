using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Day02
    {
        [Puzzle(2019, 02, Part.one)]
        public static int One(string input)
            => Intcode.Parse(input).SetNounAndVerb(1, 12).Run();

        [Puzzle(2019, 02, Part.two)]
        public static int Two(string input)
            => Intcode.Parse(input).Run();
    }
}