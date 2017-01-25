using System;
using NHibernate.Linq;
using System.Linq;
using System.Collections.Generic;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Basic implementation of <c>IFetchRequest</c> which wraps an NHibernate fetch request.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This code came from the following Stack Overflow answer: http://stackoverflow.com/a/5742158
  /// </para>
  /// </remarks>
  public class NHibernateFetchRequestWrapper<TQueried, TFetch> : FetchRequestWrapper<TQueried, TFetch>
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.NHibernateFetchRequest{TQueried, TFetch}"/> class.
    /// </summary>
    /// <param name="wrappedRequest">Wrapped request.</param>
    public NHibernateFetchRequestWrapper(INhFetchRequest<TQueried, TFetch> wrappedRequest) : base(wrappedRequest) {}

    #endregion
  }
}

