namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct GroupedLines(IReadOnlyList<string[]> groups) : IReadOnlyList<string[]>
{
    private readonly IReadOnlyList<string[]> collection = groups;

    public string[] this[int index] => collection[index];

    public Slice<string[]> this[Range range] => new Slice<string[]>(collection)[range];

    public int Count => collection.Count;

    public override string ToString() => string.Join(';', collection.SelectMany(g => g));

    public IEnumerator<string[]> GetEnumerator() => collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
