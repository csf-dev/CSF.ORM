using System;
using nh = NHibernate;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// A subordinate transaction type, which does not take ownership of the underlying NHibernate transaction instance.
  /// This allows a VERY limited nesting of transactions.
  /// </summary>
  /// <remarks>
  /// <para>
  /// If this transaction is committed then nothing happens to the underlying transaction - it is just allowed to continue
  /// working.
  /// </para>
  /// <para>
  /// If this transaction is rolled-back then (unless the underlying transaction has already been rolled back)
  /// that underlying instance is rolled back (ensuring that the whole operation is rolled back).
  /// </para>
  /// <para>
  /// If any operation is attempted and the underlying transaction is in an invalid state then an exception is raised.
  /// </para>
  /// <para>
  /// Overall, this type does not take ownership for manipulating (or disposing) the underlying transaction, apart from
  /// dealing with rollbacks.  All other work is left to the 'parent' transaction.
  /// </para>
  /// </remarks>
  public class SubordinateTransactionWrapper : TransactionWrapper
  {
    /// <summary>
    /// Commit this instance.
    /// </summary>
    public override void Commit()
    {
      if(UnderlyingTransaction.WasCommitted || UnderlyingTransaction.WasRolledBack)
      {
        throw new InvalidTransactionOperationException(Resources.ExceptionMessages.CannotCommitAlreadyFinalised);
      }

      // An intentional no-op here if the underlying transaction is not in a final state;
      // we don't own the underlying transaction so we don't want to change its state.
    }

    #region IDisposable Support
    /// <summary>
    /// Dispose the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected override void Dispose(bool disposing)
    {
      if(!DisposedValue)
      {
        // Intentional no-op, this type does not take ownership of disposing the underlying transaction.
        DisposedValue = true;
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.SubordinateTransactionWrapper"/> class.
    /// </summary>
    /// <param name="underlyingTransaction">Underlying transaction.</param>
    public SubordinateTransactionWrapper(nh.ITransaction underlyingTransaction) : base(underlyingTransaction) {}
  }
}
