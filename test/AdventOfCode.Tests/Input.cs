using SmartAss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    public sealed class Input
    {
        private Input() => Do.Nothing();

        public static string For(int year, int day, Part part = default)
        {
            var path = part == default
                ? $"AdventOfCode.Tests.Inputs._{year}.{day:00}.txt"
                : $"AdventOfCode.Tests.Inputs._{year}.{day:00}_{part}.txt";

            using (var stream = typeof(Input).Assembly.GetManifestResourceStream(path))
            {
                if (stream is null) throw new FileNotFoundException(path);
                var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
    }
}
