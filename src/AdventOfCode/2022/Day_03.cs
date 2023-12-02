namespace Advent_of_Code_2022;

[Category(Category.Cryptography)]
public class Day_03
{
    [Example(answer: 157, "vJrwpWtwJgWrhcsFMMfFFhFp;jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL;PmmdzqPrVvPwwTWBwg;wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn;ttgJtRGJQctTZtZT;CrZsJsPPZsGzwwsLwLmpwMDw")]
    [Puzzle(answer: 7742, O.μs10)]
    public int part_one(Lines input) => input.Select(Shared).Sum();

    [Example(answer: 70, "vJrwpWtwJgWrhcsFMMfFFhFp;jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL;PmmdzqPrVvPwwTWBwg;wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn;ttgJtRGJQctTZtZT;CrZsJsPPZsGzwwsLwLmpwMDw")]
    [Puzzle(answer: 2276, O.μs10)]
    public int part_two(Lines input) => input.ChunkBy(3).Select(Shared).Sum();

    static int Score(char ch) => char.IsUpper(ch) ? ch - 'A' + 27 : ch - 'a' + 1;

    static int Shared(string line)
    {
        var l = line[0..(line.Length / 2)];
        var r = line[(line.Length / 2)..];
        return Score(l.First(r.Contains));
    }

    public static int Shared(IReadOnlyList<string> lines) => Score(lines[0].First(ch => lines[1].Contains(ch) && lines[2].Contains(ch)));
}
