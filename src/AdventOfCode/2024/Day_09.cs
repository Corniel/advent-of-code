namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public class Day_09
{
    const int Space = -1;

    [Example(answer: 1928, "2333133121414131402")]
    [Puzzle(answer: 6461289671426, O.ms)]
    public long part_one(string str)
    {
        int[] disk = [..str.Select(c => c.Digit()).SelectMany((d, i) => Repeat(i.IsEven() ? i / 2 : Space, d))];
        var pos = 0; var end = disk.Length - 1;

        while (pos < end)
        {
            if (disk[pos] != Space) pos++;
            else if (disk[end] == Space) end--;
            else { disk[pos++] = disk[end]; disk[end--] = Space; }
        }
        return disk.Select((id, i) => id == Space ? 0L : id * i).Sum();
    }

    [Example(answer: 2858, "2333133121414131402")]
    [Puzzle(answer: 6488291456470, O.ms10)]
    public long part_two(string str)
    {
        var files = new Frag[1 + str.Length / 2];
        var space = new Frag[str.Length / 2];
        var start = 0;

        foreach (var s in str.Select((c, i) => new { Size = c.Digit(), Odd = i.IsOdd(), Id = i / 2 }))
        {
            if (s.Odd) space[s.Id] = new(start, s.Size, Space, s.Id);
            else files[^(s.Id + 1)] = new(start, s.Size, s.Id, files.Length - s.Id - 1);
            start += s.Size;
        }

        foreach (var f in files)
        {
            if (space.TakeWhile(s => s.Start < f.Start).FirstOrNone(s => s.Size >= f.Size) is { } s)
            {
                files[f.Index] = f with { Start = s.Start };
                space[s.Index] = s with { Start = s.Start + f.Size, Size = s.Size - f.Size };
            }
        }
        
        return files.Sum(f => (f.Start * f.Size + (f.Size.Sqr() - f.Size) / 2) * f.Id);
    }

    readonly record struct Frag(int Start, int Size, long Id, int Index);
}
