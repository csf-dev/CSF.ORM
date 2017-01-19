using System;
using System.Linq;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Interface representing an eager-fetching request.
  /// </summary>
  public interface IFetchRequest<TQueried>
  {
    /// <summary>
    /// Gets the original query which was used to create the current instance.
    /// </summary>
    /// <returns>The original query.</returns>
    IQueryable<TQueried> GetOriginalQuery();
  }

  /// <summary>
  /// Interface representing an eager-fetching request.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This code came from the following Stack Overflow answer: http://stackoverflow.com/a/5742158
  /// </para>
  /// </remarks>
  public interface IFetchRequest<TQueried, TFetch> : IOrderedQueryable<TQueried>, IFetchRequest<TQueried> { }
}

