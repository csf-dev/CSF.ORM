//
// InMemoryDataItem.cs
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

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// Represents an item in an <see cref="InMemoryDataStore"/>
    /// </summary>
    public sealed class InMemoryDataItem : IEquatable<InMemoryDataItem>
    {
        /// <summary>
        /// Gets the <see cref="ValueType"/> for which the item is stored.  Typically this is equal to <see cref="Object.GetType()"/> for the <see cref="Value"/>.
        /// </summary>
        /// <value>The value type.</value>
        public Type ValueType { get; }

        /// <summary>
        /// Gets the identity value for the <see cref="Value"/>.  This is equivalent to the concept of a primary key.
        /// </summary>
        /// <value>The identity.</value>
        public object Identity { get; }

        /// <summary>
        /// Gets the stored data object.
        /// </summary>
        /// <value>The data value.</value>
        public object Value { get; }

        /// <summary>
        /// Serves as a hash function for a <see cref="InMemoryDataItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
        public override int GetHashCode() => ValueType.GetHashCode() ^ Identity.GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="InMemoryDataItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="InMemoryDataItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="InMemoryDataItem"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (obj.GetType() != typeof(InMemoryDataItem)) return false;
            return Equals((InMemoryDataItem) obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="InMemoryDataItem"/> is equal to the current <see cref="InMemoryDataItem"/>.
        /// </summary>
        /// <param name="other">The <see cref="InMemoryDataItem"/> to compare with the current <see cref="InMemoryDataItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="InMemoryDataItem"/> is equal to the current
        /// <see cref="InMemoryDataItem"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(InMemoryDataItem other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return ValueType.Equals(other.ValueType)
                   && Identity.Equals(other.Identity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDataItem"/> class.
        /// </summary>
        /// <param name="valueType">The type for which the object is being stored.</param>
        /// <param name="identity">The value's identity.</param>
        /// <param name="value">The data value.</param>
        public InMemoryDataItem(Type valueType, object identity, object value)
        {
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}

