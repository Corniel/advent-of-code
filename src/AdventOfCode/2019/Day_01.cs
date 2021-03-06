using Advent_of_Code;
using SmartAss.Parsing;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_01
    {
        [Puzzle(answer: 3291356, year: 2019, day: 01)]
        public int part_one(string input)
            => input.Int32s().Sum(Fuel);
        
        [Puzzle(answer: 4934153, year: 2019, day: 01)]
        public int part_two(string input)
            => input.Int32s().Sum(RecursiveFuel);

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