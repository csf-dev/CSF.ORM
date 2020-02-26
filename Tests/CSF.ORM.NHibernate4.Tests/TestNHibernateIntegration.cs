using System.Linq;
using NUnit.Framework;
using Test.CSF.ORM.NHibernate.Config;
using Test.CSF.ORM.NHibernate.Models;
using Test.CSF.ORM.NHibernate.Entities;
using CSF.ORM.NHibernate;
using CSF.ORM;
using CSF.ORM.InMemory;

namespace Test.CSF.ORM.NHibernate
{
    [TestFixture, NonParallelizable]
    public class TestNHibernateIntegration
    {
        #region fields

        private global::NHibernate.Cfg.Configuration _config;
        private global::NHibernate.ISessionFactory _sessionFactory;
        private global::NHibernate.ISession _session;
        private IQuery _query;

        #endregion

        #region setup

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            var provider = new NHibernateProvider();
            _config = provider.CreateConfiguration();

            _sessionFactory = provider.CreateSessionFactory(_config);

            _session = _sessionFactory.OpenSession();

            provider.ExportSchema(_config, _session.Connection);

            using (var trans = _session.BeginTransaction())
            {
                var modelCreator = new NHibernateQueryingModel();
                modelCreator.CreateModel(_session);
                trans.Commit();
            }

            _query = new QueryAdapter(_session);

            QueryableExtensions.EagerFetchingProvider = new EagerFetchingProvider();
            QueryableExtensions.LazyQueryingProvider = new LazyResultProvider();
            QueryableExtensions.AsyncQueryingProvider = new SynchronousAsyncQueryProvider();
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
            if (_sessionFactory != null)
            {
                _sessionFactory.Dispose();
            }
        }

        #endregion

        #region tests

        [Test]
        public void FetchChildren_does_not_raise_exception()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory);

            // Act & Assert
            Assert.DoesNotThrow(() => query.ToList());
        }

        [Test]
        public void FetchChildren_returns_non_null_result()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory);

            // Act & Assert
            Assert.NotNull(query.ToList());
        }

        [Test]
        public void FetchChild_does_not_raise_exception()
        {
            // Arrange
            var query = (from store in _query.Query<InventoryItem>()
                         select store)
              .FetchChild(x => x.Product);

            // Act & Assert
            Assert.DoesNotThrow(() => query.FirstOrDefault());
        }

        [Test]
        public void FetchChild_returns_non_null_result()
        {
            // Arrange
            var query = (from store in _query.Query<InventoryItem>()
                         select store)
              .FetchChild(x => x.Product);

            // Act & Assert
            Assert.NotNull(query.FirstOrDefault());
        }

        [Test]
        public void ThenFetchGrandchild_does_not_raise_exception()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory)
              .ThenFetchGrandchild(x => x.Product);

            // Act & Assert
            Assert.DoesNotThrow(() => query.ToList());
        }

        [Test]
        public void ThenFetchGrandchild_returns_non_null_result()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory)
              .ThenFetchGrandchild(x => x.Product);

            // Act & Assert
            Assert.NotNull(query.ToList());
        }

        [Test]
        public void ThenFetchGrandchildren_does_not_raise_exception()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory)
              .ThenFetchGrandchildren(x => x.Batches);

            // Act & Assert
            Assert.DoesNotThrow(() => query.ToList());
        }

        [Test]
        public void ThenFetchGrandchildren_returns_non_null_result()
        {
            // Arrange
            var query = (from store in _query.Query<Store>()
                         select store)
              .FetchChildren(x => x.Inventory)
              .ThenFetchGrandchildren(x => x.Batches);

            // Act & Assert
            Assert.NotNull(query.ToList());
        }

        [Test]
        public void ToLazy_does_not_raise_exception()
        {
            // Arrange
            var query1 = (from store in _query.Query<Store>()
                          select store)
              .ToLazy();
            var query2 = (from product in _query.Query<Product>()
                          select product)
              .ToLazy();

            // Act & Assert
            Assert.DoesNotThrow(() => query2.Value.ToList(), "Query 2");
            Assert.DoesNotThrow(() => query1.Value.ToList(), "Query 1");
        }

        [Test]
        public void ToLazy_returns_non_null_results()
        {
            // Arrange
            var query1 = (from store in _query.Query<Store>()
                          select store)
              .ToLazy();
            var query2 = (from product in _query.Query<Product>()
                          select product)
              .ToLazy();

            // Act & Assert
            Assert.AreEqual(9, query2.Value.Count(), "Count of products");
            Assert.AreEqual(3, query1.Value.Count(), "Count of stores");
        }

        [Test]
        public void ToLazyValue_does_not_raise_exception()
        {
            // Arrange
            var query1 = (from store in _query.Query<Store>()
                          from item in store.Inventory
                          where item != null
                          select store.Name)
              .ToLazyValue();
            var query2 = (from product in _query.Query<Product>()
                          select product.Price)
              .ToLazyValue();

            // Act & Assert
            Assert.DoesNotThrow(() => { var foo = query2.Value; }, "Query 2");
            Assert.DoesNotThrow(() => { var foo = query1.Value; }, "Query 1");
        }

        [Test]
        public void ToLazyValue_returns_non_null_results()
        {
            // Arrange
            var query1 = (from store in _query.Query<Store>()
                          from item in store.Inventory
                          where item != null
                          select store.Name)
              .ToLazyValue();
            var query2 = (from product in _query.Query<Product>()
                          select product.Price)
              .ToLazyValue();

            // Act & Assert
            Assert.NotNull(query2.Value, "First product price");
            Assert.NotNull(query1.Value, "Store name");
        }

        [Test]
        public void ToListAsync_does_not_throw()
        {
            Assert.That(async () => await _query.Query<Store>().ToListAsync(), Throws.Nothing);
        }

        #endregion
    }
}

