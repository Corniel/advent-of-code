namespace SmartAss.Circuits;

public class Circuit<TValue> : SortedDictionary<string, CircuitNode<TValue>> where TValue : struct
{
    public Variable NewVariable(string name) => new(name, this);

    public sealed record Variable(string Name, Circuit<TValue> Variables) : CircuitNode<TValue>
    {
        protected override TValue? Execute()
            => Variables.TryGetValue(Name, out var node)
            ? node.Output
            : null;

        public bool Declared => Variables.ContainsKey(Name);

        public override string ToString()
            => Execute() is TValue value
            ? $"{Name}: {value}"
            : $"{Name}: {(Declared ? "{no value}" : "{not declared}")}";
    }

    public sealed record Constant(TValue Const) : CircuitNode<TValue>
    {
        protected override TValue? Execute() => Const;
        public override string ToString() => Const.ToString();
    }

    public sealed record Assignment(CircuitNode<TValue> Other) : CircuitNode<TValue>
    {
        protected override TValue? Execute() => Other.Output;
        public override string ToString() => $"Assign: {Other}";
    }
}
