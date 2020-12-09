using System;

namespace Advent_of_Code.Maths
{
    public readonly struct Vector3d : IEquatable<Vector3d>
    {
        public static readonly Vector3d O;

        public Vector3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Vector3d Adjust(int x = 0, int y = 0, int z = 0) => new Vector3d(X + x, Y + y, Z + z);

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object obj) => obj is Vector3d other && Equals(other);
        
        public bool Equals(Vector3d other)
            => X == other.X 
            && Y == other.Y
            && Z == other.Z;

        public override int GetHashCode() => X ^ (Y << 11) ^(Z << 22);
    }
}
