using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WAC_Extensions
{
    public class NullableIntComparer : IComparer<int?>
    {
        public int Compare(int? x, int? y)
        {
            return (x == null)
                   ? ((y == null) ? 0 : -1)
                   : x.Value.CompareTo(y);
        }
    }
    public class NullableStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return (x == null)
                   ? ((y == null) ? 0 : -1)
                   : x.CompareTo(y);
        }
    }
    public static class SortExtensions
    {
        /// <summary>
        /// Sorts an IList(T) in place.
        /// </summary>
        /// <typeparam name="T">The type of objects the list holds</typeparam>
        /// <param name="list">The list to be sorted</param>
        /// <param name="comparison">A delegate defining how to compare two items in the list</param>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }

        /// <summary>
        /// Convenience method on IEnumerable(T) to allow passing of a Comparison delegate
        /// to the OrderBy method.
        /// </summary>
        /// <typeparam name="T">The type of objects the list holds</typeparam>
        /// <param name="list">The list to be sorted</param>
        /// <param name="comparison">A delegate defining how to compare two items in the list</param>
        /// <returns>An IOrderedEnumerable(T)</returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }
        public static int Compare(string x, string y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.CompareTo(y);
        }
        public static int Compare(int? x, int? y)
        {
            int result;
            if (ReferenceEquals(x, y)) result = 0;
            else if (x == null) result = -1;
            else if (y == null) result = 1;
            else if (x > y) result = 1;
            else if (x < y) result = -1;
            else result = 0;
            return result;
        }
       
    }

    /// <summary>
    /// Wraps a generic Comparison(T) delegate in an IComparer to make it easy 
    /// to use a lambda expression for methods that take an IComparer or IComparer(T)
    /// </summary>
    /// <typeparam name="T">The type being compared</typeparam>
    public class ComparisonComparer<T> : IComparer<T>, IComparer
    {
        private readonly Comparison<T> _comparison;

        ///<summary>
        /// Wraps a generic Comparison(T) delegate so that we may easily use a lambda 
        /// expression with methods that accept an IComparer or IComparer(T).
        ///</summary>
        ///<param name="comparison">A delegate that compares two objects of type T</param>
        public ComparisonComparer(Comparison<T> comparison)
        {
            _comparison = comparison;
        }
        public int Compare(int? x, int? y)
        {
            return (x == null)
                   ? ((y == null) ? 0 : -1)
                   : x.Value.CompareTo(y);
        }
        public int Compare(string x, string y)
        {
            return (x == null)
                   ? ((y == null) ? 0 : -1)
                   : x.CompareTo(y);
        }
        public int Compare(T x, T y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return _comparison(x, y);
        }

        public int Compare(object o1, object o2)
        {
            return _comparison((T)o1, (T)o2);
        }
    }
}
