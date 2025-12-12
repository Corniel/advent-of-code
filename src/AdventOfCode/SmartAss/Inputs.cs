using System.Reflection;

namespace SmartAss;

/// <remarks>
/// This wrapper type exists as handled better by the MS Test runner than
/// <see cref="ImmutableArray{T}"/> as test method arguments.
/// </remarks>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Inputs<T>(ImmutableArray<T> input) : IReadOnlyList<T>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<T> collection = input;

    /// <inheritdoc />
    public T this[int index] => collection[index];

    public T this[Index index] => collection[index];

    public ImmutableArray<T> this[Range range] => collection[range];

    /// <inheritdoc />
    public int Count => collection.Length;

    /// <inheritdoc cref="ImmutableArray{T}.Length" />
    public int Length => collection.Length;

    /// <inheritdoc cref="ImmutableArray{T}.AsSpan()" />
    [Pure]
    public ReadOnlySpan<T> AsSpan() => collection.AsSpan();

    /// <inheritdoc />
    public override string ToString() => string.Join(';', collection);

    [Obsolete("Use [..count] instead.", error: true)]
    public T[] Skip(int count) => throw new NotSupportedException();

    [Obsolete("Use [count..] instead.", error: true)]
    public T[] Take(int count) => throw new NotSupportedException();
    
    [Obsolete("Use Mutable() instead.", error: true)]
    public T[] ToArray() => throw new NotSupportedException();

    [Obsolete("Use As() instead.", error: true)]
    public IEnumerable<TOut> Select<TOut>(Func<T, TOut> selector) => throw new NotSupportedException();

    [Pure]
    public IEnumerable<TOut> As<TOut>(Func<T, TOut> selector) => collection.Select(selector);

    public ImmutableArray<T>.Enumerator GetEnumerator() => collection.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)collection).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)collection).GetEnumerator();
}

public static class Inputs
{
    public static Inputs<T> New<T>(params IEnumerable<T> inputs) => new([.. inputs]);
    
    public static object Parse(Type type, Type declaring, string str)
    => parses.MakeGenericMethod(type).Invoke(null, [declaring, str]);

    public static object Parse(Type type, string str)
        => parses.MakeGenericMethod(type).Invoke(null, [type, str]);

    public static Inputs<T>? Parses<T>(Type defining, string str)
    {
        if(ParseMethod<T>(defining) is not { } parse)
        {
            return defining != typeof(T)
                ? null
                : throw new InvalidOperationException($"Could not resolve {typeof(T).ToCSharpString(true)}.Parse(string).");
        }

        return new(str.Lines().Fix(l => (T)parse.Invoke(null, [l])));
    }
    private static MethodInfo ParseMethod<T>(Type defining)
        => defining.GetMethods(Flags).Where(m
            => m.Name == nameof(Parse)
            && m.ReturnType == typeof(T)
            && m.GetParameters() is { Length: 1 } pars
            && pars[0].ParameterType == typeof(string)).ToArray() is { Length: 1 } methods
        ? methods[0]
        : null;

    private static readonly BindingFlags Flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private static readonly MethodInfo parses = typeof(Inputs).GetMethod(nameof(Parses), Flags);
}
