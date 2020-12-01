using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Parser
    {
        public static IEnumerable<int> Numbers(string str)
            => str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(sub => int.Parse(sub));
    }
}
