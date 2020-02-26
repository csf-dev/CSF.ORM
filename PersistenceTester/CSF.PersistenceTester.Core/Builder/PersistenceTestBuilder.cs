using System;
using CSF.ORM;

namespace CSF.PersistenceTester.Builder
{
    /// <summary>
    /// A builder which sets up the early stages of the test, up util the point at
    /// which the entity to save is chosen.
    /// </summary>
    public class PersistenceTestBuilder : IChoosesEntityWithOptionalSetup
    {
        readonly IGetsDataConnection sessionProvider;
        Action<IDataConnection> setup;

        /// <summary>
        /// Adds a 'setup' or 'pre-test' step to the persistence test.  Use this to (for example) save dependent
        /// entities to the database before the tested entity is saved.
        /// </summary>
        /// <returns>A service which indicates the entity to save.</returns>
        /// <param name="setup">The setup action.</param>
        /// <param name="implicitTransaction">If set to <c>true</c> then the setup action will be performed within a transaction.</param>
        public IChoosesEntity WithSetup(Action<IDataConnection> setup, bool implicitTransaction = true)
        {
            if (setup == null) return this;

            if (!implicitTransaction)
            {
                this.setup = setup;
            }
            else
            {
                this.setup = s =>
                {
                    using(var tran = s.GetTransactionFactory().GetTransaction())
                    {
                        setup(s);
                        tran.Commit();
                    }
                };
            }

            return this;
        }

        /// <summary>
        /// Specifies the entity to be saved/retrieved and compared in the persistence test.
        /// </summary>
        /// <returns>A service which configures how the entity instances will be compared.</returns>
        /// <param name="entity">The entity instance to save.</param>
        public IConfiguresComparison<T> WithEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new PersistenceTestBuilder<T>(sessionProvider, entity, setup);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceTestBuilder"/> class.
        /// </summary>
        /// <param name="sessionProvider">A session provider.</param>
        public PersistenceTestBuilder(IGetsDataConnection sessionProvider)
        {
            this.sessionProvider = sessionProvider ?? throw new ArgumentNullException(nameof(sessionProvider));
        }
    }
}
