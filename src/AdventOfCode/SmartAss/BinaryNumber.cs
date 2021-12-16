namespace SmartAss;

[DebuggerDisplay("{ToString()} ({Size}: {Value})")]
public readonly struct BinaryNumber : IEquatable<BinaryNumber>
{
    public readonly int Size;
    public readonly ulong Value;

    public BinaryNumber(ulong value, int size)
    {
        Size = size;
        Value = value;
    }

    public int Count => Bits.UInt64.Count(Value);

    public bool IsEmpty() => Value == 0;

    public bool HasFlag(int position) => Bits.UInt64.HasFlag(Value, position);

    public BinaryNumber Flag(int position) => new(Bits.UInt64.Flag(Value, position), Size);

    public override string ToString() => Bits.UInt64.ToString(Value).Replace(" ", "")[^Size..];

    public static BinaryNumber operator &(BinaryNumber l, BinaryNumber r) => new(l.Value & r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator |(BinaryNumber l, BinaryNumber r) => new(l.Value | r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator ^(BinaryNumber l, BinaryNumber r) => new(l.Value ^ r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator ~(BinaryNumber n) => new(~n.Value, n.Size);

    public static BinaryNumber Empty(int size) => new(default, size);

    public static BinaryNumber Parse(string str)
        => new(Bits.UInt64.Parse(str), str.Count(ch => ch == '0' || ch == '1'));

    public override bool Equals(object obj)=> obj is BinaryNumber other && Equals(other);
    public bool Equals(BinaryNumber other) => Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(BinaryNumber l, BinaryNumber r) => l.Equals(r);
    public static bool operator !=(BinaryNumber l, BinaryNumber r) => !(l == r);
}
public static class BinaryNumberExtensions
{
    public static BinaryNumber And(this IEnumerable<BinaryNumber> numbers)
    {
        BinaryNumber and = new(ulong.MaxValue, 0);
        foreach(var number in numbers)
        {
            and &= number;
        }
        return and;
    }
}
