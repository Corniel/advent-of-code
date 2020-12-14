using Advent_of_Code;
using SmartAss;
using SmartAss.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_14
    {
        [Example(answer: 165, @"
            mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            mem[8] = 11
            mem[7] = 101
            mem[8] = 0")]
        [Puzzle(answer: 12512013221615, year: 2020, day: 14)]
        public ulong part_one(string input)
        {
            var mask = string.Empty;
            var memory = new Dictionary<ulong, ulong>();

            foreach (var line in input.Lines())
            {
                if (line[0..2] == "ma") { mask = line[7..]; }
                else
                {
                    var inputs = Inputs.Parse(line);
                    var mask_bits = Bits.UInt64.Parse(mask, "1", "X0");
                    var val_bits = Bits.UInt64.Parse(mask, "X", "10");
                    memory[inputs.Address] = (inputs.Value & val_bits) | mask_bits;
                }
            }
            return memory.Values.Sum();
        }

        [Example(answer: 208, @"
            mask = 000000000000000000000000000000X1001X
            mem[42] = 100
            mask = 00000000000000000000000000000000X0XX
            mem[26] = 1")]
        [Puzzle(answer: 3905642473893, year: 2020, day: 14)]
        public ulong part_two(string input)
        {
            var mask = string.Empty;
            var memory = new Dictionary<ulong, ulong>();
            foreach (var line in input.Lines())
            {
                if (line[0..2] == "ma") { mask = line[7..]; }
                else
                {
                    var inputs = Inputs.Parse(line);

                    foreach (var address in Addresses(mask, inputs.Address))
                    {
                        memory[address] = inputs.Value;
                    }
                }
            }
            return memory.Values.Sum();
        }

        private static IEnumerable<ulong> Addresses(string mask, ulong value)
        {
            var value_bits = Bits.UInt64.Parse(mask, ones: "10", zeros: "X");
            var mask_bits = Bits.UInt64.Parse(mask, ones: "1", zeros: "0X");
            var address = (value & value_bits) | mask_bits;
            var indexes = mask.Select((ch, i) => new { ch, i = 35 - i }).Where(p => p.ch == 'X').Select(p => p.i).ToArray();
            var permutations = 1 << indexes.Length;

            return Enumerable.Range(0, permutations).Select(p => address | FloatingBits(p, indexes));
        }
        private static ulong FloatingBits(int permutation, int[] indexes)
        {
            ulong bits = 0;
            for (var pos = 0; pos < indexes.Length; pos++)
            {
                if (Bits.UInt64.HasFlag((ulong)permutation, pos))
                {
                    bits = Bits.UInt64.Flag(bits, indexes[pos]);
                }
            }
            return bits;
        }
        private record Inputs(ulong Address, ulong Value)
        { 
            public static Inputs Parse(string line)
            {
                var split = line.IndexOf(" = ") - 1;
                return new Inputs(line[4..split].UInt64(), line[(split + 4)..].UInt64());
            }
        }
    }
}