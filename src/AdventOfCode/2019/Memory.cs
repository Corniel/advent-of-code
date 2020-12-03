using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Memory
    {
        public static IList<int> Copy(this IList<int> memory)
              => memory.ToArray();

        public static IList<int> Update(this IList<int> memory, int pointer, int value)
        {
            memory[pointer] = value;
            return memory;
        }

        public static IList<int> Parse(string str)
            => str
            .CommaSeperated()
            .SelectMany(s => s.Ints())
            .ToArray();
    }
}
