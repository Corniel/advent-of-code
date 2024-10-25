using System.Diagnostics.Contracts;

namespace SmartAss.Numerics;

public static class VectorExtensions
{
    [Pure]
    public static Vector Vector(this char ch)
        => TryVector(ch) is { } v
        ? v
        : throw new FormatException($"'{ch}' is not a valid vector");

    [Pure]
    public static Vector? TryVector(this char ch) => ch switch
    {
        '^' or 'U' => Numerics.Vector.N,
        '>' or 'R' => Numerics.Vector.E,
        'v' or 'D' => Numerics.Vector.S,
        '<' or 'L' => Numerics.Vector.W,
        _ => null
    };
}
