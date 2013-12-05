using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// NHibernate implementation of the generic <c>IQuery</c> type, which wraps an NHibernate <c>ISession</c>, using it
  /// as the query source.
  /// </summary>
  public class NHibernateQuery<TQueried> : IQuery<TQueried>
    where TQueried : class
  {
    #region fields

    private ISession _session;

    #endregion

    #region IQuery implementation

    /// <summary>
    ///  Gets a new queryable data-source. 
    /// </summary>
    public IQueryable<TQueried> Query ()
    {
      return _session.Query<TQueried>();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the generic <c>NHibernateQuery</c> class.
    /// </summary>
    /// <param name='session'>
    /// An NHibernate ISession to wrap.
    /// </param>
    public NHibernateQuery(ISession session)
    {
      if(session == null)
      {
        throw new ArgumentNullException("session");
      }

      _session = session;
    }

    #endregion
  }
}

