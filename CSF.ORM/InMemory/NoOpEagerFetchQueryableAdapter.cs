﻿//
// NoOpEagerFetchQueryableAdapter.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// No-op implementation of <see cref="IQueryableWithEagerFetching{TQueried, TFetched}"/> which simply wraps a queryable object.
    /// </summary>
    public class NoOpEagerFetchQueryableAdapter<TQuery, TFetched> : IQueryableWithEagerFetching<TQuery, TFetched>
    {
        readonly IQueryable<TQuery> queryable;

        public Type ElementType => queryable.ElementType;

        public Expression Expression => queryable.Expression;

        public IQueryProvider Provider => queryable.Provider;

        public IEnumerator<TQuery> GetEnumerator() => queryable.GetEnumerator();

        public IQueryable<TQuery> GetQuerable() => queryable;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public NoOpEagerFetchQueryableAdapter(IQueryable<TQuery> queryable)
        {
            this.queryable = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }
    }
}
