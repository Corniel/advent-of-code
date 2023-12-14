using System.Diagnostics.Contracts;

namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Inputs<T>(IReadOnlyList<T> input) : IReadOnlyList<T>
{
    private readonly IReadOnlyList<T> collection = input;

    public T this[int index] => collection[index];

    public int Count => collection.Count;
    
    public override string ToString() => string.Join(';', collection);

    [Pure]
    public T[] Edit() => collection.ToArray();

    [Obsolete("Use Edit().", error: true)]
    public T[] ToArray() => throw new NotSupportedException();

    [Obsolete("Use As().", error: true)]
    public IEnumerable<TOut> Select<TOut>(Func<T, TOut> selector) => throw new NotSupportedException();

    public IEnumerable<TOut> As<TOut>(Func<T, TOut> selector) => collection.Select(selector);

    public IEnumerator<T> GetEnumerator() => collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
