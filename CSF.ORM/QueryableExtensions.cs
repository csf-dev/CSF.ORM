//
// QueryableExtensions.cs
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
using CSF.ORM.InMemory;

namespace CSF.ORM
{
    /// <summary>
    /// Extension methods for <see cref="IQueryable{T}"/>, relating to ORM functionality.
    /// The actual implementation for these may be swapped app-wide via the <c>public static</c>
    /// properties on this class.
    /// </summary>
    public static class QueryableExtensions
    {
        #region Provider getters and setters

        static IEagerlyFetchesFromQuery eagerFetchingProvider;

        /// <summary>
        /// Gets or sets the implementation service which provides eager-fetching.
        /// </summary>
        /// <value>The eager fetching provider.</value>
        public static IEagerlyFetchesFromQuery EagerFetchingProvider
        {
            get => eagerFetchingProvider;
            set => eagerFetchingProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        static IGetsLazyQueryResult lazyQueryingProvider;

        /// <summary>
        /// Gets or sets the implementation service which provides lazy querying.
        /// </summary>
        /// <value>The lazy querying provider.</value>
        public static IGetsLazyQueryResult LazyQueryingProvider
        {
            get => lazyQueryingProvider;
            set => lazyQueryingProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        static IProvidesAsyncQuerying asyncQueryingProvider;

        /// <summary>
        /// Gets or sets the implementation service which provides asynchronous querying.
        /// </summary>
        /// <value>The async querying provider.</value>
        public static IProvidesAsyncQuerying AsyncQueryingProvider
        {
            get => asyncQueryingProvider;
            set => asyncQueryingProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Lazy querying

        /// <summary>
        /// <para>
        /// Gets the result of the query as an <see cref="IEnumerable{T}"/>, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </para>
        /// <para>
        /// For some ORM providers, this allows the backend to 'batch' data access
        /// operations together for performance gains.  Developers may use this
        /// functionality to create many 'lazy' queries, none of which are executed against
        /// the underlying data-source until a value from any of the queries in the batch
        /// is accessed.
        /// </para>
        /// </summary>
        /// <returns>The lazy query result.</returns>
        /// <param name="query">The query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        public static Lazy<IEnumerable<T>> ToLazy<T>(this IQueryable<T> query)
        {
            return LazyQueryingProvider.GetLazyEnumerable(query);
        }

        /// <summary>
        /// <para>
        /// Gets the result of a getter-expression from the query, but lazily so
        /// that the data-source will not be contacted until the value is retrieved.
        /// </para>
        /// <para>
        /// For some ORM providers, this allows the backend to 'batch' data access
        /// operations together for performance gains.  Developers may use this
        /// functionality to create many 'lazy' queries, none of which are executed against
        /// the underlying data-source until a value from any of the queries in the batch
        /// is accessed.
        /// </para>
        /// </summary>
        /// <returns>The lazy value.</returns>
        /// <param name="query">The query.</param>
        /// <param name="valueExpression">An expression which would retrieve the value from the query.</param>
        /// <typeparam name="T">The type of object queried.</typeparam>
        /// <typeparam name="V">The type of the value retrieved from the query.</typeparam>
        public static Lazy<V> ToLazyValue<T, V>(this IQueryable<T> query, Expression<Func<IQueryable<T>, V>> valueExpression)
        {
            return LazyQueryingProvider.GetLazyValue(query, valueExpression);
        }

        #endregion

        #region Eager fetching

        /// <summary>
        /// Eagerly fetches a 'child' object from the queried object.
        /// </summary>
        /// <returns>An object equivalent to the original query.</returns>
        /// <param name="query">A query.</param>
        /// <param name="getterExpression">An expression indicating the child object to be fetched.</param>
        /// <typeparam name="TQueried">The queried type.</typeparam>
        /// <typeparam name="TChild">The type of the child object which is to be fetched.</typeparam>
        public static IQueryableWithEagerFetching<TQueried, TChild> FetchChild<TQueried, TChild>(this IQueryable<TQueried> query,
                                                                                                 Expression<Func<TQueried, TChild>> getterExpression)
        {
            return EagerFetchingProvider.FetchChild(query, getterExpression);
        }

        /// <summary>
        /// Eagerly fetches a collection of 'child' objects from the queried object.
        /// </summary>
        /// <returns>An object equivalent to the original query.</returns>
        /// <param name="query">A query.</param>
        /// <param name="getterExpression">An expression indicating the collection of child objects to be fetched.</param>
        /// <typeparam name="TQueried">The queried type.</typeparam>
        /// <typeparam name="TChild">The type of the child objects which are to be fetched.</typeparam>
        public static IQueryableWithEagerFetching<TQueried, TChild> FetchChildren<TQueried, TChild>(this IQueryable<TQueried> query,
                                                                                                    Expression<Func<TQueried, IEnumerable<TChild>>> getterExpression)
        {
            return EagerFetchingProvider.FetchChildren(query, getterExpression);
        }

        /// <summary>
        /// Eagerly fetches a 'grandchild' object from child object(s) which have themselves been fetched.
        /// </summary>
        /// <returns>An object equivalent to the original query.</returns>
        /// <param name="query">A query in which a child object or collection of objects has been fetched.</param>
        /// <param name="getterExpression">An expression indicating the grandchild object to be fetched.</param>
        /// <typeparam name="TQueried">The queried type.</typeparam>
        /// <typeparam name="TChild">The type of the child object which has been fetched.</typeparam>
        /// <typeparam name="TGrandchild">The type of the grandchild object which is to be fetched.</typeparam>
        public static IQueryableWithEagerFetching<TQueried, TGrandchild> ThenFetchGrandchild<TQueried, TChild, TGrandchild>(this IQueryableWithEagerFetching<TQueried, TChild> query,
                                                                                                                            Expression<Func<TChild, TGrandchild>> getterExpression)
        {
            return EagerFetchingProvider.FetchGrandchild(query, getterExpression);
        }

        /// <summary>
        /// Eagerly fetches a collection of 'grandchild' objects from child object(s) which have themselves been fetched.
        /// </summary>
        /// <returns>An object equivalent to the original query.</returns>
        /// <param name="query">A query in which a child object or collection of objects has been fetched.</param>
        /// <param name="getterExpression">An expression indicating the collection of grandchild objects to be fetched.</param>
        /// <typeparam name="TQueried">The queried type.</typeparam>
        /// <typeparam name="TChild">The type of the child object which has been fetched.</typeparam>
        /// <typeparam name="TGrandchild">The type of the grandchild objects which are to be fetched.</typeparam>
        public static IQueryableWithEagerFetching<TQueried, TGrandchild> ThenFetchGrandchildren<TQueried, TChild, TGrandchild>(this IQueryableWithEagerFetching<TQueried, TChild> query,
                                                                                                                               Expression<Func<TChild, IEnumerable<TGrandchild>>> getterExpression)
        {
            return EagerFetchingProvider.FetchGrandchildren(query, getterExpression);
        }

        #endregion

        #region Async querying

        /// <summary>Determines whether a sequence contains any elements.</summary>
        /// <param name="source">A sequence to check for being empty.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AnyAsync(source, cancellationToken);

        /// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
        /// <param name="source">A sequence whose elements to test for a condition.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AnyAsync(source, predicate, cancellationToken);

        /// <summary>Determines whether all elements of a sequence satisfies a condition.</summary>
        /// <param name="source">A sequence whose elements to test for a condition.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>true if all elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AllAsync(source, predicate, cancellationToken);

        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.CountAsync(source, cancellationToken);

        /// <summary>Returns the number of elements in the specified sequence that satisfies a condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.CountAsync(source, predicate, cancellationToken);

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
        public static Task<int> SumAsync(this IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<int?> SumAsync(this IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<long> SumAsync(this IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<long?> SumAsync(this IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<float> SumAsync(this IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<float?> SumAsync(this IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<double> SumAsync(this IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<double?> SumAsync(this IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<decimal> SumAsync(this IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<decimal?> SumAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, cancellationToken);

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
        public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SumAsync(source, selector, cancellationToken);

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
        public static Task<double> AverageAsync(this IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double?> AverageAsync(this IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double> AverageAsync(this IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double?> AverageAsync(this IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<float> AverageAsync(this IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<float?> AverageAsync(this IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double> AverageAsync(this IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double?> AverageAsync(this IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<decimal> AverageAsync(this IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, cancellationToken);

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
        public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.AverageAsync(source, selector, cancellationToken);

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
        public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.MinAsync(source, cancellationToken);

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
        public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.MinAsync(source, selector, cancellationToken);

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
        public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.MaxAsync(source, cancellationToken);

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
        public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.MaxAsync(source, selector, cancellationToken);

        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.LongCountAsync(source, cancellationToken);

        /// <summary>Returns the number of elements in the specified sequence that satisfies a condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.LongCountAsync(source, predicate, cancellationToken);

        /// <summary>Returns the first element of a sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The first element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.FirstAsync(source, cancellationToken);

        /// <summary>Returns the first element of a sequence that satisfies a specified condition.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The first element in <paramref name="source" /> that passes the test in <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.FirstAsync(source, predicate, cancellationToken);

        /// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>The single element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SingleAsync(source, cancellationToken);

        /// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>The single element in <paramref name="source" /> that passes the test in <paramref name="predicate" />.</returns>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SingleAsync(source, predicate, cancellationToken);

        /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the single element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty; otherwise, the single element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SingleOrDefaultAsync(source, cancellationToken);

        /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the single element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.SingleOrDefaultAsync(source, predicate, cancellationToken);

        /// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
        /// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.FirstOrDefaultAsync(source, cancellationToken);

        /// <summary>Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.</summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>default(<paramref name="source" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.FirstOrDefaultAsync(source, predicate, cancellationToken);

        /// <summary>
        /// Executes the query and returns its result as a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return a list from.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>A <see cref="List{T}"/> containing the result of the query.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="source" /> <see cref="IQueryable.Provider" /> is not a supported query provider.</exception>
        public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
            => AsyncQueryingProvider.ToListAsync(source, cancellationToken);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the <see cref="QueryableExtensions"/> class.  This sets up the use of the
        /// in-memory/no-op implementations by default.
        /// </summary>
        static QueryableExtensions()
        {
            EagerFetchingProvider = new NoOpEagerFetcher();
            LazyQueryingProvider = new InMemoryLazyResultProvider();
            AsyncQueryingProvider = new SynchronousAsyncQueryProvider();
        }

        #endregion
    }
}
