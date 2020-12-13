using Advent_of_Code;
using SmartAss.Parsing;
using SmartAss.Topology;
using System;

namespace Advent_of_Code_2020
{
    public class Day_12
    {
        [Example(answer: 25, "F10;N3;F7;R90;F11")]
        [Puzzle(answer: 1631, year: 2020, day: 12)]
        public int part_one(string input)
        {
            var ferry = Point.O;
            var orientation = Vector.E;

            foreach (var i in input.Lines(Instruction.Parse))
            {
                if (i.Action == Action.F) { ferry += orientation * i.Distance; }
                else if (i.Rotation != 0) { orientation = orientation.Rotate(i.Rotation); }
                else { ferry += i.Direction * i.Distance; }
            }
            return ferry.ManhattanDistance(Point.O);
        }

        [Example(answer: 286, "F10;N3;F7;R90;F11")]
        [Puzzle(answer: 58606, year: 2020, day: 12)]
        public int part_two(string input)
        {
            var ferry = Point.O;
            var waypoint = new Point(+10, -1);

            foreach (var i in input.Lines(Instruction.Parse))
            {
                if (i.Action == Action.F) { ferry += (waypoint - Point.O) * i.Distance; }
                else if (i.Rotation != 0) { waypoint = waypoint.Rotate(Point.O, i.Rotation); }
                else { waypoint += i.Direction * i.Distance; }
            }
            return ferry.ManhattanDistance(Point.O);
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
            public Vector Direction => Action switch
            {
                Action.E => Vector.E,
                Action.S => Vector.S,
                Action.W => Vector.W,
                Action.N => Vector.N,
                _ => Vector.O,
            };
            public static Instruction Parse(string str) => new Instruction(Enum.Parse<Action>(str[0..1]), str[1..].Int32());
        }
        private enum Action
        {
            N, E, S, W, 
            F,
            L = +90,
            R = -90,
        }
    }
}