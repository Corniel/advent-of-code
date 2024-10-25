namespace Advent_of_Code_2019;

[Category(Category.Simulation)]
public class Day_01
{
    [Puzzle(answer: 3291356, O.ns100)]
    public int part_one(Ints numbers) => numbers.Sum(Fuel);

    [Puzzle(answer: 4934153, O.ns100)]
    public int part_two(Ints numbers) => numbers.Sum(RecursiveFuel);

    static int Fuel(int mass) => (mass / 3) - 2;

    static int RecursiveFuel(int mass)
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
