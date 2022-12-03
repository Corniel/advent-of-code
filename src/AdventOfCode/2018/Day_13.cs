using SmartAss.Text;

namespace Advent_of_Code_2018;

public class Day_13
{
    [Example(answer: "7,3", 1)]
    [Puzzle(answer: "83,121")]
    public Point part_one(string input) => Simulate(input, stopOncrash: true);

    [Example(answer: "6,4", 2)]
    [Puzzle(answer: "102, 144")]
    public Point part_two(string input) => Simulate(input);

    private static Point Simulate(string input, bool stopOncrash = false)
    {
        var grid = input.CharPixels(ignoreSpace: false).Grid();
        var carts = grid.Positions
            .Select(p => new Cart(p, grid[p]))
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

                switch (grid[cart.Pos])
                {
                    case '+': cart.Intersect(); break;
                    case '/': cart.TurnTlBr(); break;
                    case '\\': cart.TurnTrBl(); break;
                }
            }
        }
        return carts.Single().Pos;
    }

    class Cart
    {
        public Cart(Point pos, char ch)
        {
            Pos = pos;
            Dir = ((CompassPoint)" ^>v<".IndexOf(ch)).ToVector();
        }

        public Point Pos { get; private set; }
        public Vector Dir { get; private set; }
        public int Intersections { get; private set; }
        public void Next() => Pos += Dir;
        public void Intersect() => Dir = (Intersections++.Mod(3)) switch
        {
            0 => Dir.TurnLeft(),  2 => Dir.TurnRight(),  _ => Dir
        };
        public void TurnTlBr() => Dir = TlBr[Dir];
        private static readonly Dictionary<Vector, Vector> TlBr = new()
        {
            [Vector.N] = Vector.E,
            [Vector.E] = Vector.N,
            [Vector.S] = Vector.W,
            [Vector.W] = Vector.S,
        };
        public void TurnTrBl() => Dir = TrBl[Dir];
        private static readonly Dictionary<Vector, Vector> TrBl = new()
        {
            [Vector.N] = Vector.W,
            [Vector.E] = Vector.S,
            [Vector.S] = Vector.E,
            [Vector.W] = Vector.N,
        };
    }
}
