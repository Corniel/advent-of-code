namespace Advent_of_Code_2017;

[Category(Category.Cryptography)]
public class Day_04
{
    [Puzzle(answer: 383, O.μs100)]
    public int part_one(Lines lines) => lines.Count(IsPrhase);
    
    [Puzzle(answer: 265, O.μs100)]
    public int part_two(Lines lines) => lines.Count(NoAnagrams);

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
