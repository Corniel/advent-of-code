﻿namespace Advent_of_Code_2022;

[Category(Category.μs, Category.Cryptography)]
public class Day_06
{
    [Example(answer: 5, "bvwbjplbgvbhsrlpgdmjqwftvncz")]
    [Example(answer: 6, "nppdvjthqldpwncqszvftbrmjlhg")]
    [Example(answer: 10, "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg")]
    [Example(answer: 11, "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw")]
    [Puzzle(answer: 1034)]
    public int part_one(string input) =>  Read(input, 4);

    [Puzzle(answer: 2472)]
    public int part_two(string input) => Read(input, 14);

    static int Read(string input, int length) => Enumerable.Range(0, input.Length).First(i => input.Substring(i, length).AllDistinct()) + length;
}
