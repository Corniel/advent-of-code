using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace SmartAss.Navigation;

public readonly struct Cursor : IEquatable<Cursor>
{
    public readonly Point Pos;
    public readonly Vector Dir;

    public Cursor(Point pos, Vector dir)
    {
        Pos = pos;
        Dir = dir;
    }

    /// <summary>Moves the cursor.</summary>
    [Pure]
    public Cursor Move(int steps = 1) => new(Pos + (Dir * steps), Dir);

    [Pure]
    public Cursor Rotate(char ch) => ch switch
    {
        'R' => new(Pos, Dir.TurnRight()),
        'L' => new(Pos, Dir.TurnLeft()),
        _ => throw new FormatException($"'{ch}' does not describe a valid rotation.")
    };

    [Pure]
    public Cursor Rotate(DiscreteRotation rotation) => new(Pos, Dir.Rotate(rotation));

    /// <inheritdoc />
    [Pure]
    public override bool Equals([NotNullWhen(true)] object obj) => obj is Cursor other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Cursor other) => Pos == other.Pos && Dir == other.Dir;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => HashCode.Combine(Pos, Dir);

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"Pos = {Pos}, Dir = {Dir}";
}
