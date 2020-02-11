//
// InMemoryQueryExtensions.cs
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
using System.Collections.Generic;
using CSF.Entities;

namespace CSF.ORM.Entities
{
  /// <summary>
  /// Extension methods for <see cref="InMemoryQuery"/>.
  /// </summary>
  public static class InMemoryQueryExtensions
  {
    #region extension methods

    /// <summary>
    /// Adds a collection of entities to the given query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="entities">The entities.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static void Add<TEntity>(this InMemoryQuery query,
                                    IEnumerable<TEntity> entities)
      where TEntity : class,IEntity
    {
      if (query == null) {
        throw new ArgumentNullException (nameof (query));
      }

      query.Add(entities, item => item.IdentityValue);
    }

    /// <summary>
    /// Adds an entity to the given query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="entity">The entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static void Add<TEntity>(this InMemoryQuery query,
                                    TEntity entity)
      where TEntity : class,IEntity
    {
      if (query == null) {
        throw new ArgumentNullException (nameof (query));
      }

      query.Add(entity, entity.IdentityValue);
    }

    /// <summary>
    /// Deletes an entity from the given query using its identity.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="identity">Identity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static void DeleteEntity<TEntity>(this InMemoryQuery query,
                                             IIdentity<TEntity> identity)
      where TEntity : class,IEntity
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      query.Delete<TEntity>(identity.Value);
    }

    #endregion
  }
}
