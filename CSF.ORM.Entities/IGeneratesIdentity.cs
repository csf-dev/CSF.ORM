//
// IIdentityGenerator.cs
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
using CSF.Entities;

namespace CSF.ORM.Entities
{
    /// <summary>
    /// Generator service which creates new identifiers for entities.
    /// </summary>
    public interface IGeneratesIdentity
    {
        /// <summary>
        /// Gets a new identity value of the specified type.
        /// </summary>
        /// <returns>The generated identity.</returns>
        /// <typeparam name="T">The identity type.</typeparam>
        T GetIdentity<T>();

        /// <summary>
        /// Gets a new identity value of the specified type.
        /// </summary>
        /// <returns>The generated identity.</returns>
        /// <param name="identityType">The identity type.</param>
        object GetIdentity(Type identityType);

        /// <summary>
        /// Updates the given entity object, ensuring that it has an identity value.
        /// </summary>
        /// <param name="entity">An entity for which to generate an identity.</param>
        void UpdateWithIdentity(IEntity entity);
    }
}
