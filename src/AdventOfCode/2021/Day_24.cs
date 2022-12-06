namespace Advent_of_Code_2021;

[Category(Category.ExpressionParsing)]
public class Day_24
{
    [Puzzle(answer: 39999698799429)]
    public long part_one(string input) => Serial(Desc, State.Zero, input.Lines(Expression.Parse).ToArray()) ?? throw new NoAnswer();

    [Puzzle(answer: 18116121134117)]
    public long part_two(string input) => Serial(Asc, State.Zero, input.Lines(Expression.Parse).ToArray()) ?? throw new NoAnswer();

    static readonly int[] Asc = Range(1, 9).ToArray();
    static readonly int[] Desc = Asc.Reverse().ToArray();

    long? Serial(int[] digits, State state, IReadOnlyList<Expression> exps, long serial = 0, int level = 0, int index = 0)
    {
        var seri = serial * 10;
        foreach(var digit in digits)
        {
            var index_next = -1;
            var state_next = State.Zero;
            var test = state.Exc(exps[index].Set(digit));

            for (var i = index + 1; i < exps.Count; i++)
            {
                var exp = exps[i];
                if (index_next == -1 && exp.Op == Op.inp) 
                {
                    state_next = test;
                    index_next = i; 
                }
                test = test.Exc(exp);
            }
            if (test.IsValid)
            {
                if (level == 13) return serial * 10 + digit;
                else if (Serial(digits, state_next, exps, seri + digit, level + 1, index_next) is { } result) return result;
            }
        }
        return null;
    }

    record Expression(Op Op, Arg[] Args)
    {
        public Arg this[int index] => index >= Args.Length ? 0 : Args[index];
        public Expression Set(int val) => this with { Op = Op.set, Args = new[] { Args[0], (Arg)val } };
        public override string ToString()=> $"{Op} {string.Join(' ', Args.AsEnumerable())}";
        public static Expression Parse(string line)
        {
            var args = line.SpaceSeparated().ToArray();
            return new(Enum.Parse<Op>(args[0]), args[1..].Select(ParseArgument).ToArray());
        }
        static Arg ParseArgument(string str) => Enum.Parse<Arg>(str);
    }

    enum Op { inp, add, mul, div, mod, eql, set }
    enum Arg : long { w = 'w', x = 'x', y = 'y', z = 'z' }

    record State(Span w, Span x, Span y, Span z)
    {
        public Span this[Arg arg] => arg switch { Arg.w => w, Arg.x => x, Arg.y => y, Arg.z => z, _ => new((int)arg) };
        public bool IsValid => z.Min <= 0 && z.Max >= 0;

        public State With(Expression exp, Span span) => With(exp[0], span);
        public State With(Arg arg, Span span) => arg switch
        {
            Arg.w => this with { w = span },
            Arg.x => this with { x = span },
            Arg.y => this with { y = span },
            Arg.z => this with { z = span },
            _ => throw new InvalidOperationException()
        };
        public State Exc(Expression exp) => exp.Op switch
        {
            Op.inp => With(exp, new(1, 9)),
            Op.add => With(exp, this[exp[0]].Add(this[exp[1]])),
            Op.mul => With(exp, this[exp[0]].Mul(this[exp[1]])),
            Op.div => With(exp, this[exp[0]].Div(this[exp[1]])),
            Op.mod => With(exp, this[exp[0]].Mod(this[exp[1]])),
            Op.eql => With(exp, this[exp[0]].Eql(this[exp[1]])),
            Op.set => With(exp, exp.Args.Length == 2 ? new((long)exp[1]) : this[exp[1]]),
            _ => throw new InvalidOperationException(),
        };
        public override string ToString() => $"w: {w}, x: {x}, y: {y}, z: {z}";

        public static readonly State Zero = new(new(0), new(0), new(0), new(0));
    }

    record Span(long Min, long Max)
    {
        public Span(long val) : this(val, val) => Do.Nothing();

        public bool IsSingle => Min == Max;

        public Span Add(Span other) => new(Min + other.Min, Max + other.Max);
        public Span Mul(Span other) => new(Min * other.Min, Max * other.Max);
        public Span Div(Span other) => new(Min / other.Max, Max / other.Min);
        public Span Mod(Span other)
            => other.IsSingle
            ? new(Min % other.Min, Max % other.Max)
            : new(0, other.Max - 1);
        public Span Eql(Span other)
        {
            if (IsSingle && other.IsSingle) return Min == other.Min ? new(1) : new(0);
            else if (Max < other.Min || Min > other.Max) return new(0);
            else return new(0, 1);
        }
        public override string ToString()=> IsSingle ? Min.ToString(): $"[{Min}, {Max}]";
    }
}
