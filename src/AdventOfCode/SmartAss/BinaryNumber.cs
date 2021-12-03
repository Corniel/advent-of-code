namespace SmartAss;

[DebuggerDisplay("{ToString()} ({Size}: {Value})")]
public readonly struct BinaryNumber
{
    public readonly int Size;
    public readonly ulong Value;

    private BinaryNumber(ulong value, int size)
    {
        Size = size;
        Value = value;
    }

    public bool HasFlag(int position) => Bits.UInt64.HasFlag(Value, position);

    public BinaryNumber Flag(int position) => new(Bits.UInt64.Flag(Value, position), Size);

    public override string ToString() => Bits.UInt64.ToString(Value)[^Size..];

    public static BinaryNumber Empty(int size) => new(default, size);

    public static BinaryNumber Parse(string str)
        => new(Bits.UInt64.Parse(str), str.Count(ch => ch == '0' || ch == '1'));
}
