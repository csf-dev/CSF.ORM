using System;
using System.Linq;
using System.Collections.Generic;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Dummy implementation of <c>IFetchRequest</c>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This code came from the following Stack Overflow answer: http://stackoverflow.com/a/5742158
  /// </para>
  /// </remarks>
  public class FetchRequestWrapper<TQueried, TFetch> : IFetchRequest<TQueried, TFetch>
  {
    #region properties

    /// <summary>
    /// Gets a reference to the <c>IOrderedQueryable</c> which is wrapped by the current instance.
    /// </summary>
    /// <value>The wrapped query.</value>
    public virtual IQueryable<TQueried> WrappedQuery
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the element type.
    /// </summary>
    /// <value>The type of the element.</value>
    public virtual Type ElementType
    {
      get {
        return WrappedQuery.ElementType;
      }
    }

    /// <summary>
    /// Gets the expression.
    /// </summary>
    /// <value>The expression.</value>
    public virtual System.Linq.Expressions.Expression Expression
    {
      get {
        return WrappedQuery.Expression;
      }
    }

    /// <summary>
    /// Gets the Linq query provider.
    /// </summary>
    /// <value>The provider.</value>
    public virtual IQueryProvider Provider
    {
      get {
        return WrappedQuery.Provider;
      }
    }

    #endregion

    #region methods

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public virtual IEnumerator<TQueried> GetEnumerator()
    {
      return WrappedQuery.GetEnumerator();
    }

    /// <summary>
    /// Gets the original query which was used to create the current instance.
    /// </summary>
    /// <returns>The original query.</returns>
    public virtual IQueryable<TQueried> GetOriginalQuery()
    {
      return WrappedQuery;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return WrappedQuery.GetEnumerator();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.DummyFetchRequest{TQueried, TFetch}"/> class.
    /// </summary>
    /// <param name="wrappedQuery">Wrapped query.</param>
    public FetchRequestWrapper(IQueryable<TQueried> wrappedQuery)
    {
      if(wrappedQuery == null)
      {
        throw new ArgumentNullException(nameof(wrappedQuery));
      }

      WrappedQuery = wrappedQuery;
    }

    #endregion
  }
}

