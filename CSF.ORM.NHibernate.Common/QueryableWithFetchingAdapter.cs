using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// An adapter which wraps an NHibernate fetch request, exposing the interface
    /// <see cref="IQueryableWithEagerFetching{TQueried, TFetched}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This code is based on information in the following Stack Overflow answer: http://stackoverflow.com/a/5742158
    /// </para>
    /// </remarks>
    public class QueryableWithFetchingAdapter<TQueried, TFetched> : IQueryableWithEagerFetching<TQueried, TFetched>
    {
        readonly IQueryable<TQueried> wrapped;

        /// <summary>
        /// Gets the element type.
        /// </summary>
        /// <value>The type of the element.</value>
        public Type ElementType => wrapped.ElementType;

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public System.Linq.Expressions.Expression Expression => wrapped.Expression;

        /// <summary>
        /// Gets the Linq query provider.
        /// </summary>
        /// <value>The provider.</value>
        public IQueryProvider Provider => wrapped.Provider;

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<TQueried> GetEnumerator() => wrapped.GetEnumerator();

        /// <summary>
        /// Gets the original query which was used to create the current instance.
        /// </summary>
        /// <returns>The original query.</returns>
        public virtual IQueryable<TQueried> GetQueryable() => wrapped;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryableWithFetchingAdapter{TQueried, TFetched}"/> class.
        /// </summary>
        /// <param name="wrapped">The wrapped query.</param>
        public QueryableWithFetchingAdapter(IQueryable<TQueried> wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}

