using Advent_of_Code;
using SmartAss.Topology;
using System;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_12
    {
        [Example(answer: 25, "F10;N3;F7;R90;F11")]
        [Puzzle(answer: 1631, year: 2020, day: 12)]
        public int part_one(string input)
        {
            var ship = Point.O;
            var orrientation = Action.E;

            foreach (var i in input.Lines().Select(Instruction.Parse))
            {
                switch (i.Action)
                {
                    case Action.F: ship += Direction(orrientation) * i.Distance; break;
                    case Action.L:
                    case Action.R: orrientation = (Action)((int)orrientation + i.Rotation).Mod(4); break;
                    case Action.N:
                    case Action.E:
                    case Action.S:
                    case Action.W: ship += Direction(i.Action) * i.Distance; break;
                }
            }
            return ship.ManhattanDistance(Point.O);
        }

        [Example(answer: 286, "F10;N3;F7;R90;F11")]
        [Puzzle(answer: 58606, year: 2020, day: 12)]
        public int part_two(string input)
        {
            var ship = Point.O;
            var waypoint = new Point(+10, -1);

            foreach (var i in input.Lines().Select(Instruction.Parse))
            {
                switch (i.Action)
                {
                    case Action.F: ship += (waypoint - Point.O) * i.Distance; break;
                    case Action.L:
                    case Action.R: waypoint = Rotate(waypoint, i.Rotation); break;
                    case Action.N:
                    case Action.E:
                    case Action.S:
                    case Action.W: waypoint += Direction(i.Action) * i.Distance; break;
                }
            }
            return ship.ManhattanDistance(Point.O);
        }

        private readonly struct Instruction
        {
            public Instruction(Action action, int value)
            {
                Action = action;
                Rotation = action == Action.L || action == Action.R ? value / (int)action : 0;
                Distance = Rotation == 0 ? value : 0;
            }

            public Action Action { get; }
            public int Distance { get; }
            public int Rotation { get; }
            public static Instruction Parse(string str)
                => new Instruction(Enum.Parse<Action>(str.Substring(0, 1)), str.Substring(1).Int32());
        }

        private enum Action
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3,
            L = -90,
            R = +90,
            F = int.MaxValue,
        }

        private static Vector Direction(Action compass)
            => compass switch
            {
                Action.E => Vector.E,
                Action.S => Vector.S,
                Action.W => Vector.W,
                Action.N => Vector.N,
                _ => throw new InvalidOperationException(),
            };

        private static Point Rotate(Point point, int rotation)
            => rotation.Mod(4) switch
            {
                1 => new Point(-point.Y, +point.X),
                2 => new Point(-point.X, -point.Y),
                3 => new Point(+point.Y, -point.X),
                _ => new Point(+point.X, +point.Y),
            };
    }
}