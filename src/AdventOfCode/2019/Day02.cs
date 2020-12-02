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
        public static int OneExample(string input)
            => Intcode.Parse(input)
            .Run();

        [Puzzle(2019, 02, Part.one)]
        public static int One(string input)
            => Intcode.Parse(input)
            .Update(1, 12)
            .Update(2, 2)
            .Run();

        [Puzzle(2019, 02, Part.two)]
        public static int Two(string input)
            => Intcode.Parse(input).Run();
    }
}