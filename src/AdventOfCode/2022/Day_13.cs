﻿namespace Advent_of_Code_2022;

[Category(Category.μs, Category.ExpressionParsing)]
public class Day_13
{
    [Example(answer: 13, 1)]
    [Puzzle(answer: 6101)]
    public int part_one(string input) => input.GroupedLines().Select(Ordered).Sum();

    static int Ordered(string[] lines, int index)
        => Package.Parse(lines[0]).CompareTo(Package.Parse(lines[1])) == +1 ? 0 : index + 1;

    [Example(answer: 140, 1)]
    [Puzzle(answer: 21909)]
    public int part_two(string input)
    {
        var two = Package.Parse("[[2]]");
        var six = Package.Parse("[[6]]");
        var packages = (input).Lines(Package.Parse).Concat(new[] { two, six }).Order().ToList();
        return (packages.IndexOf(two) + 1) * (packages.IndexOf(six) + 1);
    }

    record Package(IReadOnlyList<Package> Children) : IComparable<Package>
    {
        public virtual int CompareTo(Package other)
        {
            if (other is Number) return CompareTo(new Package(new[] { other }));
            return Range(0, Math.Min(Children.Count, other.Children.Count))
                .Select(i => Children[i].ComparesTo(other.Children[i]))
                .FirstOrDefault(c => c is { })
                ?? Children.Count.CompareTo(other.Children.Count);
        }

        public override string ToString() => $"[{string.Join(',', Children)}]";

        public static Package Parse(string line) => new Parser(line).Read();
    }
    record Number(int Value) : Package(Array.Empty<Package>())
    {
        public override int CompareTo(Package other) => other is Number number
            ? Value.CompareTo(number.Value)
            : new Package(new[] { this }).CompareTo(other);

        public override string ToString()=> Value.ToString();
    }

    class Parser : SyntaxParser
    {
        public Parser(string input) : base(input) { }

        public Package Read()
        {
            if (ReadAhead() == '[')
            {
                ReadChar();

                if (ReadAhead() == ']') return new Package(Array.Empty<Package>());

                var childeren = new List<Package> { Read() };

                while (ReadChar() == ',') childeren.Add(Read());

                return new Package(childeren);
            }
            else return new Number(ReadInt32());
        }
    }
}
