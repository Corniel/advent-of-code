using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace SmartAss.Navigation;

public readonly struct Cursor : IEquatable<Cursor>
{
    public readonly Point Pos;
    public readonly Vector Dir;

    public Cursor(Point pos, CompassPoint dir) : this(pos, dir.ToVector()) { }

    public Cursor(Point pos, Vector dir)
    {
        Pos = pos;
        Dir = dir;
    }

    /// <summary>Moves the cursor.</summary>
    [Pure]
    public Cursor Move(int steps = 1) => new(Pos + (Dir * steps), Dir);

    [Pure]
    public Cursor Reverse(int steps = 1) => new(Pos - (Dir * steps), Dir);

    [Pure]
    public Cursor WithDir(Vector dir) => new(Pos, dir);

    [Pure]
    public Cursor TurnLeft() => new(Pos, Dir.TurnLeft());

    [Pure]
    public Cursor TurnRight() => new(Pos, Dir.TurnRight());

    [Pure]
    public Cursor UTurn() => new(Pos, Dir.UTurn());

    [Pure]
    public Cursor Rotate(char ch) => ch switch
    {
        'R' => TurnRight(),
        'L' => TurnLeft(),
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
    public override string ToString() => $"Pos = {Pos}, Dir = {Dir} ({Dir.CompassPoint()})";
}
