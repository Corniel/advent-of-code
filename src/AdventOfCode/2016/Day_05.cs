using System.Security.Cryptography;

namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_05
{
    static readonly MD5 md5 = MD5.Create();

    [Example(answer: "18f47a30", "abc")]
    [Puzzle(answer: "d4cd2ee1", "ugkcyxxp", O.s)]
    public string part_one(string str) => new(Hash(str).Take(8).Select(hash => "0123456789abcdef"[hash[0]]).ToArray());

    [Example(answer: "05ace8e3", "abc")]
    [Puzzle(answer: "f2c730e5", "ugkcyxxp", O.s)]
    public string part_two(string str)
    {
        var password = new char[8];
        foreach (var hash in Hash(str))
        {
            if (hash[0] is var pos && pos < 8 && password[pos] == default)
            {
                password[pos] = "0123456789abcdef"[hash[1] >> 4];
                if (!password.Exists(c => c == default)) return new(password);
            }
        }
        throw new InfiniteLoop();
    }

    static IEnumerable<byte[]> Hash(string str)
        => Range(0, int.MaxValue)
            .Select(index => md5.ComputeHash(Encoding.ASCII.GetBytes($"{str}{index}")))
            .Where(bytes => bytes[0] == 0 && bytes[1] == 0 && bytes[2] < 16)
            .Select(bytes => bytes.AsSpan().Slice(2, 2).ToArray());
}
