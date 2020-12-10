using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code
{
    public static class Parser
    {
        public const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public static char Char(this string str)
            => str[0];

        public static int Int32(this string str)
            => int.Parse(str);

        public static long Int64(this string str)
         => long.Parse(str);

        public static IEnumerable<int> Int32s(this string str)
            => str.Int64s().Select(n => (int)n);

        public static IEnumerable<long> Int64s(this string str)
            => str.Split(new [] {",", "\r\n", "\n", "\t" }, SplitOptions)
            .Select(Int64);

        public static string[] Seperate(this string str, char splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static string[] Seperate(this string str, string splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static IEnumerable<string> CommaSeperated(this string str)
            => str.Seperate(',');

        public static IEnumerable<string> SpaceSeperated(this string str)
            => str.Seperate(' ');

        public static IEnumerable<string> Lines(this string str, StringSplitOptions options = SplitOptions)
            => str.Split(new[] { "\r\n", "\n", ";" }, options);

        public static IEnumerable<string[]> GroupedLines(this string str, StringSplitOptions options = SplitOptions)
        {
            var buffer = new List<string>();

            foreach (var line in str.Lines(StringSplitOptions.None))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (buffer.Any())
                    {
                        yield return buffer.ToArray();
                        buffer.Clear();
                    }
                    else if (!options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        yield return Array.Empty<string>();
                    }
                }
                else
                {
                    buffer.Add(options.HasFlag(StringSplitOptions.TrimEntries) ? line.Trim() : line);
                }
            }
            if (buffer.Any())
            {
                yield return buffer.ToArray();
            }
        }
    }
}
