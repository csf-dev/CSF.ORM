using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Extension methods for <c>IQueryable&lt;T&gt;</c> instances.
  /// </summary>
  public static class QueryableExtensions
  {
    #region extension methods

    /// <summary>
    /// Very similar to <c>.Any()</c> but internally uses <c>.Count()</c> and then checks the result is more than zero.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The rationale for this is that using <c>.Any()</c>, NHibernate will perform a query and fetch the whole first
    /// entity instance.  Sometimes this could be slower than performing a count of all matched rows and returning that
    /// result.
    /// </para>
    /// <para>
    /// The recommendation is to use <c>.Any()</c> by default, and if you discover a performance issue fetching a
    /// whole entity (when you only wanted to know if it existed or not) switch to <c>.AnyCount()</c> and see if that
    /// improves things.
    /// </para>
    /// <para>
    /// Your mileage is most likely to depend upon the width of your database tables (the number of properties in the
    /// entity).
    /// </para>
    /// </remarks>
    /// <returns><c>true</c> if the query would return any items; otherwise, <c>false</c>.</returns>
    /// <param name="query">The query.</param>
    /// <typeparam name="T">The queried type.</typeparam>
    public static bool AnyCount<T>(this IQueryable<T> query)
    {
      if(query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      return query.Count() > 0;
    }

    /// <summary>
    /// Very similar to <c>.Any()</c> but internally uses <c>.Count()</c> and then checks the result is more than zero.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The rationale for this is that using <c>.Any()</c>, NHibernate will perform a query and fetch the whole first
    /// entity instance.  Sometimes this could be slower than performing a count of all matched rows and returning that
    /// result.
    /// </para>
    /// <para>
    /// The recommendation is to use <c>.Any()</c> by default, and if you discover a performance issue fetching a
    /// whole entity (when you only wanted to know if it existed or not) switch to <c>.AnyCount()</c> and see if that
    /// improves things.
    /// </para>
    /// <para>
    /// Your mileage is most likely to depend upon the width of your database tables (the number of properties in the
    /// entity).
    /// </para>
    /// </remarks>
    /// <returns><c>true</c> if the query would return any items; otherwise, <c>false</c>.</returns>
    /// <param name="query">The query.</param>
    /// <param name="predicate">A predicate for elements to consider.</param>
    /// <typeparam name="T">The queried type.</typeparam>
    public static bool AnyCount<T>(this IQueryable<T> query, Expression<Func<T,bool>> predicate)
    {
      if(query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      var count = (predicate != null)? query.Count(predicate) : query.Count();

      return count > 0;
    }

    #endregion
  }
}

