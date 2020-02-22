//
// SessionAdapter.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// An adapter for a data-connection and also a transaction-beginning
    /// service, using a native NHibernate <see cref="ISession"/>.
    /// </summary>
    public class SessionAdapter : IDataConnection, IGetsTransaction
    {
        readonly ISession session;
        readonly Func<ISession, IQuery> queryFactory;
        readonly Func<ISession, IPersister> persisterFactory;
        readonly Func<global::NHibernate.ITransaction, bool, ITransaction> transactionFactory;
        readonly bool allowTransactionNesting;

        /// <summary>
        /// Gets a persister object from the current connection.
        /// </summary>
        /// <returns>The persister.</returns>
        public IPersister GetPersister() => persisterFactory(session);

        /// <summary>
        /// Gets a query object from the current connection.
        /// </summary>
        /// <returns>The query.</returns>
        public IQuery GetQuery() => queryFactory(session);

        /// <summary>
        /// Attempts to get a new transaction, perhaps starting a new one.
        /// </summary>
        /// <returns>The transaction.</returns>
        public ITransaction GetTransaction()
        {
            var nativeTransaction = GetNativeTransaction();
            bool doNotCommitOrDispose = allowTransactionNesting && HasExistingTransaction;
            return transactionFactory(nativeTransaction, doNotCommitOrDispose);
        }

        global::NHibernate.ITransaction GetNativeTransaction()
        {
            IGetsNHibernateTransaction provider = new NativeTransactionFactory();
            if (allowTransactionNesting)
                provider = new PreferExistingNativeTransactionDecorator(provider);
            return provider.GetTransaction(session);
        }

        bool HasExistingTransaction => session.Transaction?.IsActive == true;

        #region IDisposable Support
        bool disposedValue;

        /// <summary>
        /// Releases all resources uses by the current instance.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> then disposal is explicit.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    session.Dispose();

                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="SessionAdapter"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
        /// <see cref="SessionAdapter"/>. The <see cref="Dispose()"/> method leaves the
        /// <see cref="SessionAdapter"/> in an unusable state. After calling <see cref="Dispose()"/>,
        /// you must release all references to the <see cref="SessionAdapter"/> so the garbage
        /// collector can reclaim the memory that the <see cref="SessionAdapter"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionAdapter"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        /// <param name="queryFactory">Query factory.</param>
        /// <param name="persisterFactory">Persister factory.</param>
        /// <param name="transactionFactory">Transaction factory.</param>
        /// <param name="allowTransactionNesting">If set to <c>true</c> allow transaction nesting.</param>
        public SessionAdapter(ISession session,
                              Func<ISession,IQuery> queryFactory,
                              Func<ISession,IPersister> persisterFactory,
                              Func<global::NHibernate.ITransaction, bool, ITransaction> transactionFactory,
                              bool allowTransactionNesting = false)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            this.queryFactory = queryFactory ?? throw new ArgumentNullException(nameof(queryFactory));
            this.persisterFactory = persisterFactory ?? throw new ArgumentNullException(nameof(persisterFactory));
            this.transactionFactory = transactionFactory ?? throw new ArgumentNullException(nameof(transactionFactory));
            this.allowTransactionNesting = allowTransactionNesting;
        }
    }
}
