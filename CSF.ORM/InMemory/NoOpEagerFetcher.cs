//
// NoOpEagerFetcher.cs
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

namespace CSF.ORM.InMemory
{
    /// <summary>
    /// No-op implementation of <see cref="IEagerlyFetchesFromQuery"/> which simply returns
    /// a wrapper around the original query.
    /// </summary>
    public class NoOpEagerFetcher : IEagerlyFetchesFromQuery
    {
        public IQueryableWithEagerFetching<TQueried, TChild> FetchChild<TQueried, TChild>(IQueryable<TQueried> query, Expression<Func<TQueried, TChild>> getterExpression)
        {
            return new NoOpEagerFetchQueryableAdapter<TQueried, TChild>(query);
        }

        public IQueryableWithEagerFetching<TQueried, TChild> FetchChildren<TQueried, TChild>(IQueryable<TQueried> query, Expression<Func<TQueried, IEnumerable<TChild>>> getterExpression)
        {
            return new NoOpEagerFetchQueryableAdapter<TQueried, TChild>(query);
        }

        public IQueryableWithEagerFetching<TQueried, TGrandchild> FetchGrandchild<TQueried, TChild, TGrandchild>(IQueryableWithEagerFetching<TQueried, TChild> query, Expression<Func<TChild, TGrandchild>> getterExpression)
        {
            return new NoOpEagerFetchQueryableAdapter<TQueried, TGrandchild>(query);
        }

        public IQueryableWithEagerFetching<TQueried, TGrandchild> FetchGrandchildren<TQueried, TChild, TGrandchild>(IQueryableWithEagerFetching<TQueried, TChild> query, Expression<Func<TChild, IEnumerable<TGrandchild>>> getterExpression)
        {
            return new NoOpEagerFetchQueryableAdapter<TQueried, TGrandchild>(query);
        }
    }
}
