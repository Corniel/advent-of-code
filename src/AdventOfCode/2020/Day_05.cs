namespace Advent_of_Code_2020;

[Category(Category.BitManupilation)]
public class Day_05
{
    [Example(answer: 885, "BBFBBBFRLR")]
    [Puzzle(answer: 998U, O.μs10)]
    public uint part_one(Lines lines) => lines.As(Seat).Max();

    [Puzzle(answer: 676U, O.μs10)]
    public uint part_two(Lines lines)
    {
        var seats = lines.As(Seat).Order().Fix();
        return seats.Where((seat, index) => seats[index + 1] - seat > 1).First() + 1;
    }

    static uint Seat(string line) => Bits.UInt32.Parse(line, ones: "BR", zeros: "FL");
}
