using Advent_of_Code;
using NUnit.Framework;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_01
    {
        [Puzzle(answer: 3291356, year: 2019, day: 01)]
        public void part_one(long answer, string input)
        {
            var outcome = input.Int32s().Sum(Fuel);
            Assert.That(outcome, Is.EqualTo(answer));
        }

        [Puzzle(answer: 4934153, year: 2019, day: 01)]
        public void part_two(long answer, string input)
        {
            var outcome = input.Int32s().Sum(RecursiveFuel);
            Assert.That(outcome, Is.EqualTo(answer));
        }

        private static int Fuel(int mass) => (mass / 3) - 2;

        private static int RecursiveFuel(int mass)
        {
            var total = 0;
            while (true)
            {
                var fuel = Fuel(mass);
                if (fuel > 0)
                {
                    total += fuel;
                    mass = fuel;
                }
                else break;
            }
            return total;
        }
    }
}