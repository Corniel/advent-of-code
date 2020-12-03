using AdventOfCode.Maths;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Day03
    {
        [Puzzle(2020, 03, Part.one)]
        public static object One(string input)
            => CountTrees(Row.Parse(input).ToArray(), new Vector(3, 1));

        [Puzzle(2020, 03, Part.two)]
        public static object Two(string input)
        {
            var rows = Row.Parse(input).ToArray();
            var slopes = new[]
            {
                new Vector(1, 1),
                new Vector(3, 1),
                new Vector(5, 1),
                new Vector(7, 1),
                new Vector(1, 2),
            };

            var trees = 1;
            foreach(var slope in slopes)
            {
                trees *= CountTrees(rows, slope);
            }
            return trees;
        }

        private static int CountTrees(Row[] rows, Vector slope)
        {
            var position = Point.O + slope;
            var trees = 0;
            while(position.Y < rows.Length)
            {
                var row = rows[position.Y];
                trees += row.IsTree(position) ? 1 : 0;
                position += slope;
            }
            return trees;
        }


        internal class Row
        {
            private const byte Tree = 17;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly byte[] squares;

            private Row(byte[] sqs) => squares = sqs;

            public int Size => squares.Length;

            public override string ToString() => string.Concat(squares.Select(tree => tree == Tree ? '#'  : '.'));

            public bool IsTree(Point point) => squares[point.X.Mod(Size)] == Tree;

            public static IEnumerable<Row> Parse(string str)
                => str.Lines().Select(AsRow);

            private static Row AsRow(string line)
            {
                var sqs = new byte[line.Length];
                for(var p = 0; p< sqs.Length; p++)
                {
                    if (line[p] == '#') sqs[p] = Tree;
                }
                return new Row(sqs);
            }
        }
    }
}