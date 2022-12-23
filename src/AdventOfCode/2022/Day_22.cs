namespace Advent_of_Code_2022;

[Category(Category.Grid)]
public class Day_22
{
    [Example(answer: 6032, Example._1)]
    [Puzzle(answer: 1428, O.ms)]
    public int part_one(string input) => Navigate(input, Donut);

    [Example(answer: 162155, Example._2)]
    [Puzzle(answer: 142380, O.ms)]
    public int part_two(string input) => Navigate(input, Cube);

    private static int Navigate(string input, Func<Grid<char>, Cursor, Cursor> offGrid)
    {
        var group = input.GroupedLines(StringSplitOptions.None).ToArray();
        var map = group[0].CharPixels(ignoreSpace: false).Grid();
        var cursor = new Cursor(map.First(t => t.Value == '.').Key, Vector.E);

        foreach (var instr in Instructions(group[1][0]))
        {
            if (instr is char rotate) cursor = cursor.Rotate(rotate);
            else for (var step = 0; step < (int)instr; step++)
            {
                var next = cursor.Move();
                next = next.OnMap(map) ? next : offGrid(map, cursor);
                if (map[next.Pos] != '#') cursor = next;
                else break;
            }
        }
        return cursor.Score();
    }

    static Cursor Donut(Grid<char> map, Cursor cursor)
    {
        cursor = cursor.Move();
        do cursor = map.OnGrid(cursor.Pos) ? cursor.Move() : cursor.Modulo(map);
        while (!cursor.OnMap(map));
        return cursor;
    }
    
    static Cursor Cube(Grid<char> map, Cursor cursor)
    {
        var face = new Point(cursor.Pos.X / RibbonSize, cursor.Pos.Y / RibbonSize);
        var ribbon = new Ribbon(face, cursor.Dir.CompassPoint());

        var pos = ribbon.Direction == CompassPoint.N || ribbon.Direction == CompassPoint.S
               ? cursor.Pos.X % RibbonSize
               : cursor.Pos.Y % RibbonSize;

        return Flows[ribbon].Step(pos);
    }

    /// <summary>Describes the flows between different ribbons.</summary>
    /// <remarks>
    ///   1155
    ///   1155
    ///   44
    ///   44
    /// 2266
    /// 2266
    /// 33
    /// 33
    /// </remarks>
    static readonly Dictionary<Ribbon, Flow> Flows = new()
    {
        [new Ribbon(new(0, 2), CompassPoint.W)] = new Flow(new(1, 0), CompassPoint.W, true),
        [new Ribbon(new(1, 0), CompassPoint.W)] = new Flow(new(0, 2), CompassPoint.W, true),

        [new Ribbon(new(0, 2), CompassPoint.N)] = new Flow(new(1, 1), CompassPoint.W, false),
        [new Ribbon(new(1, 1), CompassPoint.W)] = new Flow(new(0, 2), CompassPoint.N, false),

        [new Ribbon(new(0, 3), CompassPoint.W)] = new Flow(new(1, 0), CompassPoint.N, false),
        [new Ribbon(new(1, 0), CompassPoint.N)] = new Flow(new(0, 3), CompassPoint.W, false),

        [new Ribbon(new(0, 3), CompassPoint.S)] = new Flow(new(2, 0), CompassPoint.N, false),
        [new Ribbon(new(2, 0), CompassPoint.N)] = new Flow(new(0, 3), CompassPoint.S, false),

        [new Ribbon(new(0, 3), CompassPoint.E)] = new Flow(new(1, 2), CompassPoint.S, false),
        [new Ribbon(new(1, 2), CompassPoint.S)] = new Flow(new(0, 3), CompassPoint.E, false),

        [new Ribbon(new(2, 0), CompassPoint.E)] = new Flow(new(1, 2), CompassPoint.E, true),
        [new Ribbon(new(1, 2), CompassPoint.E)] = new Flow(new(2, 0), CompassPoint.E, true),

        [new Ribbon(new(2, 0), CompassPoint.S)] = new Flow(new(1, 1), CompassPoint.E, false),
        [new Ribbon(new(1, 1), CompassPoint.E)] = new Flow(new(2, 0), CompassPoint.S, false),
    };

    record struct Ribbon(Point Face, CompassPoint Direction);

    record Flow(Point Face, CompassPoint Ribbon, bool Mirror)
    {
        public Cursor Step(int pos)
        {
            pos = Mirror ? Ribbon_min1 - pos : pos;
            var loc = new Point(Face.X * RibbonSize, Face.Y * RibbonSize) + Ribbon switch
            {
                CompassPoint.N => Vector.E * pos,
                CompassPoint.E => Vector.S * pos + Vector.E * Ribbon_min1,
                CompassPoint.S => Vector.E * pos + Vector.S * Ribbon_min1,
                CompassPoint.W or _ => Vector.S * pos,
            };
            return new(loc, Ribbon.ToVector().UTurn());
        }
    }
    const int RibbonSize = 50;
    const int Ribbon_min1 = RibbonSize - 1;

    static IEnumerable<object> Instructions(string line)
    {
        var factor = 0;

        foreach (var ch in line)
        {
            if (ch == 'L' || ch == 'R')
            {
                yield return factor;
                yield return ch;
                factor = 0;
            }
            else factor = factor * 10 + ch - '0';
        }
        yield return factor;
    }

    record struct Cursor(Point Pos, Vector Dir)
    {
        public Cursor Move() => new(Pos + Dir, Dir);
        public Cursor Rotate(char dir) => new(Pos, dir == 'R' ? Dir.TurnRight() : Dir.TurnLeft());
        public bool OnMap(Grid<char> map) => map.OnGrid(Pos) && map[Pos] != ' ';
        public Cursor Modulo(Grid<char> map) => new(new(Pos.X.Mod(map.Cols), Pos.Y.Mod(map.Rows)), Dir);
        public int Score() => (Pos.Y + 1) * 1000 + (Pos.X + 1) * 4 + Facing[Dir];
    }

    static readonly Dictionary<Vector, int> Facing = new() { [Vector.E] = 0, [Vector.S] = 1, [Vector.W] = 2, [Vector.N] = 3 };
}
