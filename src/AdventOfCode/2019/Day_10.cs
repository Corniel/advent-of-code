namespace Advent_of_Code_2019;

[Category(Category.VectorAlgebra)]
public class Day_10
{
    [Example(answer: 8, @"
            .#..#
            .....
            #####
            ....#
            ...###")]
    [Example(answer: 33, @"
            ......#.#.
            #..#.#....
            ..#######.
            .#.#.###..
            .#..#.....
            ..#....#.#
            #..#....#.
            .##.#..###
            ##...#..#.
            .#....####")]
    [Example(answer: 35, @"
            #.#...#.#.
            .###....#.
            .#....#...
            ##.#.#.#.#
            ....#.#.#.
            .##..###.#
            ..#...##..
            ..##....##
            ......#...
            .####.###.")]
    [Example(answer: 41, @"
            .#..#..###
            ####.###.#
            ....###.#.
            ..###.##.#
            ##.##.#.#.
            ....###..#
            ..#.#..#.#
            #..#.#.###
            .##...##.#
            .....#.#..")]
    [Example(answer: 210, @"
            .#..##.###...#######
            ##.############..##.
            .#.######.########.#
            .###.#######.####.#.
            #####.##.#.##.###.##
            ..#####..#.#########
            ####################
            #.####....###.#.#.##
            ##.#################
            #####.##.###..####..
            ..######..##.#######
            ####.##.####...##..#
            .#####..#.######.###
            ##...#.##########...
            #.##########.#######
            .####.#.###.###.#.##
            ....##.##.###..#####
            .#.#.###########.###
            #.#.#.#####.####.###
            ###.##.####.##.#..##")]
    [Puzzle(answer: 347, O.ms10)]
    public int part_one(string str)
    {
        var astroids = Astroids.Parse(str);
        return astroids
            .Max(station => Astroids.Relations(station, astroids)
            .Select(r => r.Angle)
            .Distinct()
            .Count());
    }

    [Example(answer: 802, @"
            .#..##.###...#######
            ##.############..##.
            .#.######.########.#
            .###.#######.####.#.
            #####.##.#.##.###.##
            ..#####..#.#########
            ####################
            #.####....###.#.#.##
            ##.#################
            #####.##.###..####..
            ..######..##.#######
            ####.##.####...##..#
            .#####..#.######.###
            ##...#.##########...
            #.##########.#######
            .####.#.###.###.#.##
            ....##.##.###..#####
            .#.#.###########.###
            #.#.#.#####.####.###
            ###.##.####.##.#..##")]
    [Puzzle(answer: 829, O.ms10)]
    public int part_two(string str)
    {
        var astroids = Astroids.Parse(str);
        var station = astroids
            .OrderByDescending(s =>
                Astroids.Relations(s, astroids)
                    .Select(r => r.Angle)
                    .Distinct()
                    .Count())
            .FirstOrDefault();

        var relations = Astroids.Relations(station, astroids)
            .OrderBy(r => r.Angle)
            .ThenBy(r => r.Distance)
            .ToArray();

        var vaporized = new HashSet<Point>();
        var started = false;
        var postion = 0;
        Relation last = default;

        while (vaporized.Count < 200)
        {
            var relation = relations[postion++];
            started |= relation.Angle >= Math.PI / 2;

            if (started && last.Angle != relation.Angle && vaporized.Add(relation.Astroid))
            {
                last = relation;
            }
            if (postion >= relations.Length) { postion = 0; }
        }
        return last.Astroid.X * 100 + last.Astroid.Y;
    }

    readonly struct Relation
    {
        Relation(Point astroid, double angle, double distance)
        {
            Astroid = astroid;
            Angle = angle;
            Distance = distance;
        }

        public Point Astroid { get; }
        public double Angle { get; }
        public double Distance { get; }

        public override string ToString() => $"{Astroid} {Angle / Math.PI:0.####}π, distance: {Distance:0.0#}";

        public static Relation Create(Point station, Point astroid)
        {
            var v = station - astroid;
            return new Relation(astroid, v.Angle, (v.X.Sqr() + v.Y.Sqr()).Sqrt());
        }
    }

    static class Astroids
    {
        public static IEnumerable<Relation> Relations(Point station, IEnumerable<Point> androids)
            => androids
            .Where(a => a != station)
            .Select(other => Relation.Create(station, other));

        public static List<Point> Parse(string str)
        {
            var astroids = new List<Point>();

            var x = 0;
            var y = 0;

            foreach (var ch in str)
            {
                switch (ch)
                {
                    case '#':
                        astroids.Add(new Point(x++, y));
                        break;
                    case '.':
                        x++;
                        break;
                    case '\n':
                        if (x > 0) { y++; }
                        x = 0;
                        break;
                }
            }
            return astroids;
        }
    }
}
