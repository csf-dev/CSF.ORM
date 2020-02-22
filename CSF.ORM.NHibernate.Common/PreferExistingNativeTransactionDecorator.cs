//
// UseExistingNativeTransactionDecorator.cs
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
    /// A decorator for a transaction-creator service which prefers the use of
    /// an existing active transaction, rather than creating a new one.
    /// </summary>
    public class PreferExistingNativeTransactionDecorator : IGetsNHibernateTransaction
    {
        readonly IGetsNHibernateTransaction wrapped;

        /// <summary>
        /// Gets a transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        /// <param name="session">Session.</param>
        public Nh.ITransaction GetTransaction(Nh.ISession session)
        {
            if (session.Transaction?.IsActive == true)
                return session.Transaction;

            return wrapped.GetTransaction(session);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PreferExistingNativeTransactionDecorator"/> class.
        /// </summary>
        /// <param name="wrapped">Wrapped.</param>
        public PreferExistingNativeTransactionDecorator(IGetsNHibernateTransaction wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}
