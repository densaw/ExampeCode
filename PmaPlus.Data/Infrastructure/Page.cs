using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Data
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page,
                                                                        int pageSize)
        {
            return source
              .Skip((page - 1) * pageSize)
              .Take(pageSize);
        }

        public static IEnumerable<T> Paged<T>(this IEnumerable<T> source, int page,
                                                                       int pageSize)
        {
            return source
              .Skip((page - 1) * pageSize)
              .Take(pageSize);
        }

        public static IQueryable<T> OrderQuery<T, TProperty>(this IQueryable<T> source, string propertyName, Expression<Func<T, TProperty>> defaulProperty)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {

                return source.OrderBy(defaulProperty);
            }

            var paramterExpression = Expression.Parameter(typeof(T));

            var orderExpression = (Expression<Func<T, string>>)Expression.Lambda(Expression.PropertyOrField(paramterExpression, propertyName), paramterExpression);

            return source.OrderBy(orderExpression);
        }

        public static IEnumerable<T> OrderQuery<T, TProperty>(this IEnumerable<T> list, string sortExpression, Expression<Func<T, TProperty>> defaulProperty, bool direction = false)
        {
            PropertyInfo prop = typeof(T).GetProperty(sortExpression, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
            {
                if (direction)
                    return list.AsQueryable().OrderByDescending(defaulProperty);
                else
                    return list.AsQueryable().OrderBy(defaulProperty);
            }

            if (direction)
                return list.OrderByDescending(x => prop.GetValue(x, null));
            else
                return list.OrderBy(x => prop.GetValue(x, null));

        }
    }
}
