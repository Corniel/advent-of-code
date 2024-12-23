namespace Advent_of_Code_2019;

[DebuggerDisplay("{Pointer}, Size = {Size}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public class Computer : IEnumerable<Int>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly List<Int> memory = [];

    public Computer(IEnumerable<Int> numbers) => memory.AddRange(numbers);

    public static Computer Parse(string str) => new(str.BigInts());

    public int Size => memory.Count;
    public int Pointer { get; private set; }
    public int PointerOffset { get; private set; }
    public ComputerState State { get; private set; }
    public bool Finished => State == ComputerState.Finished;

    public Queue<Int> Inputs { get; private set; } = new();

    public Computer WarmUp()
    {
        Run(new RunArguments(true, false));
        return this;
    }
    public Results Run(RunArguments arguments = null)
    {
        arguments ??= RunArguments.Empty();
        Inputs.EnqueueRange(arguments.Inputs);
        var output = new List<Int>();
        var continueOnInput = PreRun();

        while (State == ComputerState.Running)
        {
            var opcode = ReadOpcode();

            if (opcode.Instruction == 03 && arguments.HaltOnInput && !continueOnInput)
            {
                State = ComputerState.HaltOnInput; break;
            }
            continueOnInput = false;

            switch (opcode.Instruction)
            {
                case 01: Add(opcode); break;
                case 02: Multiply(opcode); break;
                case 03: Input(opcode); break;
                case 04: Output(opcode, output);
                    if (arguments.HaltOnOutput) { State = ComputerState.HaltOnOutput; }
                    break;
                case 05: JumpIf(true, opcode); break;
                case 06: JumpIf(false, opcode); break;
                case 07: LessThen(opcode); break;
                case 08: Equals(opcode); break;
                case 09: RelativeBase(opcode); break;
                case 99: State = ComputerState.Finished; break;
                default: throw UnknownInstruction.For(opcode.Instruction);
            }
        }
        return new(memory[0], output);
    }

    public Computer Copy() => new(memory)
    {
        Pointer = Pointer,
        PointerOffset = PointerOffset,
        State = State,
        Inputs = Inputs.Copy(),
    };

    public Computer Update(int position, Int value)
    {
        Write(position, value);
        return this;
    }

    bool PreRun()
    {
        if (State == ComputerState.HaltOnInput)
        {
            Pointer--;
            State = ComputerState.Running;
            return true;
        }
        else if (!Finished) { State = ComputerState.Running; }
        return false;
    }

    Opcode ReadOpcode() => new((int)Read());
    void Add(Opcode opcode) => Execute(opcode, (p1, p2) => p1 + p2);
    void Multiply(Opcode opcode) => Execute(opcode, (p1, p2) => p1 * p2);
    void Input(Opcode opcode)
    {
        var p1 = (int)ReadImmediate(opcode.P1);
        var value = Inputs.Dequeue();
        Write(p1, value);
    }
    void Output(Opcode opcode, List<Int> output)
    {
        var p1 = Read(opcode.P1);
        output.Add(p1);
    }
    void JumpIf(bool condition, Opcode opcode)
    {
        var p1 = Read(opcode.P1);
        var target = (int)Read(opcode.P2);
        if (p1 != 0 == condition)
        {
            if (target < 0) { throw new OutOfMemory(); }
            else { Pointer = target; }
        }
    }
    void LessThen(Opcode opcode) => Execute(opcode, (p1, p2) => p1 < p2 ? 1 : 0);
    void Equals(Opcode opcode) => Execute(opcode, (p1, p2) => p1 == p2 ? 1 : 0);
    void RelativeBase(Opcode opcode) => PointerOffset += (int)Read(opcode.P1);

    void Execute(Opcode opcode, Func<Int, Int, Int> function)
    {
        var p1 = Read(opcode.P1);
        var p2 = Read(opcode.P2);
        var target = (int)ReadImmediate(opcode.P3);
        var value = function(p1, p2);
        Write(target, value);
    }

    Int ReadImmediate(Mode mode) => mode switch
    {
        Mode.Position => Read(),
        Mode.Relative => Read() + PointerOffset,
        _ => throw new InvalidOperationException(),
    };
    Int Read(Mode mode) => mode switch
    {
        Mode.Position => Read((int)Read()),
        Mode.Relative => Read((int)Read() + PointerOffset),
        Mode.Immediate => Read(),
        _ => throw new InvalidOperationException(),
    };
    Int Read() => Read(Pointer++);
    Int Read(int position) => memory[InMemory(position)];
    void Write(int position, Int value)
    {
        position = InMemory(position);
        memory[position] = value;
    }
    int InMemory(int position)
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
}
