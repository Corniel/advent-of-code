using System.Linq;

namespace AdventOfCode._2019
{
    public class Day01
    {
        public static int One(string input)
            => input.Int32s().Sum(Fuel);

        private static int Fuel(int mass) => (mass / 3) - 2;
    
        public static int Two(string input)
            => input.Int32s().Sum(RecursiveFuel);

        private static int RecursiveFuel(int mass)
        {
            var total = 0;
            while(true)
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