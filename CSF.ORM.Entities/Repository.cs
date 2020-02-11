﻿//
// Repository.cs
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
using System.Linq;
using CSF.Entities;

namespace CSF.ORM.Entities
{
  /// <summary>
  /// Non-generic repository type, suitable for any entity type (via generic methods).
  /// </summary>
  [Obsolete("This is not a true repository, instead use IEntityData and its concrete implementatin EntityData.  See issue #10.")]
  public class Repository : IRepository
  {
    readonly IQuery query;
    readonly IPersister persister;

    /// <summary>
    /// Add the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public void Add<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      persister.Add(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Get an entity using the specified identity.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public TEntity Get<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));

      return query.Get(identity);
    }

    /// <summary>
    /// Create a query for entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public IQueryable<TEntity> Query<TEntity>() where TEntity : class,IEntity
    {
      return query.Query<TEntity>();
    }

    /// <summary>
    /// Remove the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public void Remove<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      persister.Delete(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Create a theory object which assumes that an entity exists with the specified entity.
    /// This operation will never return <c>null</c> but will not neccesarily make use of the underlying data-store.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public TEntity Theorise<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));

      return query.Theorise(identity);
    }

    /// <summary>
    /// Update the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public void Update<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      persister.Update(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.Entities.Repository"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="persister">Persister.</param>
    public Repository(IQuery query,
                      IPersister persister)
    {
      if(persister == null)
        throw new ArgumentNullException(nameof(persister));
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      this.query = query;
      this.persister = persister;
    }
  }
}
