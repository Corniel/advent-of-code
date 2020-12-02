using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Parser
    {
        public static IEnumerable<string> Lines(string str)
            => str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public static IEnumerable<int> Numbers(string str)
            => Lines(str)
            .Select(sub => int.Parse(sub));
    }
}
