using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.eSIS.Core
{
    public static class ExtensionMethods
    {
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }

        /// <summary>
        /// Searches a list and adds an item if it is not already present
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list</param>
        /// <param name="item">The item to add if not present</param>
        public static void AddIfNotPresent<T>(this ICollection<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Clears all of the items in a list and adds a new item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void ClearAndAdd<T>(this ICollection<T> list, T item)
        {
            list.Clear();
            list.Add(item);
        }

        /// <summary>
        /// Returns all of the distinct values of a specific property
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">The list</param>
        /// <param name="keySelector">The property you would like specific values for</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(i => i.First());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime SetToSQLEOD(this DateTime dt)
        {
            //DateTime in SQL Server is currently set to round off to 3 millisecond increments, so below is the highest time value for a given day
            return dt.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(997);
        }

        public static DateTime SetToSQLBOD(this DateTime dt)
        {
            //DateTime in SQL Server is currently set to round off to 3 millisecond increments, so below is the highest time value for a given day
            return dt.Date.AddHours(-dt.Date.Hour).AddMinutes(-dt.Date.Minute).AddSeconds(-dt.Date.Second).AddMilliseconds(-dt.Date.Millisecond);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime? SetToSQLEOD(this DateTime? dt)
        {
            DateTime? returnValue = null;
            if (dt.HasValue)
            {
                returnValue = dt.Value.SetToSQLEOD();
            }

            return returnValue;
        }

        public static IEnumerable<List<T>> InSetsOf<T>(this IEnumerable<T> source, int max)
        {
            var toReturn = new List<T>(max);
            foreach (var item in source)
            {
                toReturn.Add(item);
                if (toReturn.Count == max)
                {
                    yield return toReturn;
                    toReturn = new List<T>(max);
                }
            }
            if (toReturn.Any())
            {
                yield return toReturn;
            }
        }

        public static bool IsWeekend(this DateTime dt)
        {
            return ((dt.DayOfWeek == DayOfWeek.Saturday) || (dt.DayOfWeek == DayOfWeek.Sunday));
        }
    }
}
