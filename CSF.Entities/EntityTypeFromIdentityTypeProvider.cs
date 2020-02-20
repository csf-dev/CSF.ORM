//
// EntityTypeFromIdentityTypeProvider.cs
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

namespace CSF.Entities
{
    /// <summary>
    /// A service which gets the entity type (implementing <see cref="IEntity"/>) from an
    /// identity type, which must derive from <see cref="IIdentity{T}"/>.
    /// </summary>
    public class EntityTypeFromIdentityTypeProvider : IGetsEntityTypeFromGenericIdentityType
    {
        /// <summary>
        /// Gets the entity type from the identity type.
        /// </summary>
        /// <returns>The entity type, or a <c>null</c> reference if the specified type does not derive from <see cref="IIdentity{T}"/>.</returns>
        /// <param name="identityType">An identity type.</param>
        public Type GetEntityType(Type identityType)
        {
            if (identityType == null) return null;

            var implementedInterfaces = identityType.GetInterfaces().Union(new[] { identityType });

            var genericIdentityInterface = (from iface in implementedInterfaces
                                            where
                                              iface.IsInterface
                                              && iface.IsGenericType
                                              && iface.GetGenericTypeDefinition() == typeof(IIdentity<>)
                                            select iface)
              .FirstOrDefault();

            if (genericIdentityInterface == null) return null;

            return genericIdentityInterface.GenericTypeArguments[0];
        }
    }
}
