//
// IGetsEntityTypeForIdentityEquality.cs
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
namespace CSF.Entities
{
    /// <summary>
    /// A service which gets the entity type which should be used for determining identity-equality.
    /// </summary>
    public interface IGetsEntityTypeForIdentityEquality
    {
        /// <summary>
        /// Gets the type which should be used for identity equality for the given entity instance.
        /// </summary>
        /// <returns>The identity equality type.</returns>
        /// <param name="entity">An entity.</param>
        Type GetIdentityEqualityType(IEntity entity);

        /// <summary>
        /// Gets the type which should be used for identity equality for the given
        /// <see cref="Type"/>, which must implement <see cref="IEntity"/>.
        /// </summary>
        /// <returns>The identity equality type.</returns>
        /// <param name="entityType">The entity type.</param>
        Type GetIdentityEqualityType(Type entityType);
    }
}
