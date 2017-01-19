using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using System.Collections.Generic;

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

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>Fetch</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <param name="query">The source query.</param>
    /// <param name="relatedObjectSelector">Related object selector.</param>
    /// <typeparam name="TOriginating">The queried type.</typeparam>
    /// <typeparam name="TRelated">The related object type.</typeparam>
    public static IFetchRequest<TOriginating, TRelated> Fetch<TOriginating, TRelated>(this IQueryable<TOriginating> query,
                                                                                      Expression<Func<TOriginating, TRelated>> relatedObjectSelector)
    {
      IFetchRequest<TOriginating, TRelated> output;

      if(query != null
         && query.Provider is DefaultQueryProvider)
      {
        var trueQuery = GetTrueQuery(query);
        var fetch = EagerFetchingExtensionMethods.Fetch(trueQuery, relatedObjectSelector);
        output = new NHibernateFetchRequestWrapper<TOriginating, TRelated>(fetch);
      }
      else
      {
        output = new FetchRequestWrapper<TOriginating, TRelated>(query);
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>FetchMany</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <param name="query">The source query.</param>
    /// <param name="relatedObjectSelector">Related object selector.</param>
    /// <typeparam name="TOriginating">The queried type.</typeparam>
    /// <typeparam name="TRelated">The related object type.</typeparam>
    public static IFetchRequest<TOriginating, TRelated> FetchMany<TOriginating, TRelated>(this IQueryable<TOriginating> query,
                                                                                              Expression<Func<TOriginating, IEnumerable<TRelated>>> relatedObjectSelector)
    {
      IFetchRequest<TOriginating, TRelated> output;

      if(query != null
               && query.Provider is DefaultQueryProvider)
      {
        var trueQuery = GetTrueQuery(query);
        var fetch = EagerFetchingExtensionMethods.FetchMany(trueQuery, relatedObjectSelector);
        output = new NHibernateFetchRequestWrapper<TOriginating, TRelated>(fetch);
      }
      else
      {
        output = new FetchRequestWrapper<TOriginating, TRelated>(query);
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>FetchMany</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <param name="query">The source query.</param>
    /// <param name="relatedObjectSelector">Related object selector.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TFetch">The previously-fetched type.</typeparam>
    /// <typeparam name="TRelated">The related object type.</typeparam>
    public static IFetchRequest<TQueried, TRelated> ThenFetch<TQueried, TFetch, TRelated>(this IFetchRequest<TQueried, TFetch> query,
                                                                                              Expression<Func<TFetch, TRelated>> relatedObjectSelector)
    {
      IFetchRequest<TQueried, TRelated> output;

      if(query != null
               && query.Provider is DefaultQueryProvider)
      {
        var parentFetch = GetFetchRequest<TQueried,TFetch>(query);
        var fetch = EagerFetchingExtensionMethods.ThenFetch(parentFetch, relatedObjectSelector);
        output = new NHibernateFetchRequestWrapper<TQueried, TRelated>(fetch);
      }
      else
      {
        output = new FetchRequestWrapper<TQueried, TRelated>(query);
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>ThenFetchMany</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <param name="query">The source query.</param>
    /// <param name="relatedObjectSelector">Related object selector.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TFetch">The previously-fetched type.</typeparam>
    /// <typeparam name="TRelated">The related object type.</typeparam>
    public static IFetchRequest<TQueried, TRelated> ThenFetchMany<TQueried, TFetch, TRelated>(this IFetchRequest<TQueried, TFetch> query,
                                                                                                  Expression<Func<TFetch, IEnumerable<TRelated>>> relatedObjectSelector)
    {
      IFetchRequest<TQueried, TRelated> output;

      if(query != null
               && query.Provider is DefaultQueryProvider)
      {
        var parentFetch = GetFetchRequest<TQueried,TFetch>(query);
        var fetch = EagerFetchingExtensionMethods.ThenFetchMany(parentFetch, relatedObjectSelector);
        output = new NHibernateFetchRequestWrapper<TQueried, TRelated>(fetch);
      }
      else
      {
        output = new FetchRequestWrapper<TQueried, TRelated>(query);
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>ToFuture</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <returns>An enumerable object which represents a future query.</returns>
    /// <param name="query">The source query.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    public static IEnumerable<TQueried> ToFuture<TQueried>(this IQueryable<TQueried> query)
    {
      IEnumerable<TQueried> output;

      if(query != null && query.Provider is DefaultQueryProvider)
      {
        output = LinqExtensionMethods.ToFuture(query);
      }
      else
      {
        output = query.AsEnumerable();
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>ToFutureValue</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <returns>A future value object.</returns>
    /// <param name="query">The source query.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    public static IFutureValue<TQueried> ToFutureValue<TQueried>(this IQueryable<TQueried> query)
    {
      NHibernate.IFutureValue<TQueried> output;

      if(query != null && query.Provider is DefaultQueryProvider)
      {
        output = new FutureValueWrapper<TQueried>(() => LinqExtensionMethods.ToFutureValue(query).Value);
      }
      else
      {
        output = new EnumerableFutureValueWrapper<TQueried>(() => query.AsEnumerable());
      }

      return output;
    }

    /// <summary>
    /// Provides a wrapper for NHibernate's Linq <c>ToFutureValue</c> functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method deals gracefully with scenarios in which the queryable instance is not an NHibernate queryable,
    /// and becomes a no-op under those situations.
    /// </para>
    /// </remarks>
    /// <returns>A future value object.</returns>
    /// <param name="query">The source query.</param>
    /// <param name="expression">A value selector, which creates the future value.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public static IFutureValue<TValue> ToFutureValue<TQueried,TValue>(this IQueryable<TQueried> query,
                                                                        Expression<Func<IQueryable<TQueried>,TValue>> expression)
    {
      IFutureValue<TValue> output;

      if(query != null && query.Provider is DefaultQueryProvider)
      {
        output = new FutureValueWrapper<TValue>(() => LinqExtensionMethods.ToFutureValue(query,expression).Value);
      }
      else
      {
        output = new FutureValueWrapper<TValue>(() => expression.Compile().Invoke(query));
      }

      return output;
    }


    #endregion

    #region methods

    private static IQueryable<TQueried> GetTrueQuery<TQueried>(IQueryable<TQueried> query)
    {
      if(query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      var request = query as IFetchRequest<TQueried>;
      return (request != null)? request.GetOriginalQuery() : query;
    }

    private static INhFetchRequest<TQueried,TFetch> GetFetchRequest<TQueried,TFetch>(IQueryable<TQueried> query)
    {
      if(query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      var output = GetTrueQuery(query);
      return output as INhFetchRequest<TQueried,TFetch>;
    }

    #endregion
  }
}

