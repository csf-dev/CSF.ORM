//
// EntityIdentityEqualityTypeProvider.cs
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
using System.Reflection;

namespace CSF.Entities
{
    /// <summary>
    /// A service which uses the inheritance hierarchy and optionally the <see cref="IgnoreForIdentityEqualityAttribute"/>
    /// to determine the entity type which should be used for identity-equality.
    /// </summary>
    public class EntityIdentityEqualityTypeProvider : IGetsEntityTypeForIdentityEquality
    {
        /// <summary>
        /// Gets the type which should be used for identity equality for the given entity instance.
        /// </summary>
        /// <returns>The identity equality type.</returns>
        /// <param name="entity">An entity.</param>
        public Type GetIdentityEqualityType(IEntity entity)
        {
            var entityType = entity?.GetType() ?? throw new ArgumentNullException(nameof(entity));
            return GetIdentityEqualityType(entityType);
        }

        /// <summary>
        /// Gets the type which should be used for identity equality for the given
        /// <see cref="T:System.Type"/>, which must implement <see cref="T:CSF.Entities.IEntity"/>.
        /// </summary>
        /// <returns>The identity equality type.</returns>
        /// <param name="entityType">The entity type.</param>
        public Type GetIdentityEqualityType(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));
            if (!typeof(IEntity).IsAssignableFrom(entityType))
                throw new ArgumentException(String.Format(Resources.ExceptionMessages.TypeMustBeEntityTypeFormat, entityType.FullName, typeof(IEntity).Name), nameof(entityType));

            var current = entityType;
            do
            {
                var baseType = current.BaseType;
                if (baseType == null) return current;

                var isBaseAnEntityType = typeof(IEntity).IsAssignableFrom(baseType);
                var isBaseEligibleAsAnEqualityType = baseType.GetCustomAttribute<IgnoreForIdentityEqualityAttribute>(false) == null;

                if (!isBaseAnEntityType || !isBaseEligibleAsAnEqualityType)
                    return current;

                current = current.BaseType;
            } while (true);

        }
    }
}
