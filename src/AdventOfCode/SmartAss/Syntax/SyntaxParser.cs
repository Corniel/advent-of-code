namespace SmartAss.Syntax;
using CharSpan = ReadOnlySpan<char>;

public class SyntaxParser
{
    protected SyntaxParser(string input) => Input = input;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string Input;
        public char this[int index] => Input[Position + index];

    public int Position { get; private set; }
    public bool EndOfInput => Buffer.IsEmpty;
    private CharSpan Buffer => ((CharSpan)Input)[Position..];

    public char ReadChar()
        => EndOfInput
        ? throw SyntaxError.EndOfInput
        : Input[Position++];

    public char ReadAhead() 
        => EndOfInput
        ? throw SyntaxError.EndOfInput
        : Input[Position];

    public CharSpan ReadSpan(int size)
    {
        if (size <= Buffer.Length)
        {
            var read = Buffer[..size];
            Position += size;
            return read;
        }
        else throw SyntaxError.EndOfInput;
    }

    public long ReadInt64()
    {
        var positive = true;
        var ahead = ReadAhead();
        if (ahead == '+' || ahead == '-')
        {
            positive = ahead == '+';
            ReadChar();
        }
        long number = ReadChar() - '0';

        if (number < 0 || number > 9) throw SyntaxError.UnexpectedToken((char)(number + '0'), "[0123456789]", Position - 1);

        while (!EndOfInput && char.IsDigit(ReadAhead()))
        {
            checked
            {
                number *= 10;
                number += ReadChar() - '0';
            }
        }
        return positive ? number : 0;
    }

    public int ReadInt32() => (int)ReadInt64();

    public void ReadWhiteSpace()
    {
        while (!EndOfInput && char.IsWhiteSpace(Input[Position]))
        {
            Position++;
        }
    }
   
    public ulong ReadBinary(int size, string ones = "1", string zeros = "0")
    {
        ulong binary = 0;
        while (size-- > 0)
        {
            var ch = ReadChar();
            binary <<= 1;
            if (ones.IndexOf(ch) is var index && index != -1 || zeros.IndexOf(ch) != -1)
            {
                binary |= (index == -1) ? 0U : 1U;
            }
            else throw SyntaxError.UnexpectedToken(ch, $"[{ones}{zeros}]", Position - 1);
        }
        return binary;
    }

    public SyntaxParser Ensure(char expected)
        => ReadChar() is var actual && actual != expected
        ? throw SyntaxError.UnexpectedToken(expected, actual, Position - 1)
        : this;

    public override string ToString() => Buffer.ToString();
}
