using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Intcode
    {
        private readonly Opcode[] memory;
        
        public Intcode(params int[] instructions)
        {
            memory = instructions.Select(i => i.Opcode()).ToArray();
        }

        public int Size => memory.Length;

        public int Position { get; internal set; }

        public Opcode this[int position]
        {
            get => memory[position];
            internal set => memory[position] = value;
        }

        public int Value(int position) => this[position].Value;

        public int Run()
        {
            while(Position < Size)
            {
                var opcode = this[Position];
                opcode.Execute(this);
                Position += 4;
            }
            return this[0].Value;
        }

        public Intcode Update(int position, int value)
        {
            memory[position] = value.Opcode();
            return this;
        }

        public override string ToString()
            => string.Join(',', memory.Select(c => c.Value));

        public static Intcode Parse(string str)
            => new Intcode(str
                .CommaSeperated()
                .SelectMany(s => s.Ints())
                .ToArray());
    }
}