//
// NoOpTransaction.cs
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

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// A no-operation (dummy/fake) transaction implementation.
    /// </summary>
    public sealed class NoOpTransaction : ITransaction
    {
        bool final;
        readonly bool throwOnRollback;

        /// <summary>
        /// Gets a value indicating whether this <see cref="NoOpTransaction"/> is committed.
        /// </summary>
        /// <value><c>true</c> if committed; otherwise, <c>false</c>.</value>
        public bool Committed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="NoOpTransaction"/> is rolled back.
        /// </summary>
        /// <value><c>true</c> if rolled back; otherwise, <c>false</c>.</value>
        public bool RolledBack { get; private set; }

        /// <summary>
        /// Commit this instance.
        /// </summary>
        public void Commit()
        {
            if (final)
                throw new InvalidOperationException();

            Committed = true;
            final = true;
        }

        /// <summary>
        /// Rollback this instance.
        /// </summary>
        public void Rollback()
        {
            if (final)
                throw new InvalidOperationException();

            RolledBack = true;
            final = true;

            if (throwOnRollback)
                throw new TransactionRollbackException($"A {nameof(NoOpTransaction)} is being rolled back and 'throw on rollback' is true.");
        }

        /// <summary>
        /// Releases all resource used by the <see cref="NoOpTransaction"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="NoOpTransaction"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="NoOpTransaction"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="NoOpTransaction"/>
        /// so the garbage collector can reclaim the memory that the <see cref="NoOpTransaction"/> was occupying.</remarks>
        public void Dispose()
        {
            if (final)
                return;

            Rollback();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpTransaction"/> class.
        /// </summary>
        /// <param name="throwOnRollback">If set to <c>true</c> throw on rollback.</param>
        public NoOpTransaction(bool throwOnRollback = false)
        {
            this.throwOnRollback = throwOnRollback;
        }
    }
}
