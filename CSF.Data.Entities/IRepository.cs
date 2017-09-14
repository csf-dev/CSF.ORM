//
// IRepository.cs
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
  /// Non-generic repository service which exposes generic methods suitable for any entity type.
  /// </summary>
  [Obsolete("This is not a true repository, instead use IEntityData and its concrete implementatin EntityData.  See issue #10.")]
  public interface IRepository
  {
    /// <summary>
    /// Add the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    void Add<TEntity>(TEntity entity) where TEntity : class,IEntity;

    /// <summary>
    /// Update the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    void Update<TEntity>(TEntity entity) where TEntity : class,IEntity;

    /// <summary>
    /// Remove the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    void Remove<TEntity>(TEntity entity) where TEntity : class,IEntity;

    /// <summary>
    /// Get an entity using the specified identity.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    TEntity Get<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity;

    /// <summary>
    /// Create a theory object which assumes that an entity exists with the specified entity.
    /// This operation will never return <c>null</c> but will not neccesarily make use of the underlying data-store.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    TEntity Theorise<TEntity>(IIdentity<TEntity> identity) where TEntity : class,IEntity;

    /// <summary>
    /// Create a query for entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    IQueryable<TEntity> Query<TEntity>() where TEntity : class,IEntity;
  }
}
