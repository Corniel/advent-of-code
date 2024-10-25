namespace SmartAss;

internal static class Transposer
{
    public static IEnumerable<TResult> Transpose<TSource, TResult>(this TResult initial, IEnumerable<TSource> sources, Func<TResult, TSource, TResult> selector)
    {
        var next = initial;
        yield return next;
        foreach (var source in sources)
        {
            next = selector(next, source);
            yield return next;
        }
    }
}
