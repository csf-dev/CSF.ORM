//
// EntityIdentityEqualityComparer.cs
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

using System.Collections.Generic;

namespace CSF.Entities
{
    /// <summary>
    /// Compares <see cref="IEntity"/> instances for identity-equality.
    /// Entities are identity-equal if they both have identities and their identities are equal.
    /// </summary>
    public class EntityIdentityEqualityComparer : IEqualityComparer<IEntity>
    {
        readonly IEqualityComparer<IIdentity> identityEqualityComparer = new IdentityEqualityComparer();

        /// <summary>
        /// Determines whether the two entities are identity-equal or not.
        /// </summary>
        /// <returns><c>true</c> if the entities are identity-equal; <c>false</c> otherwise.</returns>
        /// <param name="x">The first entity.</param>
        /// <param name="y">The second entity.</param>
        public bool Equals(IEntity x, IEntity y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

            var identityX = x.GetIdentity();
            var identityY = y.GetIdentity();

            if (identityX is null || identityY is null) return false;

            return identityEqualityComparer.Equals(identityX, identityY);
        }

        /// <summary>
        /// Gets a hash code for the specified entity, based upon its identity.
        /// BEWARE that because entity identities are mutable, usage of this function
        /// for a hashtable such as a dictionary or set is NOT ADVISED.
        /// </summary>
        /// <returns>The hash code.</returns>
        /// <param name="obj">An entity.</param>
        public int GetHashCode(IEntity obj)
        {
            if (obj is null) return 0;
            var identity = obj.GetIdentity();
            return identity?.GetHashCode() ?? 0;
        }
    }
}
