//
// InMemoryQuery.cs
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
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// An implementation of <see cref="IQuery"/> which represents a transient in-memory data-set.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is intended for the purpose of mocking data-sets and queries (IE: it is a test fake).
    /// </para>
    /// </remarks>
    public class DataQuery : IQuery
    {
        readonly DataStore store;

        /// <summary>
        /// Creates an instance of the given object-type, based upon a theory that it exists in the underlying data-source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will always return a non-null object instance, even if the underlying object does not exist in the
        /// data source.  If a 'thoery object' is created for an object which does not actually exist, then an exception
        /// could be thrown if that theory object is used.
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public TQueried Theorise<TQueried>(object identityValue) where TQueried : class
        {
            if (identityValue == null)
                throw new ArgumentNullException(nameof(identityValue));

            try
            {
                store.SyncRoot.EnterReadLock();

                var actualObject = Get<TQueried>(identityValue);
                if (actualObject != null) return actualObject;

                try
                {
                    return Activator.CreateInstance<TQueried>();
                }
                catch(Exception e)
                {
                    throw new CannotTheoriseException($"Cannot create a theory object of type {typeof(TQueried).FullName}", e);
                }
            }
            finally
            {
                if (store.SyncRoot.IsReadLockHeld)
                    store.SyncRoot.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets a single instance from the underlying data source, identified by an identity value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will either get an object instance, or it will return <c>null</c> (if no instance is found).
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public TQueried Get<TQueried>(object identityValue) where TQueried : class
        {
            if (identityValue == null)
                throw new ArgumentNullException(nameof(identityValue));

            try
            {
                store.SyncRoot.EnterReadLock();

                return GetItemsOfType<TQueried>()
                  .Where(x => identityValue.Equals(x.Identity))
                  .Select(x => x.Value)
                  .Cast<TQueried>()
                  .FirstOrDefault();
            }
            finally
            {
                if (store.SyncRoot.IsReadLockHeld)
                    store.SyncRoot.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets a new queryable data-source.
        /// </summary>
        /// <typeparam name="TQueried">The type of queried-for object.</typeparam>
        public IQueryable<TQueried> Query<TQueried>() where TQueried : class
        {
            try
            {
                store.SyncRoot.EnterReadLock();

                return GetItemsOfType<TQueried>()
                  .Select(x => x.Value)
                  .Cast<TQueried>()
                  .AsQueryable();
            }
            finally
            {
                if (store.SyncRoot.IsReadLockHeld)
                    store.SyncRoot.ExitReadLock();
            }
        }

        IQueryable<DataItem> GetItemsOfType<TQueried>() where TQueried : class
        {
            return store.Items
                .Where(x => typeof(TQueried).IsAssignableFrom(x.ValueType))
                .AsQueryable();
        }

        /// <summary>
        /// Creates an instance of the given object-type, based upon a theory that it exists in the underlying data-source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method should always return a non-null object instance, even if the underlying object does not exist in the
        /// data source.  If a 'thoery object' is created for an object which does not actually exist, then an exception
        /// could be thrown if that theory object is used.
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public Task<TQueried> TheoriseAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken)) where TQueried : class
            => Task.Run(() => Theorise<TQueried>(identityValue), token);

        /// <summary>
        /// Gets a single instance from the underlying data source, identified by an identity value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will either get an object instance, or it will return <c>null</c> (if no instance is found).
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public Task<TQueried> GetAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken)) where TQueried : class
            => Task.Run(() => Get<TQueried>(identityValue), token);

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemory.DataQuery"/> class.
        /// </summary>
        /// <param name="store">The data-store which will be used by this query.</param>
        public DataQuery(DataStore store)
        {
            this.store = store ?? throw new ArgumentNullException(nameof(store));
        }
    }
}
