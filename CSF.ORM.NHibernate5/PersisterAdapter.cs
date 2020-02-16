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

        public Task<object> AddAsync<T>(T item, object identity = null, CancellationToken token = default(CancellationToken)) where T : class
            => session.SaveAsync(item, token);

        public Task UpdateAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => session.UpdateAsync(item, token);

        public Task DeleteAsync<T>(T item, object identity, CancellationToken token = default(CancellationToken)) where T : class
            => session.DeleteAsync(item, token);

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
