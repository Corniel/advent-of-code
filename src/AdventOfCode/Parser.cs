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

        public static string[] Seperate(this string str, char splitter)
            => str.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public static IEnumerable<string> CommaSeperated(this string str)
            => str.Seperate(',');

        public static IEnumerable<string> SpaceSeperated(this string str)
            => str.Seperate(' ');

        public static IEnumerable<string> Lines(this string str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            => str.Split(new[] { "\r\n", "\n" }, options);
    }
}
