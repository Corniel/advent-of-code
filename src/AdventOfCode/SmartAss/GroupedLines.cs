namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct GroupedLines(IReadOnlyList<string[]> groups) : IReadOnlyList<string[]>
{
    private readonly ImmutableArray<string[]> collection = [..groups];

    public string[] this[int index] => collection[index];

    public ImmutableArray<string[]> this[Range range] => collection[range];

    public int Count => collection.Length;

    [Obsolete("Use [.. this] instead", error: true)]
    [Pure]
    public IEnumerable Skip(int count) => throw new NotSupportedException();

    [Obsolete("Use [.. this] instead", error: true)]
    [Pure]
    public IEnumerable Take(int count) => throw new NotSupportedException();

    public override string ToString() => string.Join(';', collection.SelectMany(g => g));

    public IEnumerator<string[]> GetEnumerator() => collection.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
