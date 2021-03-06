﻿//
// NoOpTransactionCreator.cs
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

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// A no-operation dummy/fake transaction creator.
    /// </summary>
    public class NoOpTransactionCreator : IGetsTransaction
    {
        readonly bool throwOnRollback;

        /// <summary>
        /// Gets a value indicating whether a transaction is currently active, this implementation always returns <c>false</c>.
        /// </summary>
        /// <value>Always <c>false</c>.</value>
        public bool IsTransactionActive => false;

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        public ITransaction GetTransaction() => new NoOpTransaction(throwOnRollback);

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpTransactionCreator"/> class.
        /// </summary>
        /// <param name="throwOnRollback">If set to <c>true</c> throw on rollback.</param>
        public NoOpTransactionCreator(bool throwOnRollback = false)
        {
            this.throwOnRollback = throwOnRollback;
        }
    }
}
