namespace Advent_of_Code_2021;

[Category(Category.ExpressionParsing)]
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
    [Example(answer: 4207, Example._1)]
    [Puzzle(answer: 3725, O.ms10)]
    public int part_one(string input)
    {
        var lines = input.Lines();
        var expression = Pair.Parse(lines[0]);
        for (var i = 1; i < lines.Count; i++)
        {
            expression += Pair.Parse(lines[i]);
        }
        return expression.Magnitude;
    }

    [Example(answer: 4635, Example._1)]
    [Puzzle(answer: 4832, O.ms100)]
    public int part_two(string input)
    {
        var lines = input.Lines();
        return Enumerable
            .Range(0, lines.Count)
            .SelectMany(f => Range(0, lines.Count).Select(s => new { f, s }))
            .Where(pair => pair.f != pair.s)
            .Max(pair => (Pair.Parse(lines[pair.f]) + Pair.Parse(lines[pair.s])).Magnitude);
    }

    abstract class Node : SyntaxNode
    {
        public abstract int Magnitude { get; }
        public abstract bool Split();
        public virtual bool Explode() => false;
    }

    class Pair : Node
    {
        public override int Magnitude => Left.Magnitude * 3 + Right.Magnitude * 2;
        public Node Left => Children<Node>()[0];
        public Node Right => Children<Node>()[1];
        public override bool Split()
        {
            if (Left.Split())
            {
                if (Left is Const l) { ReplaceChild(Left, l.Splitted()); }
                return true;
            }
            else if (Right.Split())
            {
                if (Right is Const r) { ReplaceChild(Right, r.Splitted()); }
                return true;
            }
            else return false;
        }
        public override bool Explode()
        {
            if (Children().All(n => n is Const) && Depth > 4)
            {
                var index = Parent.Children()[0] == this ? 0 : 1;
                var other = index == 0 ? 1 : 0;
                SetNeigbor(index, other, Children<Node>()[other].Magnitude);
                AddTo(index, other, Children<Node>()[index].Magnitude);
                Parent.ReplaceChild(index, new Const(0));
                return true;
            }
            else return Children<Node>().Any(n => n.Explode());

            void SetNeigbor(int index, int other, int val)
            {
                if (Parent.Children()[other] is Const c)
                {
                    Parent.ReplaceChild(other, c.Add(val));
                }
                else
                {
                    Parent.Children()[other].ReplaceChild(index, ((Const)Parent.Children()[other].Children()[index]).Add(val));
                }
            }

            void AddTo(int index, int other, int val)
            {
                var current = Parent;
                var parent = current.Parent;
                while (parent is { } && parent.Children()[index] == current)
                {
                    current = parent;
                    parent = current.Parent;
                }
                if (parent is { })
                {
                    var child = parent.Children()[index];
                    if (child is Const child_c)
                    {
                        parent.ReplaceChild(child_c, child_c.Add(val));
                    }
                    else
                    {
                        while (child is Pair)
                        {
                            child = child.Children()[other];
                        }
                        child.Parent.ReplaceChild(child, ((Const)child).Add(val));
                    }
                }
            }
        }
        public Pair Reduce()
        {
            while (Explode() || Split()) { /* process */ }
            return this;
        }
        public override string ToString() => $"[{Left},{Right}]";
        public static Pair Parse(string line) => new Parser(line).Read() as Pair;
        public static Pair operator +(Pair l, Pair r)
        {
            var add = new Pair();
            add.AddChildren(l, r);
            return add.Reduce();
        }
    }

    class Const : Node
    {
        public Const(int val)
        {
            Magnitude = val;
        }
        public override int Magnitude { get; }

        public override bool Split() => Magnitude > 9;
        public Pair Splitted()
        {
            var splitted = new Pair();
            splitted.AddChildren(new Const(Magnitude / 2), new Const((Magnitude + 1) / 2));
            return splitted;
        }
        public Const Add(int val) => new(Magnitude + val);
        public override string ToString() => Magnitude.ToString();
    }
    class Parser : SyntaxParser
    {
        public Parser(string expresssion) : base(expresssion) => Do.Nothing();

        public Node Read()
        {
            if (ReadAhead() == '[')
            {
                var pair = new Pair();
                ReadChar();
                pair.AddChild(Read());
                Ensure(',');
                pair.AddChild(Read());
                Ensure(']');
                return pair;
            }
            else return new Const(ReadInt32());
        }
    }
}
