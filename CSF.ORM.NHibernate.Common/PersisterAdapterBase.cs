using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// Base class for an NHibernate implementation of an <see cref="IPersister"/>.
    /// </summary>
    public abstract class PersisterAdapterBase : IPersister
    {
        /// <summary>
        /// Gets the NHibernate <see cref="ISession"/>.
        /// </summary>
        protected ISession Session { get; }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public object Add<T>(T item, object identity = null) where T : class
            => Session.Save(item);

        /// <summary>
        /// Delete the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void Delete<T>(T item, object identity) where T : class
            => Session.Delete(item);

        /// <summary>
        /// Update the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void Update<T>(T item, object identity) where T : class
            => Session.Update(item);

        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        public abstract Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken)) where T : class;

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public abstract Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class;

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public abstract Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersisterAdapterBase"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        public PersisterAdapterBase(ISession session)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }
    }
}
