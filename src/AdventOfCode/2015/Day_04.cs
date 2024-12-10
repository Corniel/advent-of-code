namespace Advent_of_Code_2015;

[Category(Category.Cryptography)]
public class Day_04
{

    [Example(answer: 609043, "abcdef")]
    [Puzzle(answer: 346386, "iwrupvqb", O.ms10)]
    public int part_one(string str) => Run(str, FiveZeros);

    [Puzzle(answer: 9958218, "iwrupvqb", O.s)]
    public int part_two(string str) => Run(str, SixZeros);

    static int Run(string str, Func<byte[], bool> condition)
    {
        for (var n = 0; n < int.MaxValue; n++)
        {
            var bytes = Encoding.UTF8.GetBytes(str + n.ToString());
            var hashed = Hashing.MD5.ComputeHash(bytes);
            if (condition(hashed))
            {
                return n;
            }
        }
        throw new InfiniteLoop();
    }

    static bool FiveZeros(byte[] hashed)
        => hashed[0] == 0
        && hashed[1] == 0
        && hashed[2] != 0 && hashed[2] <= 15;

    static bool SixZeros(byte[] hashed)
        => hashed[0] == 0
        && hashed[1] == 0
        && hashed[2] == 0;
}
