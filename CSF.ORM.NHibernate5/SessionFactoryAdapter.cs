//
// SessionFactoryAdapter.cs
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
using Nh = NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// Adapter object for a data-connection factory, using a native
    /// NHibernate version 5.x <see cref="Nh.ISession"/>.
    /// </summary>
    public class SessionFactoryAdapter : SessionFactoryAdapterBase
    {
        /// <summary>
        /// Gets a query object from an NHibernate ISession.
        /// </summary>
        /// <param name="s">The session</param>
        /// <returns>A query implementation</returns>
        protected override IQuery QueryFactory(Nh.ISession s)
            => new QueryAdapter(s);

        /// <summary>
        /// Gets a persister object from an NHibernate ISession.
        /// </summary>
        /// <param name="s">The session</param>
        /// <returns>A persister implementation</returns>
        protected override IPersister PersisterFactory(Nh.ISession s)
            => new PersisterAdapter(s);

        /// <summary>
        /// Gets a transaction object from an NHibernate ITransaction.
        /// </summary>
        /// <param name="nativeTran">The NHibernate transaction</param>
        /// <param name="commitAndDispose">Whether the transaction wrapper is permitted to commit &amp; dispose the inner transaction.</param>
        /// <returns>A transaction implementation</returns>
        protected override ITransaction TransactionFactory(Nh.ITransaction nativeTran, bool commitAndDispose)
            => new TransactionAdapter(nativeTran, commitAndDispose);

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactoryAdapter"/> class.
        /// </summary>
        /// <param name="sessionFactory">Session factory.</param>
        /// <param name="allowTransactionNesting">Specifies whether or not transaction-nesting is permitted or not.</param>
        /// <param name="reuseExistingSessionWhereAvailable">Specifies whether or not an existing session will be re-used if it exists.</param>
        public SessionFactoryAdapter(Nh.ISessionFactory sessionFactory,
                                     bool allowTransactionNesting = false,
                                     bool reuseExistingSessionWhereAvailable = false) : base(sessionFactory, allowTransactionNesting, reuseExistingSessionWhereAvailable) {}
    }
}
