namespace Advent_of_Code_2019;

public class RunArguments(bool haltOnInput, bool haltOnOutput, params Int[] inputs)
{
    public static RunArguments Empty() => new();

    public RunArguments(params Int[] inputs)
        : this(false, false, inputs) => Do.Nothing();

    public bool HaltOnInput { get; } = haltOnInput;
    public bool HaltOnOutput { get; } = haltOnOutput;
    public IReadOnlyCollection<Int> Inputs { get; } = [.. inputs];
}
