namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Lines(IReadOnlyList<string> lines) : IReadOnlyList<string>
{
    private readonly IReadOnlyList<string> collection = lines;

    public string this[int index] => lines[index];

    public int Count => lines.Count;
    
    public override string ToString() => string.Join(';', collection);

    [Obsolete("Use As().", error: true)]
    public IEnumerable<T> Select<T>(Func<string, T> selector) => As(selector);

    public IEnumerable<T> As<T>(Func<string, T> selector) => collection.Select(selector);

    public IEnumerator<string> GetEnumerator() => lines.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
