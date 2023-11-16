namespace Advent_of_Code_2019;

[Category(Category.Simulation)]
public class Day_01
{
    [Puzzle(answer: 3291356, O.μs)]
    public int part_one(string input)
        => input.Int32s().Sum(Fuel);

    [Puzzle(answer: 4934153, O.μs)]
    public int part_two(string input)
        => input.Int32s().Sum(RecursiveFuel);

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
