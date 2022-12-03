namespace Advent_of_Code_2017;

[Category(Category.Cryptography)]
public class Day_04
{
    [Puzzle(answer: 383)]
    public int part_one(string input) => input.Lines().Count(IsPrhase);
    
    [Puzzle(answer: 265)]
    public int part_two(string input) => input.Lines().Count(NoAnagrams);

    static bool IsPrhase(string line)
    {
        var set = new HashSet<string>();
        return line.SpaceSeparated().All(set.Add);
    }

    static bool NoAnagrams(string line)
    {
        var set = new HashSet<string>();
        return line.SpaceSeparated().Select(Ordered).All(set.Add);
    }

    static string Ordered(string word) => new(word.OrderBy(c => c).ToArray());
}
