//
// IUpCastsIdentity.cs
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
    /// A service which converts/casts an identity instance from a
    /// less-specific entity type to a more specific entity type.
    /// </summary>
    public interface ICastsIdentityType
    {
        /// <summary>
        /// Cast the identity instance to the desired entity type, or raise an exception if the cast would be invalid.
        /// </summary>
        /// <returns>The identity, cast to a new type.</returns>
        /// <exception cref="InvalidCastException">If the <paramref name="identity"/> is not suitable for the entity type <typeparamref name="TCast"/>.</exception>
        /// <param name="identity">The identity to up-cast.</param>
        /// <typeparam name="TCast">The desired entity type.</typeparam>
        IIdentity<TCast> CastIdentity<TCast>(IIdentity identity) where TCast : IEntity;
    }
}
