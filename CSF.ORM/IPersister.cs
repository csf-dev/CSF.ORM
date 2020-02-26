//
// IPersister.cs
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

using System.Threading;
using System.Threading.Tasks;

namespace CSF.ORM
{
    /// <summary>
    /// A service which makes changes to a back-end data-store.
    /// </summary>
    public interface IPersister
    {
        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        object Add<T>(T item, object identity = null) where T : class;

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        void Update<T>(T item, object identity) where T : class;

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        void Delete<T>(T item, object identity) where T : class;

        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken)) where T : class;

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class;

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class;
    }
}
