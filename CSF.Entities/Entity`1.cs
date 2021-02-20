//
// Entity`2.cs
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
    /// Abstract base type for an <see cref="IEntity"/> class, defining the baseline
    /// functionality.  This class is generic for the type which will be used for the
    /// identity value.  Consider using <see cref="long"/> or <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identity type for the current instance.</typeparam>
    [Serializable, IgnoreForIdentityEquality]
    public abstract class Entity<TIdentity> : IEntity
    {
        /// <summary>
        /// Gets or sets the identity value for the current instance.
        /// </summary>
        /// <value>The identity value.</value>
        protected virtual TIdentity IdentityValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has an identity.
        /// </summary>
        /// <value><c>true</c> if this instance has an identity; otherwise, <c>false</c>.</value>
        protected virtual bool HasIdentity => !Equals(IdentityValue, default(TIdentity));

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="Entity{TIdentity}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current <see cref="Entity{TIdentity}"/>.
        /// </returns>
        public override string ToString()
        {
            string identityPart = HasIdentity ? IdentityValue.ToString() : Resources.Strings.NoIdentity;
            return String.Format(Resources.Strings.IdentityFormat, this.GetType().Name, identityPart);
        }

        bool IEntity.HasIdentity => HasIdentity;

        object IEntity.IdentityValue
        {
            get { return IdentityValue; }
            set { IdentityValue = ToIdentityType(value); }
        }

        /// <summary>
        /// Converts a specified <paramref name="value"/> to the <typeparamref name="TIdentity"/> type.
        /// </summary>
        /// <returns>The converted value.</returns>
        /// <param name="value">The value to convert.</param>
        static TIdentity ToIdentityType(object value)
        {
            var formatter = System.Globalization.CultureInfo.InvariantCulture;
            var converted = Convert.ChangeType(value, typeof(TIdentity), formatter);
            return (TIdentity)converted;
        }

        Type IEntity.IdentityType => typeof(TIdentity);
    }
}

