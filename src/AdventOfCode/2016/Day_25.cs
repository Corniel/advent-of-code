namespace Advent_of_Code_2016;

[Category(Category.ExpressionParsing)]
public class Day_25
{
    [Puzzle(answer: 175, O.ns10)]
    public int part_one(Ints numbers)
    {
        // This code do the following:
        // * mutliply the first two numbers copied in to memory
        // * add input
        // * print binary representation of the result
        // * reset
        //
        // So, to solve the puzzle:
        // Find the smallest nummber of the form 0b_1010.. that is larger
        // the delta between the two was the input.
        var i = 1; var prod = numbers[0] * numbers[1];

        while (i < prod) i = i << 1 | (~i & 1);

        return i - prod;
    }

    [Puzzle(answer: 50, "Power required is now 49 stars.")]
    public int part_two(string _) => 50;
}
