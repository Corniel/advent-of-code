using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day08
    {
        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 08);
            Puzzle.HasAnswer(2480, Day08.One, with: input);
        }
  
        [Test]
        /// <remarks>
        /// ZYBLH
        /// </remarks>
        public void part_two()
        {
            var input = Input.For(2019, 08);
            Puzzle.HasAnswer(@"
████░█░░░████░░█░░░░█░░█░
░░░█░█░░░██░░█░█░░░░█░░█░
░░█░░░█░█░███░░█░░░░████░
░█░░░░░█░░█░░█░█░░░░█░░█░
█░░░░░░█░░█░░█░█░░░░█░░█░
████░░░█░░███░░████░█░░█░
", Day08.Two, with: input);
        }
    }
}
