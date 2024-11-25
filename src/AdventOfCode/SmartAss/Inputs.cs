using System.Diagnostics.Contracts;

namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Inputs<T>(IReadOnlyList<T> input) : IReadOnlyList<T>
{
    private readonly T[] collection = input as T[] ?? [..input];

    public T this[int index] => collection[index];

    public Slice<T> this[Range range] => new Slice<T>(collection)[range];

    public int Count => collection.Length;
    
    public override string ToString() => string.Join(';', collection);

    [Obsolete("Use [..count] instead.", error: true)]
    public T[] Skip(int count) => throw new NotSupportedException();

    [Obsolete("Use [count..] instead.", error: true)]
    public T[] Take(int count) => throw new NotSupportedException();

    [Pure]
    public T[] Copy() => [.. collection];

    [Obsolete("Use Copy() instead.", error: true)]
    public T[] ToArray() => throw new NotSupportedException();

    [Obsolete("Use As() instead.", error: true)]
    public IEnumerable<TOut> Select<TOut>(Func<T, TOut> selector) => throw new NotSupportedException();

    public IEnumerable<TOut> As<TOut>(Func<T, TOut> selector) => collection.Select(selector);

    public IEnumerator<T> GetEnumerator() => ((IReadOnlyCollection<T>)collection).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
