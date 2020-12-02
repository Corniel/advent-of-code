using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public interface Opcode
    {
        int Value { get; }

        void Execute(Intcode program);
    }
    public readonly struct Add : Opcode
    {
        public int Value => 1;

        public void Execute(Intcode program)
        {
            var l = program.Value(program.Position + 1);
            var r = program.Value(program.Position + 2);
            var target = program.Value(program.Position + 3);
            program.Update(target, program.Value(l) + program.Value(r));
        }

        public override string ToString() => nameof(Add);
    }
    public readonly struct Multiply : Opcode
    {
        public int Value => 2;

        public void Execute(Intcode program)
        {
            var l = program.Value(program.Position + 1);
            var r = program.Value(program.Position + 2);
            var target = program.Value(program.Position + 3);
            program.Update(target, program.Value(l) * program.Value(r));
        }

        public override string ToString() => nameof(Multiply);
    }
    public readonly struct Exit : Opcode
    {
        public int Value => 99;

        public void Execute(Intcode program)
            => program.Position = program.Size;

        public override string ToString() => nameof(Exit);
    }

    public readonly struct Unknown : Opcode
    {
        internal Unknown(int value) => Value = value;
        public int Value { get; }

        public void Execute(Intcode program)
            => throw new InvalidOperationException(Value.ToString());

        public override string ToString() => Value.ToString();
    }

    public static class OpcodeExtensions
    {
        public static Opcode Opcode(this int value)
            => value switch
            {
                01 => new Add(),
                02 => new Multiply(),
                99 => new Exit(),
                _ => new Unknown(value),
            };
    }
}
