namespace Advent_of_Code_2020;

[Category(Category.BitManupilation)]
public class Day_05
{
    [Example(answer: 885, "BBFBBBFRLR")]
    [Puzzle(answer: 998, O.μs100)]
    public uint part_one(string input) => input.Lines(Seat).Max();

    [Puzzle(answer: 676, O.μs100)]
    public uint part_two(string input)
    {
        var seats = input.Lines(Seat).Order().ToArray();
        return seats.Where((seat, index) => seats[index + 1] - seat > 1).First() + 1;
    }

    static uint Seat(string line) => Bits.UInt32.Parse(line, ones: "BR", zeros: "FL");
}
