namespace Advent_of_Code_2015;

public class Day_10
{
    private const string Example = @"1";

    [Example(answer: 82350, "1")]
    [Puzzle(answer: 329356, "3113322113")]
    public int part_one(string input) => LookAndSay(input, 40);
    
    [Example(answer: 1166642, "1")]
    [Puzzle(answer: 4666278, "3113322113")]
    public long part_two(string input) => LookAndSay(input, 50);

    static int LookAndSay(string input, int rounds)
    {
        var curr = new StringBuilder();
        var prev = new StringBuilder(input);
        for (var round = 0; round < rounds; round++)
        {
            curr.Clear();
            var length = 1;
            for (var pos = 1; pos < prev.Length; pos++)
            {
                if (prev[pos - 1] == prev[pos]) { length++; }
                else
                {
                    curr.Append(length).Append(prev[pos - 1]);
                    length = 1;
                }
            }
            curr.Append(length).Append(prev[^1]);
            prev.Clear().Append(curr);
        }
        return curr.Length;
    }
}
