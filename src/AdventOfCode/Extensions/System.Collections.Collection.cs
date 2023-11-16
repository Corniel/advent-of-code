namespace System.Collections;

public static class CollectionExtensions
{
    public static bool NotEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Any();

    public static bool NotEmpty<T>(this T[] array) => array.Length != 0;

    public static bool NotEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict) => dict.Count != 0;

    public static bool NotEmpty<T>(this List<T> list) => list.Count != 0;

    public static bool NotEmpty<T>(this Queue<T> queue) => queue.Count != 0;

    public static bool NotEmpty<T>(this Stack<T> stack) => stack.Count != 0;
}
