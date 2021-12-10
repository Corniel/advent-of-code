namespace Advent_of_Code_2015;

public class Day_08
{
    [Example(answer: 7, @"""x\""\xcaj\\xwwvpdldz""")]
    [Puzzle(answer: 1342, year: 2015, day: 08)]
    public int part_one(string input)
        => input.Lines().Sum(line => line.Length - Decode(line));

    [Example(answer: 16 - 10, @"""aaa\""aaa""")]
    [Example(answer: 11 - 6, @"""\x27""")]
    [Puzzle(answer: 2074, year: 2015, day: 08)]
    public int part_two(string input)
        => input.Lines().Sum(line => Encode(line) - line.Length);
   
    static int Decode(string str)
    {
        var length = 0;
        var escape = false;

        for (var i = 1; i < str.Length - 1; i++)
        {
            if (str[i] == '\\')
            {
                if (escape) { length++; }
                escape = !escape;
            }
            else
            {
                length++;
                i += str[i] == 'x' && escape ? 2 : 0;
                escape = false;
            }
        }
        return  length;
    }

    static int Encode(string str)
        => str.Length + 4 + str[1..^1].Count(ch => ch == '"' || ch == '\\');
}
