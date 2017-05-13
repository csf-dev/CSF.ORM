using System;
using NHibernate;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Implementation of <see cref="ITransactionCreator"/> which creates transactions which wrap an NHibernate transaction.
  /// </summary>
  public class TransactionCreator : ITransactionCreator
  {
    readonly ISession session;

    /// <summary>
    /// Begins a transaction and returns it.
    /// </summary>
    /// <returns>The transaction.</returns>
    public ITransaction BeginTransaction()
    {
      if(session.Transaction != null && session.Transaction.IsActive)
      {
        return new SubordinateTransactionWrapper(session.Transaction);
      }

      return new TransactionWrapper(session.BeginTransaction());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.TransactionCreator"/> class.
    /// </summary>
    /// <param name="session">Session.</param>
    public TransactionCreator(ISession session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));

      this.session = session;
    }
  }
}
