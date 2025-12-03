namespace Advent_of_Code_2015;

[Category(Category.ExpressionEvaluation)]
public class Day_23
{
    [Puzzle(answer: 255, O.μs)]
    public int part_one(Lines lines) => Run(lines, [0, 0]);
    
    [Puzzle(answer: 334, O.μs)]
    public int part_two(Lines lines) => Run(lines, [1, 0]);

    static int Run(Lines lines, int[] vals)
    {
        var i = 0; var insts = lines.ToArray(Inst.Parse);
        while (i.InRange(0, insts.Length - 1))
        {
            var inst = insts[i]; var r = inst.Register;
            switch (inst.Type)
            {
                case Type.hlf: vals[r] /= 2; i++; break;
                case Type.tpl: vals[r] *= 3; i++; break;
                case Type.inc: vals[r] += 1; i++; break;
                case Type.jmp: i += inst.Val; break;
                case Type.jie: i += vals[r].IsEven ? inst.Val : 1; break;
                case Type.jio: i += vals[r] == 1 ? inst.Val : 1; break;
            }
        }
        return vals[1];
    }

    enum Type { hlf, tpl, inc, jmp, jie, jio }

    record Inst(Type Type, int Register, int Val)
    {
        public static Inst Parse(string l) => new(Enum.Parse<Type>(l[..3]), l[4] - 'a', l.Int32N() ?? 0);
    }
}
