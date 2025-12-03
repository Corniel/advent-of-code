using System.Runtime.CompilerServices;

namespace System.Collections;

public static class CollectionExtensions
{
    extension<T>(IReadOnlyCollection<T> collection)
    {
        /// <summary>Indicates that the collection is not empty.</summary>
        [OverloadResolutionPriority(10)]
        public bool NotEmpty => collection.Count is not 0;
    }

    extension<T>(IEnumerable<T> enumerable)
    {
        /// <summary>Indicates that the collection is not empty.</summary>
        [OverloadResolutionPriority(-1)]
        public bool NotEmpty => enumerable.Any();
    }

    public static int Count<T>(this IEnumerable<T> enumerable, T item) => enumerable.Count(i => i.Equals(item));

    public static T? FirstOrNone<T>(this IEnumerable<T> enumerable, Predicate<T> predicate) where T : struct
    {
        foreach (var item in enumerable)
        {
            if (predicate(item)) return item;
        }
        return null;
    }

    public static IEnumerable<T> WithValue<T>(this IEnumerable<T?> enumerable) where T : struct
        => enumerable.OfType<T>();
}
