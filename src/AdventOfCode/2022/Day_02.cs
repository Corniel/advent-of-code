namespace Advent_of_Code_2022;

[Category(Category.Simulation)]
public class Day_02
{
    [Example(answer: 15, "A Y;B X;C Z")]
    [Puzzle(answer: 12458, O.μs10)]
    public int part_one(Lines lines) => lines.As(One).Sum();

    static int One(string battle) => battle switch
    {
        "B X" => 1 + 0, "C Y" => 2 + 0, "A Z" => 3 + 0,
        "A X" => 1 + 3, "B Y" => 2 + 3, "C Z" => 3 + 3,
        "C X" => 1 + 6, "A Y" => 2 + 6, _ /*"B Z"*/ => 3 + 6
    };

    [Example(answer: 12, "A Y;B X;C Z")]
    [Puzzle(answer: 12683, O.μs)]
    public int part_two(Lines lines) => lines.As(Two).Sum();

    static int Two(string battle) => battle switch
    {
        "B X" => 1 + 0, "C X" => 2 + 0, "A X" => 3 + 0,
        "A Y" => 1 + 3, "B Y" => 2 + 3, "C Y" => 3 + 3,
        "C Z" => 1 + 6, "A Z" => 2 + 6, _ /* B Z"*/ => 3 + 6
    };
}
