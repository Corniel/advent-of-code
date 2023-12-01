namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Lines(IReadOnlyList<string> lines) : IReadOnlyList<string>
{
    private readonly IReadOnlyList<string> collection = lines;

    public string this[int index] => lines[index];

    public int Count => lines.Count;
    
    public override string ToString() => string.Join(';', collection);

    public IEnumerator<string> GetEnumerator() => lines.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
