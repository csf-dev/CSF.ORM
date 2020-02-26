using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace CSF.ORM.NHibernate
{
    /// <summary>
    /// NHibernate implementation of an <see cref="IPersister"/>.
    /// </summary>
    public class PersisterAdapter : PersisterAdapterBase
    {
        /// <summary>
        /// Adds the specified item to the data store.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        /// <param name="identity">Optional, the item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <returns>The identity value which the item has, after it was added.</returns>
        /// <typeparam name="T">The item type.</typeparam>
        public override Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken))
            => Task.Run(() => Add(item, identity), token);

        /// <summary>
        /// Updates the specified item in the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public override Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken))
            => Task.Run(() => Update(item, identity), token);

        /// <summary>
        /// Deletes the specified item from the data-store.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="identity">The item's identity.</param>
        /// <param name="token">A token with which the task may be cancelled.</param>
        /// <typeparam name="T">The item type.</typeparam>
        public override Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken))
            => Task.Run(() => Delete(item, identity), token);

        /// <summary>
        /// Initializes a new instance of the <see cref="PersisterAdapter"/> class.
        /// </summary>
        /// <param name="session">Session.</param>
        public PersisterAdapter(ISession session) : base(session) {}
    }
}
