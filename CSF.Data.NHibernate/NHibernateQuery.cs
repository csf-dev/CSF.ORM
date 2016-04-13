using System;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// NHibernate implementation of the generic <c>IQuery</c> type, which wraps an NHibernate <c>ISession</c>, using it
  /// as the query source.
  /// </summary>
  public class NHibernateQuery : IQuery
  {
    #region fields

    private ISession _session;

    #endregion

    #region public API

    /// <summary>
    /// Creates an instance of the given object-type, based upon a theory that it exists in the underlying data-source.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method will always return a non-null object instance, even if the underlying object does not exist in the
    /// data source.  If a 'thoery object' is created for an object which does not actually exist, then an exception
    /// could be thrown if that theory object is used.
    /// </para>
    /// </remarks>
    /// <param name="identityValue">The identity value for the object to retrieve.</param>
    /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
    public TQueried Theorise<TQueried>(object identityValue) where TQueried : class
    {
      return _session.Load<TQueried>(identityValue);
    }

    /// <summary>
    /// Gets a single instance from the underlying data source, identified by an identity value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method will either get an object instance, or it will return <c>null</c> (if no instance is found).
    /// </para>
    /// </remarks>
    /// <param name="identityValue">The identity value for the object to retrieve.</param>
    /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
    public TQueried Get<TQueried>(object identityValue) where TQueried : class
    {
      return _session.Get<TQueried>(identityValue);
    }

    /// <summary>
    /// Gets a new queryable data-source.
    /// </summary>
    /// <typeparam name="TQueried">The type of queried-for object.</typeparam>
    public IQueryable<TQueried> Query<TQueried>() where TQueried : class
    {
      return _session.Query<TQueried>();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.Data.NHibernate.NHibernateQuery"/> class.
    /// </summary>
    /// <param name="session">Session.</param>
    public NHibernateQuery(ISession session)
    {
      if(session == null)
      {
        throw new ArgumentNullException(nameof(session));
      }

      _session = session;
    }

    #endregion
  }
}

