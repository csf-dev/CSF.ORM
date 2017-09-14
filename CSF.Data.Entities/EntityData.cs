//
// EntityData.cs
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

namespace CSF.Data.Entities
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
    /// Gets the query service.
    /// </summary>
    /// <value>The query service.</value>
    protected IQuery QueryService => query;

    /// <summary>
    /// Gets the persister.
    /// </summary>
    /// <value>The persister.</value>
    protected IPersister Persister => persister;

    /// <summary>
    /// Add the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual void Add<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      Persister.Add(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Get an entity using the specified identity.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual TEntity Get<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));

      return QueryService.Get(identity);
    }

    /// <summary>
    /// Create a query for entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual IQueryable<TEntity> Query<TEntity>() where TEntity : class,IEntity
    {
      return QueryService.Query<TEntity>();
    }

    /// <summary>
    /// Remove the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual void Remove<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      Persister.Delete(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Create a theory object which assumes that an entity exists with the specified identity.
    /// This operation will never return <c>null</c> but will not neccesarily make use of the underlying data-store.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual TEntity Theorise<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));

      return QueryService.Theorise(identity);
    }

    /// <summary>
    /// Update the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public virtual void Update<TEntity>(TEntity entity) where TEntity : class,IEntity
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      Persister.Update(entity, entity.GetIdentity()?.Value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityData"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="persister">Persister.</param>
    public EntityData(IQuery query,
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
