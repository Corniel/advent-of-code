using SmartAss.Parsing;

namespace Advent_of_Code_2022;

[Category(Category.Cryptography)]
public class Day_03
{
    [Example(answer: 157, "vJrwpWtwJgWrhcsFMMfFFhFp;jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL;PmmdzqPrVvPwwTWBwg;wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn;ttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw")]
    [Puzzle(answer: 7742)]
    public int part_one(string input) => input.Lines(Shared).Sum();

    [Example(answer: 70, "vJrwpWtwJgWrhcsFMMfFFhFp;jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL;PmmdzqPrVvPwwTWBwg;wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn;ttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw")]
    [Puzzle(answer: 2276)]
    public int part_two(string input) => input.Lines().ChunkBy(3).Select(Shared).Sum();

    static int Score(char ch) => char.IsUpper(ch) ? ch - 'A' + 27 : ch - 'a' + 1;

    static int Shared(string line)
    {
        var l = line[0..(line.Length / 2)];
        var r = line[(line.Length / 2)..];
        return Score(l.First(r.Contains));
    }

    public static int Shared(IReadOnlyList<string> lines) => Score(lines[0].First(ch => lines[1].Contains(ch) && lines[2].Contains(ch)));
}