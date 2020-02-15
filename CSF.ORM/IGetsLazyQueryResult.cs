//
// IGetsLazyQueryResult.cs
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

namespace CSF.ORM
{
    /// <summary>
    /// A service which can get 'lazy' results from a query.
    /// This generally means that the interaction the underlying data-source is
    /// deferred until the lazy object's value is used.
    /// </summary>
    public interface IGetsLazyQueryResult
    {
        /// <summary>
        /// Gets the result of the query as an <see cref="IEnumerable{T}"/>, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </summary>
        /// <returns>The lazy query result.</returns>
        /// <param name="query">The query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        Lazy<IEnumerable<T>> GetLazyEnumerable<T>(IQueryable<T> query);

        /// <summary>
        /// Gets the result of a getter-expression from the query, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </summary>
        /// <returns>The lazy value.</returns>
        /// <param name="query">The query.</param>
        /// <param name="valueExpression">An expression which would retrieve the value from the query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        /// <typeparam name="V">The type of the value retrieved from the query.</typeparam>
        Lazy<V> GetLazyValue<T, V>(IQueryable<T> query, Expression<Func<IQueryable<T>, V>> valueExpression);
    }
}
