﻿using AdventOfCode.Maths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Day10
    {
        public static int One(string input)
        {
            var astroids = Astroids.Parse(input);
            return astroids
                .Max(station => Astroids.Relations(station, astroids)
                    .Select(r => r.Angle)
                    .Distinct()
                    .Count());
        }

        public static int Two(string input)
        {
            var astroids = Astroids.Parse(input);
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

            while(vaporized.Count < 200)
            {
                var relation = relations[postion++];
                started |= relation.Angle >= Math.PI / 2;

                if(started && last.Angle != relation.Angle && vaporized.Add(relation.Astroid))
                {
                    last = relation;
                }
                if (postion >= relations.Length){ postion = 0; }
            }
            return last.Astroid.X * 100 + last.Astroid.Y;
        }

        public readonly struct Relation
        {
            private Relation(Point astroid, double angle, double distance)
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
                return new Relation(astroid, v.Angle, Math.Sqrt(v.X * v.X + v.Y * v.Y));
            }
        }

        public static class Astroids
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

                foreach(var ch in str)
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
}