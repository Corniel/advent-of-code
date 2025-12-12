namespace Advent_of_Code_2024;

[Category(Category.Computation)]
public class Day_17
{
    [Example(answer: "4,6,3,5,6,3,5,2,1,0", "A: 729; B: 0; C: 0; 0,1,5,4,3,0")]
    [Puzzle(answer: "7,3,1,3,6,3,6,0,2", O.ns100)]
    public string part_one(Longs numbers) => string.Join(',', Run(numbers[0], numbers[3..]));

    [Example(answer: 117440, "A: 2024; B: 0; C: 0; Program: 0,3,5,4,3,0")]
    [Puzzle(answer: 105843716614554, O.ms)]
    public long part_two(Longs numbers)
    {
        var prog = numbers[3..];
        var q = new Queue<State>().EnqueueRange(new State(0, 1));

        foreach (var (a, size) in q.DequeueAll())
        {
            for (var a_ = a << 3; a_ <= ((a << 3) | 7); a_++)
            {
                if (Run(a_, prog).SequenceEqual(prog[^size..]))
                {
                    if (size == prog.Length) return a_;
                    q.Enqueue(new(a_, size + 1));
                }
            }
        }
        throw new NoAnswer();
    }

    /// <remarks>
    /// I did not find the solution without an important hint from: yfilipov
    /// https://www.reddit.com/r/adventofcode/comments/1hg38ah/comment/m2gy9p5/
    /// 
    /// In other words:
    /// * Every digit in the output corresponds to 3 bit in A. 
    /// * Every digit can be found with B and C being zero initially.
    /// * The first 3 bit match digit 0, the 3 bit the last digit.
    /// * Starting at the end cuts of a lot of options.
    /// </remarks>
    static List<long> Run(long A, ImmutableArray<long> program)
    {
        var p = 0; var B = 0L; var C = 0L; var output = new List<long>();

        while (p < program.Length - 1)
        {
            var op = program[p++];
            var code = program[p++];
            var combo = code switch { 4 => A, 5 => B, 6 => C, _ => code };

            switch (op)
            {
                case 0: A = A / (1 << (int)combo); break;
                case 1: B = B ^ code; break;
                case 2: B = combo & 7; break;
                case 3 when A != 0: p = (int)code; break;
                case 4: B ^= C; break;
                case 5: output.Add(combo & 7); break;
                case 6: B = A / (1 << (int)combo); break;
                case 7: C = A / (1 << (int)combo); break;
            }
        }
        return output;
    }

    readonly record struct State(long A, int Size);
}
