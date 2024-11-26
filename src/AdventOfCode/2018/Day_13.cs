namespace Advent_of_Code_2018;

[Category(Category.Simulation)]
public class Day_13
{
    [Example(answer: "7,3", Example._1)]
    [Puzzle(answer: "83,121", O.ms)]
    public Point part_one(CharGrid map) => Simulate(map, stopOncrash: true);

    [Example(answer: "6,4", Example._2)]
    [Puzzle(answer: "102, 144", O.ms)]
    public Point part_two(CharGrid map) => Simulate(map);

    static Point Simulate(CharGrid map, bool stopOncrash = false)
    {
        var carts = map.Positions()
            .Select(p => new Cart(p, map[p]))
            .Where(c => c.Dir != default)
            .ToList();

        while (carts.Count > 1)
        {
            foreach (var cart in carts.OrderBy(c => c.Pos.Y).ThenBy(c => c.Pos.X).ToArray())
            {
                cart.Next();

                if (carts.Except(cart).FirstOrDefault(other => other.Pos == cart.Pos) is { } other)
                {
                    carts.Remove(cart);
                    carts.Remove(other);
                    if (stopOncrash) return cart.Pos;
                }

                switch (map[cart.Pos])
                {
                    case '+': cart.Intersect(); break;
                    case '/': cart.TurnTlBr(); break;
                    case '\\': cart.TurnTrBl(); break;
                }
            }
        }
        return carts.Single().Pos;
    }

    class Cart(Point pos, char ch)
    {
        public Point Pos { get; private set; } = pos;
        public Vector Dir { get; private set; } = ((CompassPoint)" ^>v<".IndexOf(ch)).ToVector();
        public int Intersections { get; private set; }
        public void Next() => Pos += Dir;
        public void Intersect() => Dir = (Intersections++.Mod(3)) switch
        {
            0 => Dir.TurnLeft(), 2 => Dir.TurnRight(), _ => Dir,
        };
        public void TurnTlBr() => Dir = TlBr[Dir];
        static readonly Dictionary<Vector, Vector> TlBr = new()
        {
            [Vector.N] = Vector.E,
            [Vector.E] = Vector.N,
            [Vector.S] = Vector.W,
            [Vector.W] = Vector.S,
        };
        public void TurnTrBl() => Dir = TrBl[Dir];
        static readonly Dictionary<Vector, Vector> TrBl = new()
        {
            [Vector.N] = Vector.W,
            [Vector.E] = Vector.S,
            [Vector.S] = Vector.E,
            [Vector.W] = Vector.N,
        };
    }
}
