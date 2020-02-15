//
// IdentityPopulatingTheoryQueryDecorator.cs
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
using System.Linq;
using CSF.Entities;

namespace CSF.ORM
{
    /// <summary>
    /// An implementation of <see cref="IQuery"/> which populates the identity value
    /// into a theory object, created by a wrapped query, when the <see cref="Theorise{TQueried}(object)"/>
    /// method is used.
    /// </summary>
    public class IdentityPopulatingTheoryQueryDecorator : IQuery
    {
        readonly IQuery wrapped;

        /// <summary>
        /// Implements the <see cref="IQuery.Theorise{TQueried}(object)"/> function, but additionally
        /// if the theorised object is an <see cref="IEntity"/> - ensures that its identity value has
        /// been set before it is returned.
        /// </summary>
        /// <returns>The object theory.</returns>
        /// <param name="identityValue">Identity value.</param>
        /// <typeparam name="TQueried">The 1st type parameter.</typeparam>
        public TQueried Theorise<TQueried>(object identityValue) where TQueried : class
        {
            var output = wrapped.Theorise<TQueried>(identityValue);

            if(output is IEntity entity && !entity.HasIdentity)
                entity.SetIdentity(identityValue);

            return output;
        }

        /// <summary>
        /// Gets a single instance from the underlying data source, identified by an identity value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will either get an object instance, or it will return <c>null</c> (if no instance is found).
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public TQueried Get<TQueried>(object identityValue) where TQueried : class => wrapped.Get<TQueried>(identityValue);

        /// <summary>
        /// Gets a new queryable data-source.
        /// </summary>
        /// <typeparam name="TQueried">The type of queried-for object.</typeparam>
        public IQueryable<TQueried> Query<TQueried>() where TQueried : class => wrapped.Query<TQueried>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityPopulatingTheoryQueryDecorator"/> class.
        /// </summary>
        /// <param name="wrapped">A wrapped query.</param>
        public IdentityPopulatingTheoryQueryDecorator(IQuery wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}
