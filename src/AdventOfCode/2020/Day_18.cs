namespace Advent_of_Code_2020;

[Category(Category.ExpressionParsing)]
public class Day_18
{
    [Example(answer: 71, "1 + 2 * 3 + 4 * 5 + 6")]
    [Example(answer: 26, "2 * 3 + (4 * 5)")]
    [Example(answer: 437, "5 + (8 * 3 + 9 + 3 * 4 * 3)")]
    [Example(answer: 13632, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2")]
    [Puzzle(answer: 21993583522852, O.μs100)]
    public long part_one(Lines lines)
        => lines.As(Parentheses.Parse).Sum(token => token.Value);

    [Example(answer: 51, "1 + (2 * 3) + (4 * (5 + 6))")]
    [Example(answer: 46, "2 * 3 + (4 * 5)")]
    [Example(answer: 1445, "5 + (8 * 3 + 9 + 3 * 4 * 3)")]
    [Example(answer: 669060, "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))")]
    [Example(answer: 23340, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2")]
    [Puzzle(answer: 122438593522757, O.μs100)]
    public long part_two(Lines lines)
        => lines.As(Parentheses.Parse).Sum(token => token.Parenthese().Value);

    private interface Token { }
    private interface ValueToken : Token { long Value { get; } }
    private record Parentheses(Token[] Tokens) : ValueToken
    {
        public long Value
        {
            get
            {
                long total = this[0].Value;
                for (var i = 2; i < Tokens.Length; i += 2)
                {
                    if (Tokens[i - 1] is Add) { total += this[i].Value; }
                    else { total *= this[i].Value; }
                }
                return total;
            }
        }
        private ValueToken this[int index] => (ValueToken)Tokens[index];
        public Parentheses Parenthese()
        {
            var tokens = Tokens.ToList();
            for (var o = 1; o < tokens.Count; o += 2)
            {
                var l = tokens[o - 1] is Parentheses l_o ? l_o.Parenthese() : (ValueToken)tokens[o - 1];
                var r = tokens[o + 1] is Parentheses r_o ? r_o.Parenthese() : (ValueToken)tokens[o + 1];

                if (tokens[o] is Add)
                {
                    tokens.RemoveRange(o - 1, 3);
                    tokens.Insert(o - 1, new Addition(l, r));
                    o -= 2;
                }
                else
                {
                    tokens[o - 1] = l;
                    tokens[o + 1] = r;
                }
            }
            return new Parentheses([.. tokens]);
        }
        public static Parentheses Parse(string str)
        {
            var tokens = new List<Token>();
            foreach (var ch in str.StripChars(" \r\n"))
            {
                if (char.IsDigit(ch)) { tokens.Add(new Number(ch - '0')); }
                else if (ch == '+') { tokens.Add(new Add()); }
                else if (ch == '*') { tokens.Add(new Multiply()); }
                else if (ch == '(') { tokens.Add(new ParentheseOpen()); }
                else if (ch == ')') { tokens.Add(new ParentheseClose()); }
            }
            while (tokens.Exists(t => t is ParentheseOpen || t is ParentheseClose))
            {
                var start = 0;
                for (var pos = 0; pos < tokens.Count; pos++)
                {
                    if (tokens[pos] is ParentheseOpen)
                    {
                        start = pos;
                    }
                    else if (tokens[pos] is ParentheseClose)
                    {
                        var elements = 1 + pos - start;
                        var operation = new Parentheses(tokens.Skip(start + 1).Take(elements - 2).ToArray());
                        tokens.RemoveRange(start, elements);
                        tokens.Insert(start, operation);
                        break;
                    }
                }
            }
            return new Parentheses([.. tokens]);
        }
    };
    private record Addition(ValueToken Left, ValueToken Right) : ValueToken
    {
        public long Value => Left.Value + Right.Value;
    }
    private record Number(long Value) : ValueToken { }
    private readonly struct Add : Token { }
    private readonly struct Multiply : Token { }
    private readonly struct ParentheseOpen : Token { }
    private readonly struct ParentheseClose : Token { }
}
