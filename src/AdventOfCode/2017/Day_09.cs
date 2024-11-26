namespace Advent_of_Code_2017;

[Category(Category.ExpressionEvaluation)]
public class Day_09
{
    [Example(answer: 1, "{}")]
    [Example(answer: 1 + 2, "{{}}")]
    [Example(answer: 1 + 2 + 3, "{{{}}}")]
    [Example(answer: 1 + 2 + 2, "{{},{}}")]
    [Example(answer: 1 + 2 + 3 + 3 + 3 + 4, "{{{},{},{{}}}}")]
    [Example(answer: 1, "{<a>,<a>,<a>,<a>}")]
    [Example(answer: 1 + 2 + 2 + 2 + 2, "{{<ab>},{<ab>},{<ab>},{<ab>}}")]
    [Puzzle(answer: 9662, O.μs100)]
    public int part_one(string str) => new Parser(str).Input().Score;

    [Example(answer: 17, "{<random characters>}")]
    [Example(answer: 2, "{<{!>}>}")]
    [Example(answer: 3, "{<<<<>}")]
    [Puzzle(answer: 4903, O.μs100)]
    public int part_two(string str) => new Parser(str).Input().Garbage;

    class Input : SyntaxNode
    {
        public int Score => Children<Group>().Single().Scores;
        public int Garbage => Children<Group>().Single().Garbages;
    }

    class Group : SyntaxNode
    {
        public SyntaxNodes<Group> ChildGroups => Children<Group>();
        public int Score => Parent is Group parent ? parent.Score + 1 : 1;
        public int Scores => Score + ChildGroups.Sum(ch => ch.Scores);
        public int Garbage { get; set; }
        public int Garbages => Garbage + ChildGroups.Sum(ch => ch.Garbages);
    }

    class Parser(string str) : SyntaxParser(str)
    {
        public Input Input()
        {
            var stream = new Input();
            Ensure('{');
            stream.AddChild(Read());
            return stream;
        }

        private Group Read()
        {
            var group = new Group();
            var escaped = false;
            var garbage = false;

            while (TryReadChar() is char ch)
            {
                if (garbage)
                {
                    if (escaped) escaped = false;
                    else if (ch == '>') garbage = false;
                    else if (ch == '!') escaped = true;
                    else group.Garbage++;
                }
                else
                {
                    if (ch == '{') group.AddChild(Read());
                    else if (ch == '}') return group;
                    else if (ch == '<') garbage = true;
                }
            }
            throw SyntaxError.UnexpectedToken(ReadChar(), Position);
        }
    }
}
