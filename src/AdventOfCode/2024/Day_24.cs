namespace Advent_of_Code_2024;

[Category(Category.ExpressionEvaluation)]
public class Day_24
{
    [Example(answer: 2024, Example._1)]
    [Puzzle(answer: 58367545758258, O.μs10)]
    public long part_one(GroupedLines groups) => Output(Pars(groups));

    [Puzzle(answer: "bpf,fdw,hcc,hqc,qcw,z05,z11,z35", O.ms100)]
    public string part_two(GroupedLines groups) => string.Join(',', Swaps(0, Pars(groups), []).First().Order());

    public IEnumerable<ImmutableArray<string>> Swaps(int bit, Params gates, ImmutableHashSet<string> prev)
    {
        if (bit >= 44) yield return [];

        var curr = prev.Union(Involved(bit, gates));

        if (AdderIsOkay(bit, gates))
        {
            foreach (var swaps in Swaps(bit + 1, gates, curr)) yield return swaps;
        }
        else
        {
            foreach (var l in curr.Where(g => !prev.Contains(g)))
            {
                foreach (var r in gates.Where(g => g.Expr is Binary && !prev.Contains(g.Name)))
                {
                    var mod = Params.New(gates);
                    mod[l] = r.Expr;
                    mod[r.Name] = gates[l];

                    if (AdderIsOkay(bit, mod))
                    {
                        foreach (var swaps in Swaps(bit + 1, mod, prev.Union([l, r.Name])))
                        {
                            yield return swaps.AddRange(l, r.Name);
                        }
                    }
                }
            }
        }
    }

    static IEnumerable<string> Involved(int z, Params gates)
    {
        var q = new Queue<string>([$"z{z:00}"]);
        foreach (var param in q.DequeueAll())
        {
            if (gates.TryGet(param) is Binary expr)
            {
                yield return param;
                q.Enqueue(((Ref)expr.Left).Name);
                q.Enqueue(((Ref)expr.Right).Name);
            }
        }
    }

    static bool AdderIsOkay(int bit, Params gates)
    {
        var hi = 1L << bit;
        var lo = hi >> 1;

        return Adders.All(XorOkay)
            && (bit == 0 || Adders.All(CarryOkay));

        bool XorOkay(Adder a)
        {
            var xor = Output(a.L * hi, a.R * hi, gates) & hi;
            return (xor == hi) == a.Xor;
        }
        bool CarryOkay(Adder a)
        {
            var carry = Output((a.L * hi) | lo, (a.R * hi) | lo, gates) & hi;
            return (carry == hi) == a.Carry;
        }
    }

    record Adder(int L, int R, bool Xor, bool Carry);

    static readonly Adder[] Adders = [new(0, 0, false, true), new(0, 1, true, false), new(1, 0, true, false), new(1, 1, false, true)];

    static long Output(long x, long y, Params pars)
    {
        for (var i = 0; i <= 44; i++)
        {
            pars[$"x{i:00}"] = (x & 1L << i) == 0 ? Expr.Zero : Expr.One;
            pars[$"y{i:00}"] = (y & 1L << i) == 0 ? Expr.Zero : Expr.One;
        }
        return Output(pars);
    }

    static long Output(Params pars)
    {
        using var _ = pars.Cache();

        var n = 0L;
        foreach (var par in pars.Where(p => p.Name[0] is 'z'))
        {
            n |= (par.Expr.TryValue(pars) ?? 0) << par.Name.Int32();
        }
        return n;
    }

    static Params Pars(GroupedLines groups)
    {
        var pars = new Params();

        foreach (var s in groups[0].Select(l => l.Separate(":", " ")))
        {
            pars[s[0]] = s[1] == "1" ? Expr.One : Expr.Zero;
        }
        foreach (var s in groups[1].Select(l => l.SpaceSeparated()))
        {
            pars[s[4]] = Expr.Binary(Expr.Ref(s[0]), s[1], Expr.Ref(s[2]));
        }
        return pars;
    }
}
