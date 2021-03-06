﻿//
// LazyResultProvider.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using NhQueryProvider = NHibernate.Linq.DefaultQueryProvider;

/* Note that SonarQube will mark this file up as being a duplicate of the NHibernate 4 version.
 * They cannot be consolidated into the common project though, because the method signatures of
 * the NH 4 & 5 versions of methods on the LinqExtensionMethods class are different.
 *
 * Trying to consolidate these two versions of the class leads to MissingMethodException errors.
 */

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// An implementation of <see cref="IGetsLazyQueryResult"/> which uses NHibernate to batch
    /// queries together so that they may be retrieved from the database using a single round-trip.
    /// </summary>
    public class LazyResultProvider : IGetsLazyQueryResult
    {
        /// <summary>
        /// Gets the result of the query as an <see cref="IList{T}"/>, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </summary>
        /// <returns>The lazy query result.</returns>
        /// <param name="query">The query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        public Lazy<IList<T>> GetLazyList<T>(IQueryable<T> query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if(!(query.Provider is NhQueryProvider))
                throw new ArgumentException($"The query provider must be an instance of {typeof(NhQueryProvider).FullName}.", nameof(query));

            var futureEnumerable = LinqExtensionMethods.ToFuture(query);
            return new Lazy<IList<T>>(() => futureEnumerable.ToList());
        }

        /// <summary>
        /// Gets the result of a getter-expression from the query, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </summary>
        /// <returns>The lazy value.</returns>
        /// <param name="query">The query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        public Lazy<T> GetLazyValue<T>(IQueryable<T> query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (!(query.Provider is NhQueryProvider))
                throw new ArgumentException($"The query provider must be an instance of {typeof(NhQueryProvider).FullName}.", nameof(query));

            var futureValue = LinqExtensionMethods.ToFutureValue(query);
            return new Lazy<T>(() => futureValue.Value);
        }

        /// <summary>
        /// Gets the result of a getter-expression from the query, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </summary>
        /// <returns>The lazy value.</returns>
        /// <param name="query">The query.</param>
        /// <param name="valueExpression">An expression which would retrieve the value from the query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        /// <typeparam name="V">The type of the value retrieved from the query.</typeparam>
        public Lazy<V> GetLazyValue<T, V>(IQueryable<T> query, Expression<Func<IQueryable<T>, V>> valueExpression)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (!(query.Provider is NhQueryProvider))
                throw new ArgumentException($"The query provider must be an instance of {typeof(NhQueryProvider).FullName}.", nameof(query));

            var futureValue = LinqExtensionMethods.ToFutureValue(query, valueExpression);
            return new Lazy<V>(() => futureValue.Value);
        }
    }
}
