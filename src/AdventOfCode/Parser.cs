using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Parser
    {
        public static char Char(this string str)
            => str[0];

        public static int Int(this string str)
            => int.Parse(str);

        public static IEnumerable<int> Ints(this string str)
            => Lines(str)
            .Select(Int);

        public static IEnumerable<string> CommaSeperated(this string str)
            => str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public static IEnumerable<string> Lines(this string str)
            => str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
