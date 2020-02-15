//
// InMemoryPersister.cs
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
using System.Collections.Generic;
using System.Linq;

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// In-memory implementation of <see cref="IPersister"/> which works upon an <see cref="InMemoryQuery"/>.
    /// </summary>
    public class InMemoryPersister : IPersister
    {
        readonly InMemoryDataStore store;

        /// <summary>
        /// Adds the specified item to the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public object Add<T>(T item, object identity = null) where T : class
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (identity == null)
                throw new ArgumentNullException(nameof(identity), $"For an {nameof(InMemoryPersister)}, the identity must be specified upfront and must not be null.");

            try
            {
                store.SyncRoot.EnterWriteLock();

                var dataItem = new InMemoryDataItem(item.GetType(), identity, item);
                store.Items.Add(dataItem);
                return identity;
            }
            finally
            {
                if (store.SyncRoot.IsWriteLockHeld)
                    store.SyncRoot.ExitWriteLock();
            }
        }

        /// <summary>
        /// Adds many items to the data store, in bulk.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <param name="identitySelector">A selector function which gets the identity from each item to add.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public void BulkAdd<T>(IEnumerable<T> items, Func<T,object> identitySelector) where T : class
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (identitySelector == null)
                throw new ArgumentNullException(nameof(identitySelector));

            try
            {
                store.SyncRoot.EnterWriteLock();

                foreach (var item in items)
                    Add(item, identitySelector);
            }
            finally
            {
                if (store.SyncRoot.IsWriteLockHeld)
                    store.SyncRoot.ExitWriteLock();
            }
        }

        void Add<T>(T item, Func<T, object> identitySelector) where T : class
        {
            if (ReferenceEquals(item, null)) return;

            object identity;

            try
            {
                identity = identitySelector(item);
            }
            catch(Exception e) { return; }

            Add(T, identity);
        }

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public void Delete<T>(T item, object identity) where T : class
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            try
            {
                store.SyncRoot.EnterWriteLock();

                var toDelete = store.Items.Where(x => ReferenceEquals(x.Value, item));
                foreach (var inMemoryItem in toDelete)
                    store.Items.Remove(inMemoryItem);
            }
            finally
            {
                if(store.SyncRoot.IsWriteLockHeld)
                    store.SyncRoot.ExitWriteLock();
            }
        }

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public void Update<T>(T item, object identity) where T : class
        {
            // Intentional no-op, because the data-store is in memory, objects are changed 'live'
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryPersister"/> class.
        /// </summary>
        /// <param name="store">The data store.</param>
        public InMemoryPersister(InMemoryDataStore store)
        {
            this.store = store ?? throw new ArgumentNullException(nameof(store));
        }
    }
}
