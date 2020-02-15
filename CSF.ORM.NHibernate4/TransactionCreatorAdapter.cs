using System;
using nh = NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// Implementation of <see cref="IBeginsTransaction"/> which creates transactions which wrap an NHibernate transaction.
    /// </summary>
    public class TransactionCreatorAdapter : IBeginsTransaction
    {
        readonly nh.ISession session;
        readonly bool allowNesting;

        /// <summary>
        /// Begins a transaction and returns it.
        /// </summary>
        /// <returns>The transaction.</returns>
        public ITransaction BeginTransaction()
        {
            var useNestableTransaction = (allowNesting && session.Transaction?.IsActive == true);
            return new TransactionAdapter(GetNhTransaction(useNestableTransaction), !useNestableTransaction);
        }

        nh.ITransaction GetNhTransaction(bool useNestedTransaction)
            => useNestedTransaction ? session.Transaction : session.BeginTransaction();

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCreatorAdapter"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        /// <param name="allowNesting">If <c>true</c> then this transaction creator allows the creation of nestable transactions.</param>
        public TransactionCreatorAdapter(nh.ISession session, bool allowNesting = false)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            this.allowNesting = allowNesting;
        }
    }
}
