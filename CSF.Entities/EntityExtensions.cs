//
// EntityExtensions.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2015 CSF Software Limited
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
using System.Reflection;

namespace CSF.Entities
{
    /// <summary>
    /// Extension methods for entity types.
    /// </summary>
    public static class EntityExtensions
    {
        static readonly ICreatesIdentity identityFactory = new IdentityFactory();

        /// <summary>
        /// Gets the identity for the specified entity.
        /// </summary>
        /// <returns>The entity identity, possibly a <c>null</c> reference if the entity does not have an identity value.</returns>
        /// <param name="entity">The entity.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public static IIdentity<TEntity> GetIdentity<TEntity>(this TEntity entity) where TEntity : IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return (IIdentity<TEntity>)identityFactory.Create(typeof(TEntity), entity.IdentityType, entity.IdentityValue);
        }
    }
}

