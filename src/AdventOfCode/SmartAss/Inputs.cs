using System.Reflection;
using System.Runtime.CompilerServices;

namespace SmartAss;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
[CollectionBuilder(typeof(Inputs), nameof(Inputs.Create))]
public readonly struct Inputs<T>(IReadOnlyList<T> input) : IReadOnlyList<T>
{
    private readonly T[] collection = input as T[] ?? [.. input];

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

    public TOut[] ToArray<TOut>(Func<T, TOut> selector)
    {
        var array = new TOut[collection.Length];
        for (int i = 0; i < collection.Length; i++)
            array[i] = selector(collection[i]);
        return array;
    }

    public IEnumerator<T> GetEnumerator() => ((IReadOnlyCollection<T>)collection).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public static class Inputs
{
    public static Inputs<T> Create<T>(ReadOnlySpan<T> values) => new([.. values]);

    public static object Parse(Type type, string str)
        => parses.MakeGenericMethod(type).Invoke(null, [str]);

    public static Inputs<T> Parses<T>(string str)
    {
        var parse = ParseMethod<T>();
        return new Inputs<T>([.. str.Lines().Select(l => (T)parse.Invoke(null, [l]))]);
    }
    private static MethodInfo ParseMethod<T>()
        => typeof(T).GetMethods(Flags).Where(m
            => m.Name == nameof(Parse)
            && m.ReturnType == typeof(T)
            && m.GetParameters() is { Length: 1 } pars
            && pars[0].ParameterType == typeof(string)).ToArray() is { Length: 1 } methods
        ? methods[0]
        : throw new InvalidOperationException($"Could not resolve {typeof(T).ToCSharpString(true)}.Parse(string).");

    private static readonly BindingFlags Flags = BindingFlags.Static | BindingFlags.Public;
    private static readonly MethodInfo parses = typeof(Inputs).GetMethod(nameof(Parses), Flags);
}
