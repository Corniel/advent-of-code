using NUnit.Framework;
using System;
using System.Diagnostics;

namespace AdventOfCode.Tests
{
    public static class Puzzle
    {
        public static void HasAnswer<T>(T expected, ProgrammingPuzzle<T> puzzle, string with)
        {
            var sw = Stopwatch.StartNew();
            var answer = puzzle(with);
            sw.Stop();

            Console.WriteLine($"Took: {sw.Elapsed.TotalMilliseconds:#,##0.000}ms ({sw.ElapsedTicks:#,##0} ticks)");
            Assert.AreEqual(expected, answer);
        }
    }
}
