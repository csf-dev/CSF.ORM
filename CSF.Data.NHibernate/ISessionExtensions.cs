using System;
using NHibernate;
using CSF.Entities;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Extension methods for an NHibernate ISession.
  /// </summary>
  public static class ISessionExtensions
  {
    /// <summary>
    /// Convenience method, similar to <c>ISession.Get</c> that specifically gets entity-based types.
    /// </summary>
    /// <returns>
    /// An entity instance, or a null reference.
    /// </returns>
    /// <param name='session'>
    /// An NHibernate ISession instance.
    /// </param>
    /// <param name='identity'>
    /// An identity that refers to a specific entity type.
    /// </param>
    /// <typeparam name='TEntity'>
    /// The type of entity that will be returned by this method.
    /// </typeparam>
    public static TEntity Get<TEntity>(this ISession session, IIdentity<TEntity> identity)
      where TEntity : IEntity
    {
      TEntity output;

      if(session == null)
      {
        throw new ArgumentNullException("session");
      }

      if(identity != null)
      {
        output = session.Get<TEntity>(identity.Value);
      }
      else
      {
        output = default(TEntity);
      }

      return output;
    }

    /// <summary>
    /// Convenience method, similar to <c>ISession.Load</c> that specifically gets entity-based types.
    /// </summary>
    /// <returns>
    /// An entity instance, or a proxy instance that refers to that entity.
    /// </returns>
    /// <param name='session'>
    /// An NHibernate ISession instance.
    /// </param>
    /// <param name='identity'>
    /// An identity that refers to a specific entity type.
    /// </param>
    /// <typeparam name='TEntity'>
    /// The type of entity that will be returned by this method.
    /// </typeparam>
    public static TEntity Load<TEntity>(this ISession session, IIdentity<TEntity> identity)
      where TEntity : IEntity
    {
      TEntity output;

      if(session == null)
      {
        throw new ArgumentNullException("session");
      }

      if(identity != null)
      {
        output = session.Load<TEntity>(identity.Value);
      }
      else
      {
        output = default(TEntity);
      }

      return output;
    }
  }
}

