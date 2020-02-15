//
// InMemoryDataStore.cs
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
using System.Collections.Generic;
using System.Threading;

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// A storage back-end for an in memory database.
    /// </summary>
    public class InMemoryDataStore
    {
        /// <summary>
        /// Gets a synchronisation object under which the current instance may be locked.
        /// </summary>
        public readonly ReaderWriterLockSlim SyncRoot;

        readonly ISet<InMemoryDataItem> items;

        /// <summary>
        /// Gets the collection of items in the data store.
        /// </summary>
        /// <value>The data items.</value>
        public ICollection<InMemoryDataItem> Items => items;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDataStore"/> class.
        /// </summary>
        public InMemoryDataStore()
        {
            items = new HashSet<InMemoryDataItem>();
            SyncRoot = new ReaderWriterLockSlim();
        }
    }
}
