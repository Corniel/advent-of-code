using Advent_of_Code;
using Advent_of_Code_2019.IntComputing;
using NUnit.Framework;
using SmartAss.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_11
    {
        private static readonly char White = '█';
        private static readonly char Black = '░';

        [Puzzle(answer: 2184, year: 2019, day: 11)]
        public int part_one(string input)
        {
            var computer = Computer.Parse(input);
            return DrawCanvas(computer, 0).Count;
        }

        /// <remarks>AHCHZEPK</remarks>
        [Puzzle(answer: @"
░░██░░█░░█░░██░░█░░█░████░████░███░░█░░█░░░
░█░░█░█░░█░█░░█░█░░█░░░░█░█░░░░█░░█░█░█░░░░
░█░░█░████░█░░░░████░░░█░░███░░█░░█░██░░░░░
░████░█░░█░█░░░░█░░█░░█░░░█░░░░███░░█░█░░░░
░█░░█░█░░█░█░░█░█░░█░█░░░░█░░░░█░░░░█░█░░░░
░█░░█░█░░█░░██░░█░░█░████░████░█░░░░█░░█░░░", year: 2019, day: 11)]
        public string part_two(string input)
        {
            var computer = Computer.Parse(input);
            var canvas = DrawCanvas(computer, 1);

            Assert.AreEqual(0, canvas.Keys.Min(k => k.X), "x-min");
            Assert.AreEqual(0, canvas.Keys.Min(k => k.Y), "y-min");
            Assert.AreEqual(42, canvas.Keys.Max(k => k.X), "x-max");
            Assert.AreEqual(5, canvas.Keys.Max(k => k.Y), "y-max");

            var line = new string(Black, 43);
            var output = Enumerable.Range(0, 6).Select(i => line.ToCharArray()).ToArray();
            
            foreach(var pos in canvas.Where(s => s.Value == 1).Select(s=> s.Key))
            {
                output[pos.Y][pos.X] = White;
            }

            var message = string.Join("\r\n", output.Select(l => string.Concat(l)));
            return "\r\n" + message;
        }


        private static Dictionary<Point, int> DrawCanvas(Computer computer, int color)
        {
            var canvas = new Dictionary<Point, int> { { Point.O, color } };
            var bot = Point.O;
            var dir = Vector.N;

            while (!computer.Finished)
            {
                canvas.TryGetValue(bot, out color);
                color = (int)computer.Run(new RunArguments(false, true, color)).LastOrDefault();
                var turn = computer.Run(new RunArguments(false, true)).LastOrDefault();

                canvas[bot] = color;
                dir = dir.Rotate(turn == 0 ? DiscreteRotation.Deg090 : DiscreteRotation.Deg270);
                bot += dir;
            }

            return canvas;
        }

    }
}