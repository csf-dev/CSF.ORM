using System;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// Base class for an NHibernate implementation of the generic <c>IQuery</c> type, which wraps an NHibernate <c>ISession</c>, using it
    /// as the query source.
    /// </summary>
    public abstract class QueryAdapterBase : IQuery
    {
        /// <summary>
        /// Gets the NHibernate <see cref="ISession"/>.
        /// </summary>
        protected ISession Session { get; }

        /// <summary>
        /// Creates an instance of the given object-type, based upon a theory that it exists in the underlying data-source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will always return a non-null object instance, even if the underlying object does not exist in the
        /// data source.  If a 'theory object' is created for an object which does not actually exist, then an exception
        /// could be thrown if that theory object is used.
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public TQueried Theorise<TQueried>(object identityValue) where TQueried : class
            => Session.Load<TQueried>(identityValue);

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
        public TQueried Get<TQueried>(object identityValue) where TQueried : class
            => Session.Get<TQueried>(identityValue);

        /// <summary>
        /// Gets a new queryable data-source.
        /// </summary>
        /// <typeparam name="TQueried">The type of queried-for object.</typeparam>
        public abstract IQueryable<TQueried> Query<TQueried>() where TQueried : class;

        /// <summary>
        /// Creates an instance of the given object-type, based upon a theory that it exists in the underlying data-source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method should always return a non-null object instance, even if the underlying object does not exist in the
        /// data source.  If a 'theory object' is created for an object which does not actually exist, then an exception
        /// could be thrown if that theory object is used.
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public abstract Task<TQueried> TheoriseAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken)) where TQueried : class;

        /// <summary>
        /// Gets a single instance from the underlying data source, identified by an identity value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will either get an object instance, or it will return <c>null</c> (if no instance is found).
        /// </para>
        /// </remarks>
        /// <param name="identityValue">The identity value for the object to retrieve.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="TQueried">The type of object to retrieve.</typeparam>
        public abstract Task<TQueried> GetAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken)) where TQueried : class;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAdapterBase"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        protected QueryAdapterBase(ISession session)
        {
            this.Session = session ?? throw new ArgumentNullException(nameof(session));
        }
    }
}

