//
// EntityData.cs
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
using System.Threading;
using System.Threading.Tasks;
using CSF.Entities;

namespace CSF.ORM
{
    /// <summary>
    /// Provides access to a data-source for entities, coordinating between query and persister implementations
    /// as appropriate.
    /// </summary>
    public class EntityData : IEntityData
    {
        readonly IQuery query;
        readonly IPersister persister;

        /// <summary>
        /// Add the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var identity = entity.GetIdentity();
            persister.Add(entity, identity.Value);
        }

        /// <summary>
        /// Get an entity using the specified identity.
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public TEntity Get<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return query.Get(identity);
        }

        /// <summary>
        /// Create a query for entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            return query.Query<TEntity>();
        }

        /// <summary>
        /// Remove the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            persister.Delete(entity, entity.GetIdentity()?.Value);
        }

        /// <summary>
        /// Remove the specified entity from the data-store using its identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public void Remove<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            var instance = query.Get(identity);
            if (instance == null) return;
            persister.Delete(instance, identity.Value);
        }

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
        public TEntity Theorise<TEntity>(IIdentity<TEntity> identity) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return query.Theorise(identity);
        }

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            persister.Update(entity, entity.GetIdentity()?.Value);
        }

        /// <summary>
        /// Add the specified entity to the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public Task AddAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var identity = entity.GetIdentity();
            return persister.AddAsync(entity, identity.Value, token);
        }

        /// <summary>
        /// Update the specified entity in the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public Task UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return persister.UpdateAsync(entity, entity.GetIdentity()?.Value, token);
        }

        /// <summary>
        /// Remove the specified entity from the data-store.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public Task RemoveAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return persister.DeleteAsync(entity, entity.GetIdentity()?.Value, token);
        }

        /// <summary>
        /// Remove the specified entity from the data-store using its identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public Task RemoveAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            var instance = query.Get(identity);
#if NET45
            if (instance == null) return Task.FromResult(0);
#else
            if (instance == null) return Task.CompletedTask;
#endif
            return persister.DeleteAsync(instance, identity.Value, token);
        }

        /// <summary>
        /// Get an entity using the specified identity.
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public Task<TEntity> GetAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return query.GetAsync(identity, token);
        }

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
        public Task<TEntity> TheoriseAsync<TEntity>(IIdentity<TEntity> identity, CancellationToken token = default(CancellationToken)) where TEntity : class, IEntity
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return query.TheoriseAsync(identity, token);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityData"/> class.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <param name="persister">Persister.</param>
        public EntityData(IQuery query, IPersister persister)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.persister = persister ?? throw new ArgumentNullException(nameof(persister));
        }
    }
}
