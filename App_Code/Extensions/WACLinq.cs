
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
/// <summary>
/// Summary description for 
/// </summary>
/// 
namespace WAC_Extensions
{
    public static class Extensions
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> predicate)
        {
            return source.Distinct(new PredicateEqualityComparer<TSource>(predicate));
        }

        public static bool In<T>(this T obj, IEnumerable<T> values)
        {
            return values.Contains(obj);
        }

    }

    public class PredicateEqualityComparer<T> : EqualityComparer<T>
    {
        private Func<T, T, bool> predicate;

        public PredicateEqualityComparer(Func<T, T, bool> predicate)
            : base()
        {
            this.predicate = predicate;
        }

        public override bool Equals(T x, T y)
        {
            if (x != null)
            {
                return ((y != null) && this.predicate(x, y));
            }

            if (y != null)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode(T obj)
        {
            // Always return the same value to force the call to IEqualityComparer<T>.Equals
            return 0;
        }

    }
    public static class DynamicOrderBy
    {

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                      bool desc) where TEntity : class
        {

            string command = desc ? "OrderByDescending" : "OrderBy";

            var type = typeof(TEntity);

            var property = type.GetProperty(orderByProperty);

            var parameter = Expression.Parameter(type, "p");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },

                                   source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);

        }

    }
}