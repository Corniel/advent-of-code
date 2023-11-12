﻿namespace System;

public static class ArrayExtensions
{
    public static bool Exists<T>(this T[] array, Predicate<T> predicate) => Array.Exists(array, predicate);

    public static bool TrueForAll<T>(this T[] array, Predicate<T> predicate) => Array.TrueForAll(array, predicate);
}