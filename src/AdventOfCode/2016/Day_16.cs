namespace Advent_of_Code_2016;

[Category(Category.SequenceProgression)]
public class Day_16
{
    [Example(answer: "100", "110010110100", 12)]
    [Puzzle(answer: "10101001010100001", "10001001100000001", 272, O.ns100)]
    public string part_one(string str, int size)
    {
        var dat = new bool[size];
        var len = str.Length;

        for (var i = 0; i < len; i++) dat[i] = str[i] == '1';

        while (len < size)
        {
            var i = -1 + len++;
            while (i >= 0 && len < size) dat[len++] = !dat[i--];
        }

        while (len.IsEven())
        {
            for (var i = 0; i < dat.Length; i += 2) dat[i >> 1] = dat[i] == dat[i + 1];
            len >>= 1;
        }

        return new([.. dat[..len].Select(x => x ? '1' : '0')]);
    }
    
    [Puzzle(answer: "10100001110101001", "10001001100000001", 35651584, O.ms100)]
    public string part_two(string str, int max) => part_one(str, max);
}
