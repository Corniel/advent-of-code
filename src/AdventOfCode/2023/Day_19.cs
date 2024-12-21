namespace Advent_of_Code_2023;

[Category(Category.ExpressionEvaluation)]
public class Day_19
{
    [Example(answer: 19114, Example._1)]
    [Puzzle(answer: 376008, O.ms10)]
    public int part_one(GroupedLines groups)
        => groups[1].Select(Ranking.Parse).Select(r => Sum("in", Workflows(groups), r)).Sum();

    [Example(answer: 167409079868000, Example._1)]
    [Puzzle(answer: 124078207789312, O.μs100)]
    public long part_two(GroupedLines groups)
    {
        var wfs = Workflows(groups);
        return wfs["in"].Permutations(new(Ranges.All, Ranges.All, Ranges.All, Ranges.All), wfs);
    }

    static Dictionary<string, Workflow> Workflows(GroupedLines groups) => groups[0].Select(Workflow.Parse).ToDictionary(w => w.Name, w => w);

    static int Sum(string name, Dictionary<string, Workflow> wfs, Ranking rank)
    {
        while (name != "R" && name != "A") name = wfs[name].Conditions.First(c => c.Run(rank)).Next;
        return name == "A" ? rank.Sum : 0;
    }

    record Workflow(string Name, Condition[] Conditions)
    {
        public static Workflow Parse(string s)
        {
            var parts = s.Separate('{', '}', ',');
            return new(parts[0], [.. parts[1..].Select(Condition.Parse)]);
        }

        public long Permutations(Ranges ranges, Dictionary<string, Workflow> wfs)
        {
            var p = 0L;
            var remaining = ranges;
            foreach (var c in Conditions)
            {
                if (c is Const) return p + c.Permutations(remaining, wfs);

                var fitler = remaining.Edit(c.Filter, c.Ranking, (l, r) => l.Intersection([r]));
                p += c.Permutations(fitler, wfs);
                remaining = remaining.Edit(c.Filter, c.Ranking, (l, r) => l.Except(r));
            }
            return p;
        }
    }

    record Condition(string Ranking, string Next, Int32Range Filter)
    {
        public long Permutations(Ranges ranges, Dictionary<string, Workflow> wfs)
        {
            if (Next == "A") return ranges.Permutations;
            if (Next == "R") return 0;
            return wfs[Next].Permutations(ranges, wfs);
        }

        public virtual bool Run(Ranking raking) => true;

        public static Condition Parse(string s)
        {
            var parts = s.Separate('<', '>', ':');
            if (parts.Length == 1) return new Const(parts[0]);
            return s.Contains('<')
                ? new LT(parts[0], parts[1].Int32(), parts[2], new(1, parts[1].Int32() - 1))
                : new GT(parts[0], parts[1].Int32(), parts[2], new(parts[1].Int32() + 1, 4000));
        }
    }

    record LT(string Ranking, int Val, string Next, Int32Range Filter) : Condition(Ranking, Next, Filter)
    {
        public override bool Run(Ranking raking) => raking.Val(Ranking) < Val;
    }
    record GT(string Ranking, int Val, string Next, Int32Range Filter) : Condition(Ranking, Next, Filter)
    {
        public override bool Run(Ranking raking) => raking.Val(Ranking) > Val;
    }

    record Const(string Next) : Condition("", Next, default);

    record Ranking(int X, int M, int A, int S)
    {
        public int Val(string name) => name switch { "x" => X, "m" => M, "a" => A, _ => S };

        public int Sum => X + M + A + S;

        public static Ranking Parse(string s) => Ctor.New<Ranking>(s.Int32s());
    }

    record Ranges(Int32Ranges X, Int32Ranges M, Int32Ranges A, Int32Ranges S)
    {
        public static readonly Int32Ranges All = Int32Ranges.New(new Int32Range(1, 4000));

        public long Permutations => 1L * X.Size * M.Size * A.Size * S.Size;

        public Ranges Edit(Int32Range filter, string name, Func<Int32Ranges, Int32Range, Int32Ranges> edit) => name switch
        {
            "x" => this with { X = edit(X, filter) },
            "m" => this with { M = edit(M, filter) },
            "a" => this with { A = edit(A, filter) },
            _ => this with { S = edit(S, filter) }
        };
    }
}
