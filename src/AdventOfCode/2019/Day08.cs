﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2019
{
    public class Day08
    {
        public static int One(string input)
            => Layer.Parse(25, 6, input)
            .OrderBy(layer => layer.Zeros)
            .Select(layer => layer.Ones * layer.Twos)
            .FirstOrDefault();

        public static string Two(string input)
        {
            var layers = Layer.Parse(25, 6, input).ToArray();
            var merged = layers.Last();
            foreach(var layer in layers.Reverse().Skip(1))
            {
                merged = layer.Merge(merged);
            }
            return merged.ToString(25);
        }

        public readonly struct Layer
        {
            private const char Transprant = '2';

            private readonly string pixels;
            public Layer(string pixels) => this.pixels = pixels;

            public int Size => pixels.Length;
            public int Zeros => pixels.Count(ch => ch == '0');
            public int Ones => pixels.Count(ch => ch == '1');
            public int Twos => pixels.Count(ch => ch == Transprant);

            public string ToString(int width)
            {
                var sb = new StringBuilder(Size).AppendLine();
                var pos = 0;
                while (pos < Size)
                {
                    sb.AppendLine(pixels.Substring(pos, width));
                    pos += width;
                }
                return sb.ToString()
                    .Replace("1", "█")
                    .Replace("0", "░");
            }

            public Layer Merge(Layer lower)
            {
                var merged = pixels.ToCharArray();
                for(var i = 0; i < Size; i++)
                {
                    if(pixels[i] == Transprant)
                    {
                        merged[i] = lower.pixels[i];
                    }
                }
                return new Layer(new string(merged));
            }

            public static IEnumerable<Layer> Parse(int width, int height, string str)
            {
                var pos = 0;
                var size = width * height;
                while (pos < str.Length - size)
                {
                    yield return new Layer(str.Substring(pos, size));
                    pos += size;
                }
            }
        }

    }
}