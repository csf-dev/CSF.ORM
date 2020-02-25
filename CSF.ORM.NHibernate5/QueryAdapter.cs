using System;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// NHibernate implementation of the generic <c>IQuery</c> type, which wraps an NHibernate <c>ISession</c>, using it
    /// as the query source.
    /// </summary>
    public class QueryAdapter : QueryAdapterBase
    {
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
        public override Task<TQueried> TheoriseAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken))
            => Session.LoadAsync<TQueried>(identityValue, token);

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
        public override Task<TQueried> GetAsync<TQueried>(object identityValue, CancellationToken token = default(CancellationToken))
            => Session.GetAsync<TQueried>(identityValue, token);

        /// <summary>
        /// Initializes a new instance of the <see cref="CSF.ORM.NHibernate.QueryAdapter"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        public QueryAdapter(ISession session) : base(session) {}
    }
}

