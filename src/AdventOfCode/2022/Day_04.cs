﻿namespace Advent_of_Code_2022;

[Category(Category.μs, Category.Computation)]
public class Day_04
{
    [Example(answer: 2, "2-4,6-8;2-3,4-5;5-7,7-9;2-8,3-7;6-6,4-6;2-6,4-8")]
    [Puzzle(answer: 433)]
    public int part_one(string input) => input.Lines(Parse).Count(ps => ps[0].FullyContains(ps[1]) || ps[1].FullyContains(ps[0]));

    [Example(answer: 4, "2-4,6-8;2-3,4-5;5-7,7-9;2-8,3-7;6-6,4-6;2-6,4-8")]
    [Puzzle(answer: 852)]
    public long part_two(string input) => input.Lines(Parse).Count(ps => ps[0].Overlaps(ps[1]));

    static Int32Range[] Parse(string line) => line.CommaSeparated().Select(Int32Range.Parse).ToArray();
}