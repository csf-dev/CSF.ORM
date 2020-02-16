//
// IdentityGeneratingEntityData.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2017 Craig Fowler
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
using System.Threading;
using System.Threading.Tasks;
using CSF.Entities;

namespace CSF.ORM
{
    /// <summary>
    /// Implementation of <see cref="EntityData"/> which generates identities for any entities which are added
    /// via the <see cref="EntityData.Add"/> method.
    /// </summary>
    public class IdentityGeneratingPersisterDecorator : IPersister
    {
        readonly IPersister wrapped;
        readonly IGeneratesIdentity identityGenerator;

        /// <summary>
        /// Adds the specified item to the data store, but makes an attempt to
        /// ensure that it has an identity first.  If <paramref name="identity"/> is
        /// <c>null</c> and the item is an implementation of <see cref="IEntity"/> then
        /// <see cref="IGeneratesIdentity.UpdateWithIdentity(CSF.Entities.IEntity)"/>
        /// is used first to generate an identity for the item.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        public object Add<T>(T item, object identity = null) where T : class
        {
            if (identity != null)
                return wrapped.Add<T>(item, identity);

            if(item is IEntity entity)
            {
                identityGenerator.UpdateWithIdentity(entity);
                return wrapped.Add<T>((T) entity, entity.IdentityValue);
            }

            return wrapped.Add<T>(item, identity);
        }


        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public void Update<T>(T item, object identity) where T : class
            => wrapped.Update<T>(item, identity);

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public void Delete<T>(T item, object identity) where T : class
            => wrapped.Delete<T>(item, identity);

        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        public async Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken)) where T : class
        {
            if (identity != null)
                return await wrapped.AddAsync<T>(item, identity, token);

            if (item is IEntity entity)
            {
                identityGenerator.UpdateWithIdentity(entity);
                return await wrapped.AddAsync<T>((T)entity, entity.IdentityValue, token);
            }

            return await wrapped.AddAsync<T>(item, identity, token);
        }

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => wrapped.UpdateAsync(item, identity, token);

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => wrapped.DeleteAsync(item, identity, token);

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityGeneratingPersisterDecorator"/> class.
        /// </summary>
        /// <param name="wrapped">A persister implementation to wrap.</param>
        /// <param name="identityGenerator">An identity generator.</param>
        public IdentityGeneratingPersisterDecorator(IPersister wrapped, IGeneratesIdentity identityGenerator = null)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
            this.identityGenerator = identityGenerator ?? new InMemoryIdentityGenerator();
        }
    }
}
