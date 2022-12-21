﻿using System.Security.Cryptography;

namespace Advent_of_Code_2015;

[Category(Category.Cryptography)]
public class Day_04
{
    
    [Example(answer: 609043, "abcdef")]
    [Puzzle(answer: 346386, "iwrupvqb", O.ms10)]
    public int part_one(string input) => Run(input, FiveZeros);

    [Puzzle(answer: 9958218, "iwrupvqb", O.s)]
    public int part_two(string input) => Run(input, SixZeros);

    private static readonly HashAlgorithm Hash = MD5.Create();

    private static int Run(string input, Func<byte[], bool> condition)
    {
        for (var n = 0; n < int.MaxValue; n++)
        {
            var bytes = Encoding.UTF8.GetBytes(input + n.ToString());
            var hashed = Hash.ComputeHash(bytes);
            if (condition(hashed))
            {
                return n;
            }
        }
        throw new InfiniteLoop();
    }

    private static bool FiveZeros(byte[] hashed)
        => hashed[0] == 0 
        && hashed[1] == 0 
        && hashed[2] != 0 && hashed[2] <= 15;

    private static bool SixZeros(byte[] hashed)
        => hashed[0] == 0
        && hashed[1] == 0
        && hashed[2] == 0;
}
