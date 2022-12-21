﻿
namespace Advent_of_Code_2018;

[Category(Category.Cryptography)]
public class Day_05
{
    [TestCase("dabAcCaCBAcCcaDA", "dabCBAcaDA")]
    public void Reduces(string polymer, string reduced) => Destroy(polymer).Should().Be(reduced);

    [Example(answer: 10, "dabAcCaCBAcCcaDA")]
    [Puzzle(answer: 11264)]
    public int part_one(string input) => Destroy(input).Length;

    [Example(answer: 4, "dabAcCaCBAcCcaDA")]
    [Puzzle(answer: 4552, O.s)]
    public int part_two(string input)
        => Characters.a_z.Select(ch => Strip(input, Lower(ch)))
        .Select(reduced => Destroy(reduced))
        .Min(polymer => polymer.Length);

    public static string Destroy(string polymer)
    {
        var buffer = new char[polymer.Length];
        buffer[0] = polymer[0];
        var length = 1;
        var check = true;

        foreach (var pair in polymer.SelectWithPrevious<char>())
        {
            if (check && IsDestroy(pair.Previous, pair.Current))
            {
                length--;
                check = false;
            }
            else
            {
                buffer[length++] = pair.Current;
                check = true;
            }
        }
        return polymer.Length == length ? polymer : Destroy(new(buffer, 0, length));
    }
    public static string Strip(string polymer, int ch) => new(polymer.Where(c => Lower(c) != ch).ToArray());
    private static bool IsDestroy(char l, char r) => l != r && Lower(l) == Lower(r);
    private static int Lower(char ch) => ch & 0b11111;
}
