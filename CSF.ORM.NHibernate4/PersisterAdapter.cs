using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// NHibernate implementation of an <see cref="IPersister"/>.
    /// </summary>
    public class PersisterAdapter : IPersister
    {
        readonly ISession session;

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public object Add<T>(T item, object identity) where T : class
            => session.Save(item);

        /// <summary>
        /// Delete the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void Delete<T>(T item, object identity) where T : class
            => session.Delete(item);

        /// <summary>
        /// Update the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="identity">Identity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void Update<T>(T item, object identity) where T : class
            => session.Update(item);

        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        public Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken)) where T : class
            => Task.Run(() => Add(item, identity), token);

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => Task.Run(() => Update(item, identity), token);

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => Task.Run(() => Delete(item, identity), token);

        /// <summary>
        /// Initializes a new instance of the <see cref="PersisterAdapter"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        public PersisterAdapter(ISession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
    }
}
