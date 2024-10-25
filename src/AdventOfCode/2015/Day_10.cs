namespace Advent_of_Code_2015;

[Category(Category.SequenceProgression)]
public class Day_10
{
    [Example(answer: 82350, "1")]
    [Puzzle(answer: 329356, "3113322113", O.ms)]
    public int part_one(string str) => LookAndSay(str, 40);
    
    [Example(answer: 1166642, "1")]
    [Puzzle(answer: 4666278, "3113322113", O.ms100)]
    public int part_two(string str) => LookAndSay(str, 50);

    static int LookAndSay(string str, int rounds)
    {
        var curr = new StringBuilder();
        var prev = new StringBuilder(str);
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
