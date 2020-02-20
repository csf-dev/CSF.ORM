//
// IEntity.cs
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

namespace CSF.Entities
{
    /// <summary>
    /// Represents an entity; an object which has a unique identity within the domain of an application's business
    /// logic, as well as other properties which store data.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets a value which indicates whether or not the current instance has an identity value set.
        /// </summary>
        bool HasIdentity { get; }

        /// <summary>
        /// Gets or sets the identity value for the current instance.
        /// </summary>
        /// <value>The identity value.</value>
        object IdentityValue { get; set; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the identity value which would be used with <see cref="IdentityValue"/>.
        /// </summary>
        /// <returns>The identity type.</returns>
        Type IdentityType { get; }
    }
}

