using System;
using System.Threading.Tasks;
using nh = NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// Wrapper/adapter for an NHibernate transaction.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The constructor parameter <c>mayCommitAndDispose</c> indicates whether or not the current
    /// instance 'owns' the underlying transaction or not.  If it does not 'own' it, then this instance
    /// is not permitted to commit or dispose the underlying transaction, because doing so would
    /// prevent an 'outer' transaction (which does own the underlying transaction) from rolling back.
    /// </para>
    /// <para>
    /// Rolling this transaction back, though, will have the effect of rolling back the whole underlying
    /// transaction, forcing everything to be rolled back.
    /// </para>
    /// <para>
    /// This is a very naïve implementation of transaction-nesting, but it will handle the most common
    /// scenarios faily gracefully.
    /// </para>
    /// </remarks>
    public class TransactionAdapter : ITransaction, IHasNativeImplementation
    {
        readonly nh.ITransaction transaction;
        readonly bool mayCommitAndDispose;
        bool disposedValue;

        /// <summary>
        /// Gets the native implementation which provides the service's functionality.
        /// </summary>
        /// <value>The native implementation.</value>
        public object NativeImplementation => transaction;

        /// <summary>
        /// Gets a value which indicates whether this transaction has been finalised or not.
        /// Final states include "committed", "rolled-back" and "disposed".
        /// </summary>
        /// <value><c>true</c> if this instance is final; otherwise, <c>false</c>.</value>
        public bool IsFinal { get; private set; }

        /// <summary>
        /// Commit this instance.
        /// </summary>
        public void Commit()
        {
            if (IsFinal || disposedValue)
                throw new InvalidTransactionOperationException(Resources.ExceptionMessages.CannotCommitAlreadyFinalised);

            if(mayCommitAndDispose)
                transaction.Commit();

            IsFinal = true;
        }

        /// <summary>
        /// Commit this transaction to the back-end using an asynchronous API, where available.
        /// </summary>
        public Task CommitAsync() => Task.Run(() => Commit());

        /// <summary>
        /// Rollback this instance.
        /// </summary>
        public void Rollback()
        {
            if (transaction.WasRolledBack || IsFinal)
            {
                IsFinal = true;
                return;
            }
            transaction.Rollback();
            IsFinal = true;
        }

        /// <summary>
        /// Roll the transaction back and abort changes using an asynchronous API, where available.
        /// </summary>
        public Task RollbackAsync() => Task.Run(() => Rollback());

        #region IDisposable Support
        /// <summary>
        /// Dispose the current instance.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> then this call represents an explicit disposal.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && mayCommitAndDispose)
                {
                    transaction.Dispose();
                }
                disposedValue = true;
                IsFinal = true;
            }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="TransactionAdapter"/> object.
        /// </summary>
        /// <remarks>Call Dispose when you are finished using the
        /// <see cref="TransactionAdapter"/>. The Dispose method leaves the
        /// <see cref="TransactionAdapter"/> in an unusable state. After calling
        /// Dispose, you must release all references to the
        /// <see cref="TransactionAdapter"/> so the garbage collector can reclaim the
        /// memory that the <see cref="TransactionAdapter"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The underlying transaction.</param>
        /// <param name="preventCommitAndDispose">Indicates whether or not this instance is permitted to commit and dispose the transaction or not.</param>
        public TransactionAdapter(nh.ITransaction transaction, bool preventCommitAndDispose = true)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            mayCommitAndDispose = !preventCommitAndDispose;
        }
    }
}
