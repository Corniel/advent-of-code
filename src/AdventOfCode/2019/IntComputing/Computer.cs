using Advent_of_Code;
using SmartAss;
using SmartAss.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Int = System.Numerics.BigInteger;

namespace Advent_of_Code_2019.IntComputing
{
    [DebuggerDisplay("{Pointer}, Size = {Size}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class Computer : IEnumerable<Int>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<Int> memory = new();

        public Computer(IEnumerable<Int> numbers) => memory.AddRange(numbers);

        public static Computer Parse(string input) => new(input.BigInts());

        public int Size => memory.Count;

        public int Pointer { get; private set; }
        public int PointerOffset { get; private set; }
        public bool Finished { get; private set; }

        public Queue<Int> Inputs { get; private set;  } = new();

        public Results Run(RunArguments arguments = null)
        {
            arguments ??= RunArguments.Empty();
            Inputs.EnqueueRange(arguments.Inputs);
            var results = new Results();
            var running = true;

            while (running && !Finished)
            {
                var opcode = ReadOpcode();

                Log($"\r\n{Pointer - 1:0000}: {opcode} ");

                switch (opcode.Instruction)
                {
                    case 01: Add(opcode); break;
                    case 02: Multiply(opcode); break;
                    case 03: Input(opcode); break;
                    case 04: Output(opcode, results.Output); running = !arguments.HaltOnOutput; break;
                    case 05: JumpIf(true, opcode); break;
                    case 06: JumpIf(false, opcode); break;
                    case 07: LessThen(opcode); break;
                    case 08: Equals(opcode); break;
                    case 09: RelativeBase(opcode); break;
                    case 99: Finished = true; break;
                    default: throw UnknownInstruction.For(opcode.Instruction);
                }
                results.Answer = memory[0];
            }
            return results;
        }
        public Computer Copy() => new(memory)
        {
            Pointer = Pointer,
            PointerOffset = PointerOffset,
            Finished = Finished,
            Inputs = Inputs.Copy(),
        };

        public Computer Update(int postion, Int value)
        {
            Write(postion, value);
            return this;
        }

        private Opcode ReadOpcode() => new Opcode((int)Read());
        private void Add(Opcode opcode) => Execute(opcode, (p1, p2) => p1 + p2);
        private void Multiply(Opcode opcode) => Execute(opcode, (p1, p2) => p1 * p2);
        private void Input(Opcode opcode)
        {
            var p1 = (int)ReadImmediate(opcode.P1);
            var value = Inputs.Dequeue();
            Write(p1, value);
            Log($"({Inputs.Count})");
        }
        private void Output(Opcode opcode, ICollection<Int> output)
        {
            var p1 = Read(opcode.P1);
            output.Add(p1);
            Log($"=> {p1} ({output.Count})");
        }
        private void JumpIf(bool condition, Opcode opcode)
        {
            var p1 = Read(opcode.P1);
            var target = (int)Read(opcode.P2);
            var jump = (p1 != 0) == condition;
            Log(jump ? $"=> {target:0000}" : "false");
            if (jump)
            {
                if (target < 0) { throw new OutOfMemory(); }
                else { Pointer = target; }
            }
        }
        private void LessThen(Opcode opcode) => Execute(opcode, (p1, p2) => p1 < p2 ? 1 : 0);
        private void Equals(Opcode opcode) => Execute(opcode, (p1, p2) => p1 == p2 ? 1 : 0);
        private void RelativeBase(Opcode opcode)
        {
            var p1 = (int)Read(opcode.P1);
            PointerOffset += p1;
            Log($"+= {p1} => {PointerOffset}");
        }
        private void Execute(Opcode opcode, Func<Int, Int, Int> function)
        {
            var p1 = Read(opcode.P1);
            var p2 = Read(opcode.P2);
            var target = (int)ReadImmediate(opcode.P3);
            var value = function(p1, p2);
            Log($"{p1}, {p2} ");
            Write(target, value);
        }

        private Int ReadImmediate(Mode mode)
            => mode switch
            {
                Mode.Position => Read(),
                Mode.Relative => Read() + PointerOffset,
                _ => throw new InvalidOperationException(),
            };
        private Int Read(Mode mode)
            => mode switch
            {
                Mode.Position => Read((int)Read()),
                Mode.Relative => Read((int)Read() + PointerOffset),
                Mode.Immediate => Read(),
                _ => throw new InvalidOperationException(),
            };
        private Int Read() => Read(Pointer++);
        private Int Read(int position) => memory[InMemory(position)];
        private void Write(int position, Int value)
        {
            position = InMemory(position);
            Log($"=> {position:0000}: {value} ");
            memory[position] = value;
        }
        private int InMemory(int position)
        {
            if (position < 0) { throw new OutOfMemory(); }
            while (position >= Size)
            {
                memory.Add(default);
            }
            return position;
        }

        public IEnumerator<Int> GetEnumerator() => memory.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private static void Log(string str) => Do.Nothing(); // Console.Write(str)
    }
}
