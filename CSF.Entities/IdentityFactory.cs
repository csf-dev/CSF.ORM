//
// IdentityFactory.cs
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
    /// A factory for identity instances which uses <see cref="Activator"/> to create objects.
    /// </summary>
    public class IdentityFactory : ICreatesIdentity
    {
        static readonly IGetsIdentityType identityTypeProvider = new IdentityTypeProvider();

        /// <summary>
        /// Create an identity from the specified parameters.
        /// </summary>
        /// <returns>The created identity, or a <c>null</c> reference if the identity value is equal to the default of its data-type.</returns>
        /// <param name="entityType">The entity type.</param>
        /// <param name="identityValue">The identity value.</param>
        public IIdentity Create(Type entityType, object identityValue)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            var identityType = identityTypeProvider.GetIdentityType(entityType);
            return Create(entityType, identityType, identityValue);
        }

        /// <summary>
        /// Create an identity from the specified parameters.
        /// </summary>
        /// <returns>The created identity, or a <c>null</c> reference if the identity value is equal to the default of its data-type.</returns>
        /// <param name="entityType">The entity type.</param>
        /// <param name="identityType">The identity-value type.</param>
        /// <param name="identityValue">The identity value.</param>
        public IIdentity Create(Type entityType, Type identityType, object identityValue)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));
            if (identityType == null)
                throw new ArgumentNullException(nameof(identityType));
            if (Equals(identityValue, GetDefaultValue(identityType)))
                return null;

            object convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(identityValue, identityType);
            }
            catch (Exception e)
            {
                var message = String.Format(Resources.ExceptionMessages.InvalidIdentityType, identityType.Name, identityValue.GetType());
                throw new ArgumentException(message, nameof(identityValue), e);
            }

            var closedIdentityType = typeof(Identity<,>).MakeGenericType(identityType, entityType);
            return (IIdentity)Activator.CreateInstance(closedIdentityType, new[] { convertedValue });
        }

        object GetDefaultValue(Type identityType)
            => identityType.IsValueType ? Activator.CreateInstance(identityType) : null;
    }
}
