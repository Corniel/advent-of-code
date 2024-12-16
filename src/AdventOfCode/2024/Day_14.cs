namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public partial class Day_14
{
    [Example(answer: 12, null, 11, 7, Example._1)]
    [Puzzle(answer: 231221760, null, 101, 103, O.μs)]
    public int part_one(Ints numbers, int wide, int tall) => Quadrants(Points(Bots(numbers), 100, wide, tall), wide, tall)[..4].Product();

    /// <remarks>
    /// Searching for this sub image 31 x 33.
    /// 
    /// ###############################
    /// #.............................#
    /// #.............................#
    /// #.............................#
    /// #.............................#
    /// #..............#..............#
    /// #.............###.............#
    /// #............#####............#
    /// #...........#######...........#
    /// #..........#########..........#
    /// #............#####............#
    /// #...........#######...........#
    /// #..........#########..........#
    /// #.........###########.........#
    /// #........#############........#
    /// #..........#########..........#
    /// #.........###########.........#
    /// #........#############........#
    /// #.......###############.......#
    /// #......#################......#
    /// #........#############........#
    /// #.......###############.......#
    /// #......#################......#
    /// #.....###################.....#
    /// #....#####################....#
    /// #.............###.............#
    /// #.............###.............#
    /// #.............###.............#
    /// #.............................#
    /// #.............................#
    /// #.............................#
    /// #.............................#
    /// ###############################
    /// </remarks>
    [Puzzle(answer: 6771, null, 101, 103, O.ms100)]
    public int part_two(Ints numbers, int wide, int tall)
    {
        var map = new Grid<bool>(wide, tall);
        var bots = Bots(numbers).ToArray();
        
        for (var step = 0; step < 10000; step++)
        {
            map.Clear();

            foreach (var p in Points(bots, step, wide, tall)) map[p] = true;

            for (var r = 0; r < tall - 33; r++)
            {
                var length = 0; var c = -1;
                while (++c + length < wide)
                {
                    var on = map[c, r];
                    if (on && ++length > 20) return step;
                    else if (!on) length = 0;
                }
            }
        }
        throw new NoAnswer();
    }

    private static IEnumerable<Point> Points(IEnumerable<Cursor> bots, int step, int wide, int tall)
        => bots.Select(b => b.Move(step).Pos).Select(b => new Point(b.X.Mod(wide), b.Y.Mod(tall)));

    static int[] Quadrants(IEnumerable<Point> points, int wide, int tall)
    {
        var w = wide / 2; var t = tall / 2;
        var qs = new int[5];

        foreach (var p in points)
        {
            qs[p switch 
            { 
                _ when p.X < w && p.Y < t => 0,
                _ when p.X < w && p.Y > t => 1,
                _ when p.X > w && p.Y < t => 2,
                _ when p.X > w && p.Y > t => 3,
                _ => 4,
            }]++;
        }
        return qs;
    }

    static IEnumerable<Cursor> Bots(Ints ns) => ns.ChunkBy(4).Select(n => new Cursor(new(n[0], n[1]), new Vector(n[2], n[3])));
}
