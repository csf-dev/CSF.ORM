//
// DataConnection.cs
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
    /// An in-memory data connection.
    /// </summary>
    public sealed class DataConnection : IDataConnection
    {
        readonly DataStore store;

        /// <summary>
        /// Releases all resource used by the <see cref="DataConnection"/> object.
        /// </summary>
        public void Dispose() { /* Intentional no-op */ }

        /// <summary>
        /// Where an underlying ORM system uses an identity-map or cache of retrieved
        /// objects, this method removes the given object from that cache.  This means that if
        /// the object is retrieved again, it will be re-loaded from the underlying data-store.
        /// </summary>
        /// <param name="objectToEvict">The object to evict from the cache (if applicable).</param>
        public void EvictFromCache(object objectToEvict) { /* Intentional no-op */ }

        /// <summary>
        /// Gets a persister object from the current connection.
        /// </summary>
        /// <returns>The persister.</returns>
        public IPersister GetPersister() => new DataPersister(store);

        /// <summary>
        /// Gets a query object from the current connection.
        /// </summary>
        /// <returns>The query.</returns>
        public IQuery GetQuery() => new DataQuery(store);

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnection"/> class.
        /// </summary>
        /// <param name="store">If this is not-null then it will be used as the data-storage for queries/persisters created from this connection.</param>
        public DataConnection(DataStore store = null)
        {
            this.store = store ?? new DataStore();
        }
    }
}
