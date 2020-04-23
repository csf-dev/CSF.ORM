//
// Identity.cs
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
using System.Collections.Generic;
using System.Linq;

namespace CSF.Entities
{
    /// <summary>
    /// Static functionality supporting the <see cref="IIdentity"/> type and its implementations.
    /// </summary>
    public static class Identity
    {
        static readonly IGetsIdentityType identityTypeProvider = new IdentityTypeProvider();
        static readonly ICreatesIdentity identityFactory = new IdentityFactory();
        static readonly ICastsIdentityType caster = new IdentityTypeCaster();
        static readonly IParsesIdentity parser = new IdentityParser();

        /// <summary>
        /// Create an entity-typed identity instance from a specified identity value.
        /// </summary>
        /// <returns>An identity.</returns>
        /// <param name="identityValue">Identity value.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public static IIdentity<TEntity> Create<TEntity>(object identityValue) where TEntity : IEntity
        {
            var entityType = typeof(TEntity);
            var identityType = identityTypeProvider.GetIdentityType(entityType);
            return (IIdentity<TEntity>)identityFactory.Create(entityType, identityType, identityValue);
        }

        /// <summary>
        /// Creates an entity-typed identity instance by parsing a specified identity value.
        /// </summary>
        /// <returns>An identity, or a <c>null</c> reference if the value could not be parsed.</returns>
        /// <param name="value">The identity value to parse.</param>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        public static IIdentity<TEntity> Parse<TEntity>(object value) where TEntity : IEntity
            => (IIdentity<TEntity>) parser.Parse(typeof(TEntity), value);

        /// <summary>
        /// Cast the identity instance to the desired entity type, or raise an exception if the cast would be invalid.
        /// </summary>
        /// <returns>The identity, converted to a new entity type.</returns>
        /// <param name="identity">The identity to convert to a different entity type.</param>
        /// <typeparam name="TCast">The desired entity type.</typeparam>
        /// <exception cref="InvalidCastException">If the <paramref name="identity"/> is not suitable for the entity type <typeparamref name="TCast"/>.</exception>
        public static IIdentity<TCast> Cast<TCast>(this IIdentity identity) where TCast : IEntity
            => caster.CastIdentity<TCast>(identity);
    }
}

