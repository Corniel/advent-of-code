namespace Advent_of_Code_2020;

[Category(Category.VectorAlgebra, Category.Simulation)]
public class Day_12
{
    [Example(answer: 25, "F10;N3;F7;R90;F11")]
    [Puzzle(answer: 1631, O.μs)]
    public int part_one(Inputs<Instr> input)
    {
        var ferry = Point.O;
        var orientation = Vector.E;

        foreach (var i in input)
        {
            if (i.Action == Action.F) { ferry += orientation * i.Distance; }
            else if (i.Rotation != 0) { orientation = orientation.Rotate(i.Rotation); }
            else { ferry += i.Direction * i.Distance; }
        }
        return ferry.ManhattanDistance(Point.O);
    }

    [Example(answer: 286, "F10;N3;F7;R90;F11")]
    [Puzzle(answer: 58606, O.μs)]
    public int part_two(Inputs<Instr> input)
    {
        Point ferry = (0, 0);
        Point waypoint = (+10, -1);

        foreach (var i in input)
        {
            if (i.Action == Action.F) { ferry += (waypoint - Point.O) * i.Distance; }
            else if (i.Rotation != default) { waypoint = waypoint.Rotate(Point.O, i.Rotation); }
            else { waypoint += i.Direction * i.Distance; }
        }
        return ferry.ManhattanDistance(Point.O);
    }

    public readonly struct Instr
    {
        public Instr(Action action, int value)
        {
            Action = action;
            Rotation = action == Action.L || action == Action.R ? (DiscreteRotation)(value / (int)action) : default;
            Distance = Rotation == 0 ? value : 0;
        }
        public Action Action { get; }
        public int Distance { get; }
        public DiscreteRotation Rotation { get; }
        public Vector Direction => Action switch
        {
            Action.E => Vector.E,
            Action.S => Vector.S,
            Action.W => Vector.W,
            Action.N => Vector.N,
            _ => Vector.O,
        };
        public static Instr Parse(string str) => new(Enum.Parse<Action>(str[0..1]), str[1..].Int32());
    }
    public enum Action { N, E, S, W, F, L = +90, R = -90 }
}
