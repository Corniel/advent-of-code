using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Parser
    {
        public static char Char(this string str)
            => str[0];

        public static int Int32(this string str)
            => int.Parse(str);

        public static IEnumerable<int> Int32s(this string str)
            => Lines(str)
            .Select(Int32);

        public static string[] Seperate(this string str, char splitter)
            => str.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public static string[] Seperate(this string str, string splitter)
            => str.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public static IEnumerable<string> CommaSeperated(this string str)
            => str.Seperate(',');

        public static IEnumerable<string> SpaceSeperated(this string str)
            => str.Seperate(' ');

        public static IEnumerable<string> Lines(this string str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            => str.Split(new[] { "\r\n", "\n" }, options);

        public static IEnumerable<string[]> GroupedLines(this string str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
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
