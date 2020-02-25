//
// IEntityData.cs
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

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSF.Entities;

namespace CSF.ORM
{
    /// <summary>
    /// An object which provides access to an ORM-like data source for entities.
    /// </summary>
    public interface IEntityData
    {
        /// <summary>
        /// Add the specified entity to the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        IIdentity<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Update the specified entity in the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Remove the specified entity from the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Remove the specified entity from the data-store using its identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        void Remove<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity;

        /// <summary>
        /// Get an entity using the specified identity.
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        TEntity Get<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity;

        /// <summary>
        /// <para>
        /// Gets an instance of an object which is <typeparamref name="TEntity"/>.  This might be an object
        /// from the data-store, or it might be a stub/proxy or other form of stand-in object.
        /// </para>
        /// <para>
        /// This function should be used when the 'real' entity is not required but where a stand-in will suffice,
        /// and where all that is required of the stand-in is for it to have to correct identity.
        /// </para>
        /// <para>
        /// This function will never return <c>null</c>, but will also not make unneccesary use of the
        /// underlying data-store.
        /// </para>
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        TEntity Theorise<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity;

        /// <summary>
        /// Add the specified entity to the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task<IIdentity<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// Update the specified entity in the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// Remove the specified entity from the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task RemoveAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// Remove the specified entity from the data-store using its identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task RemoveAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// Get an entity using the specified identity.
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task<TEntity> GetAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// <para>
        /// Gets an instance of an object which is <typeparamref name="TEntity"/>.  This might be an object
        /// from the data-store, or it might be a stub/proxy or other form of stand-in object.
        /// </para>
        /// <para>
        /// This function should be used when the 'real' entity is not required but where a stand-in will suffice,
        /// and where all that is required of the stand-in is for it to have to correct identity.
        /// </para>
        /// <para>
        /// This function will never return <c>null</c>, but will also not make unneccesary use of the
        /// underlying data-store.
        /// </para>
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        Task<TEntity> TheoriseAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity;

        /// <summary>
        /// Create a query for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
    }
}
