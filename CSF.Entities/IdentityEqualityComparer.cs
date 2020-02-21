//
// IdentityEqualityComparer.cs
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

namespace CSF.Entities
{
    /// <summary>
    /// An implementation of <see cref="IEqualityComparer{IIdentity}"/> which compares
    /// <see cref="IIdentity"/> instances for equality.  This is where the identity
    /// values are equal and the entity types for the identities are either equal or compatible,
    /// as determined by <see cref="EntityIdentityEqualityTypeProvider"/>.
    /// </summary>
    public class IdentityEqualityComparer : IEqualityComparer<IIdentity>
    {
        readonly IGetsEntityTypeForIdentityEquality identityEqualityTypeProvider;

        /// <summary>
        /// Determines whether the two identities are equal or not.
        /// </summary>
        /// <returns><c>true</c> if the identities are equal; <c>false</c> otherwise.</returns>
        /// <param name="x">The first identity.</param>
        /// <param name="y">The second identity.</param>
        public bool Equals(IIdentity x, IIdentity y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

            Type xEntityType, yEntityType;

            try
            {
                xEntityType = identityEqualityTypeProvider.GetIdentityEqualityType(x.EntityType);
                yEntityType = identityEqualityTypeProvider.GetIdentityEqualityType(y.EntityType);
            }
            catch(ArgumentException) { return false; }

            return Equals(xEntityType, yEntityType) && Equals(x.Value, y.Value);
        }

        /// <summary>
        /// Calculates a hash code for a given identity.
        /// </summary>
        /// <returns>The hash code.</returns>
        /// <param name="obj">The identity.</param>
        public int GetHashCode(IIdentity obj)
        {
            if (obj is null) return 0;

            Type entityType;
            try
            {
                entityType = identityEqualityTypeProvider.GetIdentityEqualityType(obj.EntityType);
            }
            catch(ArgumentException) { entityType = null; }

            var typeHash = entityType?.GetHashCode() ?? 0;
            var valueHash = obj.Value?.GetHashCode() ?? 0;

            return typeHash ^ valueHash;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityEqualityComparer"/> class.
        /// </summary>
        /// <param name="identityEqualityTypeProvider">An identity equality type provider implementation (use <c>null</c> here to select a default implementation).</param>
        public IdentityEqualityComparer(IGetsEntityTypeForIdentityEquality identityEqualityTypeProvider = null)
        {
            this.identityEqualityTypeProvider = identityEqualityTypeProvider ?? new EntityIdentityEqualityTypeProvider();
        }
    }
}
