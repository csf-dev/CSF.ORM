//
// IdentityTypeAttribute.cs
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
    /// May decorate an entity class to indicate the <see cref="Type"/>
    /// of identity value which is used with the entity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only required for types which implement <see cref="IEntity"/> but do not
    /// derive from <see cref="Entity{T}"/> AND which do not have a public parameterless constructor.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class IdentityTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the identity type.
        /// </summary>
        /// <value>The type of the identity.</value>
        public Type IdentityType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityTypeAttribute"/> class.
        /// </summary>
        /// <param name="identityType">The identity type.</param>
        public IdentityTypeAttribute(Type identityType)
        {
            IdentityType = identityType ?? throw new ArgumentNullException(nameof(identityType));
        }
    }
}
