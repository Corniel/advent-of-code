using System.Security.Cryptography;

namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_14
{
    [Example(answer: 22728, "abc")]
    [Puzzle(answer: 23890, "ahsbgdzn", O.ms)]
    public int part_one(string str) => Find(str, 0);

    [Puzzle(answer: 22696, "ahsbgdzn", O.s)]
    public int part_two(string str) => Find(str, 2016);

    static int Find(string str, int repeat)
    {
        var seed = Encoding.ASCII.GetBytes(str);
        var src = new byte[32];
        var tar = new byte[32];
        var key = 0;
        var keys = 0;
        var max = -1;
        Cache[0] = Hash(src, Seed(src, seed, 0), tar, repeat);

        while (keys < 64)
        {
            if (Cache[key & 1023] is { Byte: not byte.MaxValue } scan)
            {
                for (var nxt = key + 1; nxt < key + 1000; nxt++)
                {
                    var test = Cache[nxt & 1023];

                    if (max < nxt)
                    {
                        test = Hash(src, Seed(src, seed, nxt), tar, repeat);
                        Cache[nxt & 1023] = test;
                        max = nxt;
                    }
                    if ((test.Flags & (1 << scan.Byte)) is not 0)
                    {
                        keys++;
                        break;
                    }
                }
            }
            key++;
        }
        return key - 1;
    }

    static int Seed(byte[] src, byte[] seed, int index)
    {
        Array.Copy(seed, src, seed.Length);
        index.TryFormat(src.AsSpan()[seed.Length..], out var written);
        return seed.Length + written;
    }

    static Scan Hash(byte[] seed, int length, byte[] hash, int repeat)
    {
        int pos;
        MD5.HashData(seed.AsSpan()[..length], hash);

        for (var r = 0; r < repeat; r++)
        {
            (hash, seed) = (seed, hash);

            pos = 31;
            for (var i = 15; i >= 0; i--)
            {
                var b = seed[i];
                seed[pos--] = Ascii[b & 15];
                seed[pos--] = Ascii[b >> 4];
            }

            MD5.HashData(seed, hash);
        }

        pos = 31;
        for (var i = 15; i >= 0; i--)
        {
            var b = hash[i];
            hash[pos--] = (byte)(b & 15);
            hash[pos--] = (byte)(b >> 4);
        }
        return Scans(hash);
    }

    static Scan Scans(ReadOnlySpan<byte> hash)
    {
        var flags = 0;
        byte? bits = null;
        var prev = hash[0];
        var len = 1;
        for (var i = 1; i < 32; i++)
        {
            var curr = hash[i];
            if (curr == prev)
            {
                if (++len is 3) bits ??= curr;
                if (len is 5) flags |= 1 << curr;
            }
            else len = 1;
            prev = curr;
        }
        return new(bits ?? byte.MaxValue, flags);
    }

    static readonly Scan[] Cache = new Scan[1024];
    static readonly byte[] Ascii = [.. "0123456789abcdef".Select(c => (byte)c)];
    readonly record struct Scan(byte Byte, int Flags);
}
