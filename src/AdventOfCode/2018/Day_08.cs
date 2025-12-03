namespace Advent_of_Code_2018;

[Category(Category.ExpressionParsing)]
public class Day_08
{
    [Example(answer: 138, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2")]
    [Puzzle(answer: 36027, O.μs100)]
    public int part_one(string str) => new Parser(str).Read().Sum;

    [Example(answer: 66, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2")]
    [Puzzle(answer: 23960, O.μs100)]
    public int part_two(string str) => new Parser(str).Read().Value;

    record Node()
    {
        public readonly List<Node> Children = [];
        public readonly List<int> MetaData = [];
        public int Sum => MetaData.Sum() + Children.Sum(ch => ch.Sum);
        public int Value => Children.NotEmpty
            ? MetaData.Where(i => i <= Children.Count).Sum(i => Children[i - 1].Value)
            : MetaData.Sum();
    }

    class Parser : SyntaxParser
    {
        public Parser(string expresssion) : base(expresssion) => Do.Nothing();

        public Node Read()
        {
            var node = new Node();
            var children = ReadInt32(); ReadWhiteSpace();
            var data = ReadInt32(); ReadWhiteSpace();

            for (var c = 0; c < children; c++)
            {
                node.Children.Add(Read());
            }
            for (var d = 0; d < data; d++)
            {
                node.MetaData.Add(ReadInt32()); ReadWhiteSpace();
            }
            return node;
        }
    }
}
