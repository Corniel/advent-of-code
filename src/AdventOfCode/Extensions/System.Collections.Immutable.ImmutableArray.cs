using System.Runtime.CompilerServices;

namespace System.Collections.Immutable;

public static class Immutables
{
    extension<T>(IReadOnlyList<T> array)
    {
        [Pure]
        [OverloadResolutionPriority(10)]
        public ImmutableArray<TOut> Fix<TOut>(Func<T, TOut> selector)
            => [.. array.Select(selector)];

        [Pure]
        [OverloadResolutionPriority(10)]
        public IEnumerable<TOut> As<TOut>(Func<T, TOut> selector)
            => array.Select(selector);

        [Pure]
        public bool Exists(Func<T, bool> predicate)
        {
            for (var i = 0; i < array.Count; i++)
                if (predicate(array[i])) return true;

            return false;
        }


        [Pure]
        public T[] Mutable() => [.. array];
    }

    extension<T>(IEnumerable<T> source)
    {
        [Pure]
        public ImmutableArray<T> Fix() => [.. source];

        [Pure]
        public ImmutableArray<TOut> Fix<TOut>(Func<T, TOut> selector)
            => [.. source.Select(selector)];

        [Pure]
        public ImmutableArray<TOut> FixMany<TOut>(Func<T, IEnumerable<TOut>> selector)
                => [.. source.SelectMany(selector)];
    }
}
