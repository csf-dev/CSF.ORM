//
// Identity`2.cs
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
using System.Collections.Generic;

namespace CSF.Entities
{
    /// <summary>
    /// Describes an immutable identity for an <see cref="IEntity"/> instance.  This is a unique identifier
    /// for that entity within the application's logic domain.
    /// </summary>
    /// <typeparam name="TIdentity">The underlying type of the identity value.</typeparam>
    /// <typeparam name="TEntity">The concrete type of <see cref="IEntity"/> which this instance described.</typeparam>
    [Serializable]
    public sealed class Identity<TIdentity, TEntity> : IIdentity<TEntity>,
                                                       IEquatable<IIdentity>,
                                                       IEquatable<IIdentity<TEntity>>,
                                                       IEquatable<Identity<TIdentity, TEntity>>
                                                       where TEntity : IEntity
    {
        readonly IEqualityComparer<IIdentity> comparer = new IdentityEqualityComparer();

        /// <summary>
        /// Gets a <see cref="Type"/> that indicates the type of entity that this instance describes.
        /// </summary>
        public Type EntityType { get { return typeof(TEntity); } }

        /// <summary>
        /// Gets the underlying type of <see cref="Value"/>.
        /// </summary>
        /// <value>The identity type.</value>
        public Type IdentityType { get { return typeof(TIdentity); } }

        /// <summary>
        /// Gets the identity value.
        /// </summary>
        public TIdentity Value { get; }

        object IIdentity.Value => Value;

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">
        /// A <see cref="System.Object"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as IIdentity);

        /// <summary>
        /// Determines whether the specified identity is equal to the current instance.
        /// </summary>
        /// <param name='other'>The identity to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if the specified identity is equal to the current instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IIdentity other) => comparer.Equals(this, other);

        /// <summary>
        /// Determines whether the specified identity is equal to the current instance.
        /// </summary>
        /// <param name='other'>The identity to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if the specified identity is equal to the current instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IIdentity<TEntity> other) => Equals((IIdentity)other);

        /// <summary>
        /// Determines whether the specified identity is equal to the current instance.
        /// </summary>
        /// <param name='other'>
        /// The identity to compare with the current instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified identity is equal to the current instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Identity<TIdentity, TEntity> other) => Equals((IIdentity)other);

        /// <summary>
        /// Computes a hash code for this identity instance.
        /// </summary>
        /// <returns>
        /// An <see cref="Int32"/> hash code.
        /// </returns>
        public override int GetHashCode() => comparer.GetHashCode(this);

        /// <summary>
        /// Gets a <see cref="String"/> representation of this identity.s
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public override string ToString() => string.Format(Resources.Strings.IdentityFormat, EntityType.Name, Value);

        /// <summary>
        /// Gets the identity value and converts it to a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public string GetValueAsString() => Value.ToString();

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity{TIdentity,TEntity}"/> class.
        /// </summary>
        /// <param name="identity">The identity value.</param>
        public Identity(TIdentity identity)
        {
            if (Equals(identity, default(TIdentity)))
                throw new ArgumentException(String.Format(Resources.ExceptionMessages.MustNotBeDefaultForDataTypeFormat, typeof(TIdentity).Name), nameof(identity));

            Value = identity;
        }

        /// <summary>
        /// Operator overload for testing equality between identity instances.
        /// </summary>
        /// <param name="objectA">An identity instance.</param>
        /// <param name="objectB">An identity instance.</param>
        public static bool operator ==(Identity<TIdentity, TEntity> objectA, IIdentity objectB)
        {
            if (ReferenceEquals(objectA, objectB)) return true;
            if (ReferenceEquals(objectA, null)) return false;
            return objectA.Equals(objectB);
        }

        /// <summary>
        /// Operator overload for testing inequality between identity instances.
        /// </summary>
        /// <param name="objectA">An identity instance.</param>
        /// <param name="objectB">An identity instance.</param>
        public static bool operator !=(Identity<TIdentity, TEntity> objectA, IIdentity objectB) => !(objectA == objectB);
    }
}

