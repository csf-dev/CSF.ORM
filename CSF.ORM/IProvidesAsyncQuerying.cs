//
// IProvidesAsyncQuerying.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 
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

namespace CSF.ORM
{
    /// <summary>
    /// A service which provides results from an ORM query via an asynchronous/task-based API.
    /// </summary>
    public interface IProvidesAsyncQuerying
    {
        #region AnyAsync

        /// <summary>Determines whether a sequence contains any elements.</summary>
        /// <param name="source">A sequence to check for being empty.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
        /// <param name="source">A sequence whose elements to test for a condition.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region AllAsync

        /// <summary>Determines whether all elements of a sequence satisfies a condition.</summary>
        /// <param name="source">A sequence whose elements to test for a condition.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if all elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<bool> AllAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region CountAsync

        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        Task<int> CountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the number of elements in the specified sequence that satisfies a condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        Task<int> CountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region SumAsync

        /// <summary>
        /// Computes the sum of a sequence of <see cref="T:System.Int32"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Int32"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue"/>.</exception>
        Task<int> SumAsync(IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="T:System.Int32"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue"/>.</exception>
        Task<int?> SumAsync(IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of <see cref="T:System.Int64"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Int64"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue"/>.</exception>
        Task<long> SumAsync(IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="T:System.Int64"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue"/>.</exception>
        Task<long?> SumAsync(IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of <see cref="T:System.Single"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Single"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Single.MaxValue"/>.</exception>
        Task<float> SumAsync(IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="T:System.Single"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Single.MaxValue"/>.</exception>
        Task<float?> SumAsync(IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of <see cref="T:System.Double"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Double"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Double.MaxValue"/>.</exception>
        Task<double> SumAsync(IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="T:System.Double"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Double.MaxValue"/>.</exception>
        Task<double?> SumAsync(IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of <see cref="T:System.Decimal"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Decimal"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"/>.</exception>
        Task<decimal> SumAsync(IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="T:System.Decimal"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal"/> values to calculate the sum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"/>.</exception>
        Task<decimal?> SumAsync(IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="T:System.Int32"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue"/>.</exception>
        Task<int> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="T:System.Int32"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue"/>.</exception>
        Task<int?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="T:System.Int64"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue"/>.</exception>
        Task<long> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="T:System.Int64"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue"/>.</exception>
        Task<long?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="T:System.Single"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Single.MaxValue"/>.</exception>
        Task<float> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="T:System.Single"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Single.MaxValue"/>.</exception>
        Task<float?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="T:System.Double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Double.MaxValue"/>.</exception>
        Task<double> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="T:System.Double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Double.MaxValue"/>.</exception>
        Task<double?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="T:System.Decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"/>.</exception>
        Task<decimal> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="T:System.Decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values of type <paramref name="source"/>.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"/>.</exception>
        Task<decimal?> SumAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Average

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Int32"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Int32"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync(IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Int32"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the source sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync(IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Int64"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Int64"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync(IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Int64"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the source sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync(IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Single"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Single"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<float> AverageAsync(IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Single"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the source sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<float?> AverageAsync(IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Double"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Double"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync(IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Double"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the source sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync(IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Decimal"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:System.Decimal"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<decimal> AverageAsync(IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Decimal"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal"/> values to calculate the average of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the source sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<decimal?> AverageAsync(IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Int32"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Int32"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the <paramref name="source"/> sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Int64"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Int64"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the <paramref name="source"/> sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Single"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<float> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Single"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the <paramref name="source"/> sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<float?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<double> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the <paramref name="source"/> sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of <see cref="T:System.Decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        Task<decimal> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="T:System.Decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The average of the sequence of values, or <see langword="null"/> if the <paramref name="source"/> sequence is empty or contains only <see langword="null"/> values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<decimal?> AverageAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region MinAsync

        /// <summary>
        /// Returns the minimum value of a generic <see cref="T:System.Linq.IQueryable`1"/>.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The minimum value in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> MinAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Invokes a projection function on each element of a generic <see cref="T:System.Linq.IQueryable`1"/> and returns the minimum resulting value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector"/>.</typeparam>
        /// <returns>
        /// The minimum value in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TResult> MinAsync<TSource, TResult>(IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region MaxAsync

        /// <summary>
        /// Returns the maximum value in a generic <see cref="T:System.Linq.IQueryable`1"/>.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// The maximum value in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> MaxAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Invokes a projection function on each element of a generic <see cref="T:System.Linq.IQueryable`1"/> and returns the maximum resulting value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum of.</param>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector"/>.</typeparam>
        /// <returns>
        /// The maximum value in the sequence.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        Task<TResult> MaxAsync<TSource, TResult>(IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region LongCountAsync

        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the number of elements in the specified sequence that satisfies a condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region FirstAsync

        /// <summary>Returns the first element of a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The first element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the first element of a sequence that satisfies a specified condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The first element in <paramref name="source" /> that passes the test in <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region SingleAsync

        /// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The single element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>The single element in <paramref name="source" /> that passes the test in <paramref name="predicate" />.</returns>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region SingleOrDefaultAsync

        /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the single element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty; otherwise, the single element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the single element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region FirstOrDefaultAsync

        /// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region ToListAsync

        /// <summary>
        /// Executes the query and returns its result as a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return a list from.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>A <see cref="List{T}"/> containing the result of the query.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}