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
    /// NHibernate version 4.x <see cref="Nh.ISession"/>.
    /// </summary>
    public class SessionFactoryAdapter : ICreatesDataConnection
    {
        readonly Nh.ISessionFactory sessionFactory;
        readonly bool allowTransactionNesting;

        /// <summary>
        /// Create a new data connection and return it.
        /// </summary>
        /// <returns>The connection.</returns>
        public IDataConnection CreateConnection()
        {
            Func<Nh.ISession, IQuery> queryFactory = s => new QueryAdapter(s);
            Func<Nh.ISession, IPersister> persisterFactory = s => new PersisterAdapter(s);
            Func<Nh.ITransaction, bool, ITransaction> transactionFactory = (nativeTran, commitAndDispose) => new TransactionAdapter(nativeTran, commitAndDispose);

            return new SessionAdapter(sessionFactory.OpenSession(), queryFactory, persisterFactory, transactionFactory, allowTransactionNesting);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactoryAdapter"/> class.
        /// </summary>
        /// <param name="sessionFactory">Session factory.</param>
        /// <param name="allowTransactionNesting">Specified whether or not transaction-nesting is permitted or not.</param>
        public SessionFactoryAdapter(Nh.ISessionFactory sessionFactory, bool allowTransactionNesting = false)
        {
            this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
            this.allowTransactionNesting = allowTransactionNesting;
        }
    }
}
