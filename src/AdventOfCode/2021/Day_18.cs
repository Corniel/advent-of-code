namespace Advent_of_Code_2021;

public partial class Day_18
{
    [Example(answer: 3488, @"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]")]
    [Example(answer: 4207, year: 2021, day: 18, example: 1)]
    [Puzzle(answer: 3725, year: 2021, day: 18)]
    public int part_one(string input)
    {
        var lines = input.Lines();
        var expression = Pair.Parse(lines[0]);
        for (var i = 1; i < lines.Count; i++)
        {
            expression = expression.Add(Pair.Parse(lines[i])).Reduce();
        }
        return expression.Magnitude;
    }

    [Example(answer: 4635, year: 2021, day: 18, example: 1)]
    [Puzzle(answer: 4832, year: 2021, day: 18)]
    public int part_two(string input)
    {
        var lines = input.Lines();
        return Enumerable
            .Range(0, lines.Count)
            .SelectMany(f => Enumerable.Range(0, lines.Count).Select(s => new { f, s }))
            .Where(pair => pair.f != pair.s)
            .Max(pair => Pair.Parse(lines[pair.f]).Add(Pair.Parse(lines[pair.s])).Reduce().Magnitude);
    }

    [TestCase(143, "[[1,2],[[3,4],5]]")]
    [TestCase(1384, "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [TestCase(445, "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
    public void Has_Magnitude(int magnitude, string expression)
       => Pair.Parse(expression).Magnitude.Should().Be(magnitude);

    [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]\r\n[1,1]", "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]")]
    public void Can_Add(string expression, string updated)
    {
        var lines = expression.Lines();
        var left = Pair.Parse(lines[0]);
        var right = Pair.Parse(lines[1]);
        left.Add(right).ToString().Should().Be(updated);
    }

    [TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
    [TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
    public void Can_Split(string expression, string updated)
    {
        var node = Pair.Parse(expression);
        node.Split(out _);
        node.ToString().Should().Be(updated);
    }

    [TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]")]
    [TestCase("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[15,[0,13]]],[1,1]]")]
    [TestCase("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [TestCase(
        "[[[[4,0],[5,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]",
        "[[[[4,0],[5,4]],[[0,[7,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]")]
    [TestCase(
        "[[[[4,0],[5,4]],[[0,[7,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]",
        "[[[[4,0],[5,4]],[[7,0],[15,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]")]
    [TestCase(
        "[[[[4,0],[5,4]],[[7,0],[15,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]",
        "[[[[4,0],[5,4]],[[7,0],[15,5]]],[10,[[0,[11,3]],[[6,3],[8,8]]]]]")]
    [TestCase(
        "[[[[0,1],7],[[11,0],[8,6]]],[[[[2,0],5],7],[[[3,1],[2,6]],[[0,8],6]]]]",
        "[[[[0,1],7],[[11,0],[8,8]]],[[[0,5],7],[[[3,1],[2,6]],[[0,8],6]]]]")]
    [TestCase(
        "[[[[7,14],[13,0]],[[[14,9],[8,9]],[[9,8],[0,8]]]],[[9,[9,8]],[[1,[9,1]],[2,5]]]]",
        "[[[[7,14],[13,14]],[[0,[17,9]],[[9,8],[0,8]]]],[[9,[9,8]],[[1,[9,1]],[2,5]]]]")]
    public void Can_Explode(string expression, string updated)
    {
        var node = Pair.Parse(expression);
        node.Explode();
        node.ToString().Should().Be(updated, because: expression);
    }

    [TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [TestCase(
        "[[[[4,0],[5,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]",
        "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
    public void Can_Reduce(string expression, string updated)
        => Pair.Parse(expression).Reduce().ToString().Should().Be(updated);

    abstract class Node
    {
        protected Node(Pair parent) => Parent = parent;
        
        public Pair Parent { get; private set; }
        public int Depth => (Parent?.Depth + 1) ?? 1;
        public abstract int Magnitude { get; }
        
        public Pair Add(Node other)
        {
            var add = new Pair();
            Parent = add;
            other.Parent = add;
            return add.With(this, other);
        }
        public virtual bool Split(out Pair splitted)
        {
            splitted = this as Pair;
            return false;
        }
        public virtual bool Explode() => false;
    }

    class Pair : Node
    {
        public Pair(Pair parent = null, Node left = null, Node right = null) : base(parent)
        {
            Left = left;
            Right = right;
        }
       
        public Node Left { get; private set; }
        public Node Right { get; private set; }
        public override int Magnitude => Left.Magnitude * 3 + Right.Magnitude * 2;

        public override bool Split(out Pair splitted)
        {
            if (Left.Split(out splitted))
            {
                Left = splitted;
                splitted = this;
                return true;
            }
            else if (Right.Split(out splitted))
            {
                Right = splitted;
                splitted = this;
                return true;
            }
            else return false;
        }
        public override bool Explode()
        {
            if (Depth > 4 && Left is Const && Right is Const)
            {
                var l = Left.Magnitude;
                var r = Right.Magnitude;

                if (Parent.Left == this)
                {
                    Parent.Left = Const.Zero(Parent);
                    Parent.Set(left: false, r);
                    Ancestor(left: true, l);
                }
                else
                {
                    Parent.Right = Const.Zero(Parent);
                    Parent.Set(left: true, l);
                    Ancestor(left: false, r);
                }
                return true;
            }
            else return Left.Explode() || Right.Explode();

            void Ancestor(bool left, int val)
            {
                Func<Pair, Node> select = left ? SelectLeft : SelectRight;

                var current = Parent;
                var parent = current?.Parent;
                while (parent is { } && select(parent) == current)
                {
                    current = parent;
                    parent = current.Parent;
                }
                if (parent is { })
                {
                    parent.Set(left, val);
                }
            }

        }
        public Pair With(Node left, Node right)
        {
            Left = left;
            Right = right;
            return this;
        }
        public Pair Reduce()
        {
            while (Explode() || Split(out _)) { /* process */ }
            return this;
        }
        public override string ToString() => $"[{Left},{Right}]";

        void Set(bool left, int val)
        {
            Func<Pair, Node> select = left ? SelectLeft : SelectRight;
            Func<Pair, Node> other = !left ? SelectLeft : SelectRight;
            Action<Pair, Node> set = left ? SetLeft : SetRight;

            var parent = this;
            var child = select(parent);
            if (child is Const c)
            {
                set(this, c.Add(val));
            }
            else
            {
                while (child is Pair pair)
                {
                    child = other(pair);
                    parent = pair;
                }
                parent.Set(!left, val);
            }
        }
        static void SetLeft(Pair p, Node n) => p.Left = n;
        static void SetRight(Pair p, Node n) => p.Right = n;
        static Node SelectLeft(Pair p) => p.Left;
        static Node SelectRight(Pair p) => p.Right;

        public static Pair Parse(string line) => new Parser(line).Read() as Pair;
    }

    class Const : Node
    {
        public Const(Pair parent, int val) : base(parent)
        {
            Magnitude = val;
        }
        public override int Magnitude { get; }

        public override bool Split(out Pair splitted)
        {
            splitted = null;
            if (Magnitude > 9)
            {
                var pair = new Pair(Parent);
                var left = new Const(pair, Magnitude / 2);
                var right = new Const(pair, (Magnitude + 1) / 2);
                splitted = pair.With(left, right);
                return true;
            }
            else return false;
        }
        public Const Add(int val) => new(Parent, Magnitude + val);
        public override string ToString() => Magnitude.ToString();

        public static Const Zero(Pair parent) => new(parent, 0);
    }

    class Parser
    {
        public Parser(string expresssion) => Expression = expresssion;
        readonly string Expression;
        int Pos = 0;

        public Node Read(Pair parent = null)
        {
            var first = Next();
            if (char.IsDigit(first))
            {
                int val = first - '0';
                while (char.IsDigit(Expression[Pos]))
                {
                    val *= 10;
                    val += Next() - '0';
                }
                return new Const(parent, val);
            }
            else if (first == '[')
            {
                var pair = parent is null ? new Pair() : new Pair(parent);
                var left = Read(pair);
                _ = Next(); // ,
                var right = Read(pair);
                _ = Next(); // ]
                return pair.With(left, right);
            }
            else throw new FormatException();
        }
        char Next() => Expression[Pos++];
        public override string ToString() => Expression[Pos..];
    }
}
