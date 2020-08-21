using System;
using CSF.EqualityRules;
using CSF.ORM;

namespace CSF.PersistenceTester.Impl
{
    /// <summary>
    /// The persistence tester, which contains the logic to perform the actual persistence test itself
    /// and get a result.
    /// </summary>
    public class PersistenceTester<T> : ITestsPersistence<T>where T : class
    {
        readonly PersistenceTestSpec<T> spec;
        object identity;

        /// <summary>
        /// Gets the test result.
        /// </summary>
        /// <returns>The test result.</returns>
        public PersistenceTestResult GetTestResult()
        {
            PersistenceTestResult result = null;

            result = TrySetup();
            if (result != null) return result;

            result = TrySave();
            if (result != null) return result;

            return TryCompare();
        }

        PersistenceTestResult TrySetup()
        {
            IDataConnection conn = null;

            try
            {
                conn = spec.SessionProvider.GetConnection();
                spec.Setup?.Invoke(conn);
            }
            catch(Exception ex)
            {
                return new PersistenceTestResult(typeof(T))
                {
                    SetupException = ex
                };
            }
            finally
            {
                conn?.Dispose();
            }

            return null;
        }

        PersistenceTestResult TrySave()
        {
            IDataConnection conn = null;

            try
            {
                conn = spec.SessionProvider.GetConnection();
                using (var tran = conn.GetTransactionFactory().GetTransaction())
                {
                    identity = conn.GetPersister().Add(spec.Entity);
                    if (identity == null)
                        throw new InvalidOperationException($"The entity identity returned by {nameof(IPersister)}.{nameof(IPersister.Add)} should not be null.");
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                return new PersistenceTestResult(typeof(T))
                {
                    SaveException = ex
                };
            }
            finally
            {
                conn?.Dispose();
            }

            return null;
        }

        PersistenceTestResult TryCompare()
        {
            IDataConnection conn = null;

            try
            {
                conn = spec.SessionProvider.GetConnection();
                conn.EvictFromCache(spec.Entity);

                var retrieved = conn.GetQuery().Get<T>(identity);
                if (ReferenceEquals(retrieved, null))
                    return new PersistenceTestResult(typeof(T)) { SavedObjectNotFound = true };

                var equalityResult = spec.EqualityRule.GetEqualityResult(spec.Entity, retrieved);
                EnsureEqualityResultMayBeReportedUpon(equalityResult);

                return new PersistenceTestResult(typeof(T))
                {
                    EqualityResult = equalityResult
                };
            }
            catch (Exception ex)
            {
                return new PersistenceTestResult(typeof(T))
                {
                    ComparisonException = ex
                };
            }
            finally
            {
                conn?.Dispose();
            }
        }

        /// <summary>
        /// If either the persisted value or the retrieved value are NHibernate proxies, then we probably won't
        /// be able to use <see cref="Object.ToString()"/> later-on, once the connection has been terminated.
        /// This method ensures that both values can be converted to string now, while we still have the connection.
        /// </summary>
        /// <param name="equalityResult">An equality result.</param>
        void EnsureEqualityResultMayBeReportedUpon(EqualityResult equalityResult)
        {
            foreach(var item in equalityResult.RuleResults)
            {
                try { item.ValueA?.ToString(); }
                catch(Exception e)
                {
                    throw new PersistenceTestingException($"An exception was raised when pre-converting the persisted value {item.Name} to string.", e);
                }

                try { item.ValueB?.ToString(); }
                catch (Exception e)
                {
                    throw new PersistenceTestingException($"An exception was raised when pre-converting the retrieved value {item.Name} to string.", e);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceTester{T}"/> class.
        /// </summary>
        /// <param name="spec">The specification for a persistence test.</param>
        public PersistenceTester(PersistenceTestSpec<T> spec)
        {
            this.spec = spec ?? throw new ArgumentNullException(nameof(spec));
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Dispose the current instance.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> then disposal is explicit.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && spec.SessionProvider is IDisposable disposableProvider)
                    disposableProvider.Dispose();

                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="PersistenceTester{T}"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
        /// <see cref="PersistenceTester{T}"/>. The <see cref="Dispose()"/> method leaves the
        /// <see cref="PersistenceTester{T}"/> in an unusable state. After calling
        /// <see cref="Dispose()"/>, you must release all references to the
        /// <see cref="PersistenceTester{T}"/> so the garbage collector can reclaim the
        /// memory that the <see cref="PersistenceTester{T}"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
