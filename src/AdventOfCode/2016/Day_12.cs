namespace Advent_of_Code_2016;

[Category(Category.ExpressionParsing)]
public class Day_12
{
    [Example(answer: 42, "cpy 41 a;inc a;inc a;dec a;jnz a 2;dec a")]
    [Puzzle(answer: 318003, O.ms)]
    public int part_one(Lines lines) => Run(lines, 0);

    [Puzzle(answer: 9227657, O.ms10)]
    public int part_two(Lines lines) => Run(lines, 1);

    static int Run(Lines lines, int c)
    {
        var mem = new int[] { 0, 0, c, 0 };
        var acts = lines.ToArray(Parse);
        var pt = 0;

        while (pt < acts.Length)
        {
            pt += acts[pt] switch
            {
                Inc inc => inc.Do(mem),
                Dec dec => dec.Do(mem),
                Jnz Jnz => Jnz.Do(mem),
                Jmp jmp => jmp.Value,
                CpyRef rf => rf.Do(mem),
                CpyVal vl => vl.Do(mem),
                _ => throw new NotSupportedException(),
            };
        }
        return mem[0];
    }

    public static object Parse(string line)
    {
        var b = line.Split(' ')[1..];
        return line[..3] switch
        {
            "inc" => new Inc(b[0][0] - 'a'),
            "dec" => new Dec(b[0][0] - 'a'),
            "jnz" => b[0][0] is '1'
                ? new Jmp(b[1].Int32())
                : new Jnz(b[0][0] - 'a', b[1].Int32()),
            _ => b[0].Int32N() is { } val
                ? new CpyVal(val, b[1][0] - 'a')
                : new CpyRef(b[0][0] - 'a', b[1][0] - 'a'),
        };
    }

    record CpyVal(int Val, int Target) { public int Do(int[] mem) { mem[Target] = Val; return 1; } }
    record CpyRef(int Source, int Target) { public int Do(int[] mem) { mem[Target] = mem[Source]; return 1; } }
    record Inc(int Index) { public int Do(int[] mem) { mem[Index]++; return 1; } }
    record Dec(int Index) { public int Do(int[] mem) { mem[Index]--; return 1; } }
    record Jnz(int Index, int Value) { public int Do(int[] mem) => mem[Index] == 0 ? 1 : Value; }
    record Jmp(int Value);
}
