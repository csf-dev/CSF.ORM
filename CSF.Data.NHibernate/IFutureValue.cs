using System;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Interface representing a repository value which will be lazy-loaded from the database on first access.
  /// </summary>
  public interface IFutureValue<TValue>
  {
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    TValue Value { get; }
  }
}

