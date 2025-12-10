namespace SmartAss;

[DebuggerDisplay("{ToString()} ({Size}: {Value})")]
public readonly struct BinaryNumber(ulong value, int size) : IEquatable<BinaryNumber>, IComparable<BinaryNumber>
{
    public readonly int Size = size;
    public readonly ulong Value = value;

    public int Count => Bits.UInt64.Count(Value);

    public bool IsEmpty() => Value == 0;

    public bool HasFlag(int position) => Bits.UInt64.HasFlag(Value, position);

    public BinaryNumber Flag(int position) => new(Bits.UInt64.Flag(Value, position), Size);

    public BinaryNumber Unflag(int position) => new(Bits.UInt64.Unflag(Value, position), Size);

    public override string ToString() => Bits.UInt64.ToString(Value).Replace(" ", "")[^Size..];

    public static BinaryNumber operator ++(BinaryNumber n) => new(n.Value + 1, n.Size);
    public static BinaryNumber operator &(BinaryNumber l, BinaryNumber r) => new(l.Value & r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator |(BinaryNumber l, BinaryNumber r) => new(l.Value | r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator ^(BinaryNumber l, BinaryNumber r) => new(l.Value ^ r.Value, Math.Max(l.Size, r.Size));
    public static BinaryNumber operator ~(BinaryNumber n) => new(~n.Value, n.Size);

    public static bool operator <(BinaryNumber l, BinaryNumber r) => l.Value < r.Value;
    public static bool operator >(BinaryNumber l, BinaryNumber r) => l.Value > r.Value;
    public static bool operator <=(BinaryNumber l, BinaryNumber r) => l.Value <= r.Value;
    public static bool operator >=(BinaryNumber l, BinaryNumber r) => l.Value >= r.Value;

    public int CompareTo(BinaryNumber other) => Value.CompareTo(other.Value);
    public override bool Equals(object obj) => obj is BinaryNumber other && Equals(other);
    public bool Equals(BinaryNumber other) => Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(BinaryNumber l, BinaryNumber r) => l.Equals(r);
    public static bool operator !=(BinaryNumber l, BinaryNumber r) => !(l == r);

    public static BinaryNumber Empty(int size) => new(default, size);

    public static BinaryNumber All(int size) => new((1UL << (size + 1)) - 1, size);

    public static BinaryNumber Parse(string str, string ones, string zeros)
        => new(Bits.UInt64.Parse(str, ones, zeros), str.Count(ch => (ones + zeros).Contains(ch)));

    public static BinaryNumber Parse(string str) => Parse(str, "1", "0");
}
  
public static class BinaryNumberExtensions
{
    public static BinaryNumber And(this IEnumerable<BinaryNumber> numbers)
    {
        BinaryNumber and = new(ulong.MaxValue, 0);
        foreach (var number in numbers)
        {
            and &= number;
        }
        return and;
    }
}
