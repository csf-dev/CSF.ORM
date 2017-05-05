using System;
using nh = NHibernate;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Wrapper for an NHibernate transaction.
  /// </summary>
  public class TransactionWrapper : ITransaction
  {
    readonly nh.ITransaction underlyingTransaction;

    protected nh.ITransaction UnderlyingTransaction => underlyingTransaction;

    protected bool DisposedValue
    {
      get { return disposedValue; }
      set { disposedValue = value; }
    }

    /// <summary>
    /// Commit this instance.
    /// </summary>
    public virtual void Commit()
    {
      UnderlyingTransaction.Commit();
    }

    /// <summary>
    /// Rollback this instance.
    /// </summary>
    public virtual void Rollback()
    {
      if(UnderlyingTransaction.WasRolledBack)
      {
        // An intentional no-op here if the underlying transaction was already rolled-back.
        return;
      }
      UnderlyingTransaction.Rollback();
    }

    #region IDisposable Support
    bool disposedValue;

    /// <summary>
    /// Dispose the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!DisposedValue)
      {
        if(disposing)
        {
          UnderlyingTransaction.Dispose();
        }
        DisposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/> object.
    /// </summary>
    /// <remarks>Call Dispose when you are finished using the
    /// <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/>. The Dispose method leaves the
    /// <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/> in an unusable state. After calling
    /// Dispose, you must release all references to the
    /// <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/> so the garbage collector can reclaim the
    /// memory that the <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.TransactionWrapper"/> class.
    /// </summary>
    /// <param name="underlyingTransaction">Underlying transaction.</param>
    public TransactionWrapper(nh.ITransaction underlyingTransaction)
    {
      if(underlyingTransaction == null)
        throw new ArgumentNullException(nameof(underlyingTransaction));

      this.underlyingTransaction = underlyingTransaction;
    }
  }
}
