//
// IdentityTypeProvider.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSF.Entities
{
    /// <summary>
    /// A service which gets the identity value type for a specified entity type.
    /// </summary>
    public class IdentityTypeProvider : IGetsIdentityType
    {
        /// <summary>
        /// Gets the identity-value type.
        /// </summary>
        /// <returns>The identity type.</returns>
        /// <param name="entityType">An entity type.</param>
        public Type GetIdentityType(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            Type output;

            if (TryGetIdentityTypeFromGenericEntityType(entityType, out output))
                return output;

            if (TryGetIdentityTypeFromNewInstance(entityType, out output))
                return output;

            if (TryGetIdentityTypeFromAttribute(entityType, out output))
                return output;

            return null;
        }

        static bool TryGetIdentityTypeFromGenericEntityType(Type entityType, out Type identityType)
        {

            var genericEntityType = (from type in GetAllImplementedTypes(entityType)
                                     where type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Entity<>)
                                     select type)
                .FirstOrDefault();

            if (genericEntityType == null)
            {
                identityType = null;
                return false;
            }

            identityType = genericEntityType.GenericTypeArguments[0];
            return true;
        }

        static bool TryGetIdentityTypeFromNewInstance(Type entityType, out Type identityType)
        {
            var parameterlessCtor = entityType.GetConstructor(Type.EmptyTypes);

            if(parameterlessCtor == null)
            {
              identityType = null;
              return false;
            }

            var instance = (IEntity) Activator.CreateInstance(entityType);
            identityType = instance.IdentityType;
            return true;
        }

        static bool TryGetIdentityTypeFromAttribute(Type entityType, out Type identityType)
        {
            var attrib = entityType.GetCustomAttribute<IdentityTypeAttribute>();

            if (attrib == null)
            {
                identityType = null;
                return false;
            }

            identityType = attrib.IdentityType;
            return true;
        }

        static IEnumerable<Type> GetAllImplementedTypes(Type type)
        {
            yield return type;

            var current = type;
            while(current.BaseType != null)
            {
                current = current.BaseType;
                yield return current;
            }
        }
    }
}
