//
// NoOpAsyncQueryProvider.cs
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
using System.Threading;
using System.Threading.Tasks;
using NhLinq = NHibernate.Linq.LinqExtensionMethods;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// An implementation of <see cref="IProvidesAsyncQuerying"/> which just runs the
    /// commands synchronously.
    /// </summary>
    public class AsyncQueryProvider : IProvidesAsyncQuerying
    {
        public Task<bool> AllAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AllAsync(source, predicate, cancellationToken);

        public Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AnyAsync(source, cancellationToken);

        public Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AnyAsync(source, predicate, cancellationToken);

        public Task<double> AverageAsync(IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);
        
        public Task<double?> AverageAsync(IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<double> AverageAsync(IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<double?> AverageAsync(IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<float> AverageAsync(IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<float?> AverageAsync(IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<double> AverageAsync(IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<double?> AverageAsync(IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<decimal> AverageAsync(IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<decimal?> AverageAsync(IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, cancellationToken);

        public Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<float> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<float?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<decimal> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<decimal?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.AverageAsync(source, selector, cancellationToken);

        public Task<int> CountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.CountAsync(source, cancellationToken);

        public Task<int> CountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.CountAsync(source, predicate, cancellationToken);

        public Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.FirstAsync(source, cancellationToken);

        public Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.FirstAsync(source, predicate, cancellationToken);

        public Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.FirstOrDefaultAsync(source, cancellationToken);

        public Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.FirstOrDefaultAsync(source, predicate, cancellationToken);

        public Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.LongCountAsync(source, cancellationToken);

        public Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.LongCountAsync(source, predicate, cancellationToken);

        public Task<TSource> MaxAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.MaxAsync(source, cancellationToken);

        public Task<TResult> MaxAsync<TSource, TResult>(IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.MaxAsync(source, selector, cancellationToken);

        public Task<TSource> MinAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.MinAsync(source, cancellationToken);

        public Task<TResult> MinAsync<TSource, TResult>(IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.MinAsync(source, selector, cancellationToken);

        public Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SingleAsync(source, cancellationToken);

        public Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SingleAsync(source, predicate, cancellationToken);

        public Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SingleOrDefaultAsync(source, cancellationToken);

        public Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SingleOrDefaultAsync(source, predicate, cancellationToken);

        public Task<int> SumAsync(IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<int?> SumAsync(IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<long> SumAsync(IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<long?> SumAsync(IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<float> SumAsync(IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<float?> SumAsync(IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<double> SumAsync(IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<double?> SumAsync(IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<decimal> SumAsync(IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<decimal?> SumAsync(IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, cancellationToken);

        public Task<int> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<int?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<long> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<long?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<float> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<float?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<double> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<double?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<decimal> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<decimal?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.SumAsync(source, selector, cancellationToken);

        public Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => NhLinq.ToListAsync(source, cancellationToken);
    }
}
