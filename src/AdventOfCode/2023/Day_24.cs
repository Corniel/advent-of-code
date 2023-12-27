using Microsoft.Z3;

namespace Advent_of_Code_2023;

[Category(Category.VectorAlgebra)]
public class Day_24
{
    [Example(answer: 2, "19, 13, 30 @ -2,  1, -2;18, 19, 22 @ -1, -1, -2;20, 25, 34 @ -2, -2, -4;12, 31, 28 @ -1, -2, -1;20, 19, 15 @  1, -5, -3", 7, 27)]
    [Puzzle(answer: 16589, null, 200_000_000_000_000, 400_000_000_000_000, O.ms)]
    public int part_one(Lines lines, long min, long max)
        => lines.As(Fx.Parse).ToArray().RoundRobin().Count(p => Cross(p, min, max));

    [Puzzle(answer: 781390555762385, O.s)]
    public long part_two(Lines lines)
    {
        var c = new Context();
        var s = c.MkSolver();

        var x  = Const("x");  var y  = Const("y");  var z  = Const("z");
        var Δx = Const("Δx"); var Δy = Const("Δy"); var Δz = Const("Δz");

        foreach (var fx in lines.As(Fx.Parse))
        {
            var t = c.MkIntConst(fx.ToString());
            s.Add(t >= 0);
            s.Add(c.MkEq(x + (t * Δx), Num(fx.Pos.X) + t * Num(fx.Dir.X)));
            s.Add(c.MkEq(y + (t * Δy), Num(fx.Pos.Y) + t * Num(fx.Dir.Y)));
            s.Add(c.MkEq(z + (t * Δz), Num(fx.Pos.Z) + t * Num(fx.Dir.Z)));
        }
        s.Check();
        return Val(x) + Val(y) + Val(z);

        long Val(IntExpr e) => s.Model.Eval(e).ToString().Int64();
        IntExpr Const(string n) => c.MkIntConst(n);
        IntNum Num(long n) => c.MkInt(n);
    }

    bool Cross(Pair<Fx> p, long min, long max)
    {
        var cross = p.First.Crosses(p.Second);
        return cross.X >= min && cross.X <= max
            && cross.Y >= min && cross.Y <= max
            && p.First.At(cross) >= 0 && p.Second.At(cross) >= 0;
    }

    [DebuggerDisplay("f(x) = {A}x + {B}, ({Pos.X}, {Pos.Y}, {Pos.Z}), ({Dir.X}, {Dir.Y}, {Dir.Z})")]
    record Fx(double A, double B, Vec Pos, Vec Dir)
    {
        public Vec Crosses(Fx other)
        {
            var x = (other.B - B) / (A - other.A);
            return new(x.Long(), (A * x + B).Long(), 0);
        }

        public double At(Vec pos) => 1d * (pos.X - Pos.X) / Dir.X;

        public static Fx Parse(string line)
        {
            var ns = line.Int64s().ToArray();
            var pos = new Vec(ns[0], ns[1], ns[2]);
            var dir = new Vec(ns[3], ns[4], ns[5]);
            var a = 1d * dir.Y / dir.X;
            var b = pos.Y - a * pos.X;
            return new(a, b, pos, dir);
        }
    }

    record struct Vec(long X, long Y, long Z);
}
