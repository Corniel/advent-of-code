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

        public static string ForPuzzle(int year, int day, Part part)
        {
            var path = $"AdventOfCode.Tests.Inputs._{year}.{day:00}_{part.ToString()}.txt";
            using (var stream = typeof(Input).Assembly.GetManifestResourceStream(path))
            {
                var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
    }
}
