using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Intcoding
{
    public static class IntcodeExtensions
    {
        public static int Answer(this Intcode intcode)
            => intcode is null
            ? throw new NoAnswer()
            : intcode.Memory[0];
    }
}
