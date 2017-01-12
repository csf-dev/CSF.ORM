using System;
using NHibernate;
using NHibernate.Impl;
using NHibernate.Proxy;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Extension methods for NHibernate <c>ISession</c>.
  /// </summary>
  public static class SessionExtensions
  {
    #region extension methods

    /// <summary>
    /// Unproxies/unwraps the given object, which might be an NHibernate proxy.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the object is a proxy then it is unproxied.  If the object is not a proxy then it is returned unaltered.
    /// If the ISession is not a proper NHibernate implementation then the input object is returned unaltered.
    /// </para>
    /// </remarks>
    /// <param name="session">An <c>ISession</c> implementation.</param>
    /// <param name="possibleProxy">An object which might be an NHibernate proxy.</param>
    /// <typeparam name="T">The object type.</typeparam>
    public static T Unproxy<T>(this ISession session, T possibleProxy) where T : class
    {
      if(session == null)
      {
        throw new ArgumentNullException(nameof(session));
      }

      if(Object.ReferenceEquals(possibleProxy, null))
      {
        return null;
      }
      else if(!(session is SessionImpl))
      {
        return possibleProxy;
      }

      if (!NHibernateUtil.IsInitialized(possibleProxy))
      {
        NHibernateUtil.Initialize(possibleProxy);
      }

      if (possibleProxy is INHibernateProxy)
      {    
        return (T) session.GetSessionImplementation().PersistenceContext.Unproxy(possibleProxy);
      }

      return possibleProxy;
    }

    #endregion
  }
}

