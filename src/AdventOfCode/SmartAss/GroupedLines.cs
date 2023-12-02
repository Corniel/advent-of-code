namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct GroupedLines(IReadOnlyList<string[]> groups) : IReadOnlyList<string[]>
{
    private readonly IReadOnlyList<string[]> collection = groups;

    public string[] this[int index] => groups[index];

    public int Count => groups.Count;
    
    public override string ToString() => string.Join(';', collection.SelectMany(g => g));

    public IEnumerator<string[]> GetEnumerator() => groups.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
