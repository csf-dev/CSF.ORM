using System;
using System.Linq;
using NUnit.Framework;
using Test.CSF.ORM.NHibernate.Config;
using Test.CSF.ORM.NHibernate.Models;
using CSF.ORM.NHibernate;
using Test.CSF.ORM.NHibernate.Entities;
using CSF.ORM;

namespace Test.CSF.ORM.NHibernate
{
  [TestFixture]
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

      using(var trans = _session.BeginTransaction())
      {
        var modelCreator = new NHibernateQueryingModel();
        modelCreator.CreateModel(_session);
        trans.Commit();
      }

      _query = new NHibernateQuery(_session);
    }

    [OneTimeTearDown]
    public void FixtureTeardown()
    {
      if(_session != null)
      {
        _session.Dispose();
      }
      if(_sessionFactory != null)
      {
        _sessionFactory.Dispose();
      }
    }

    #endregion

    #region tests

    [Test]
    public void AnyCount_returns_true_for_results()
    {
      // Act
      var result = _query.Query<Store>().AnyCount();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void AnyCount_returns_false_for_empty_set()
    {
      // Act
      var result = _query.Query<Store>().Where(x => x.Name == "Non-matching name").AnyCount();

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void FetchMany_does_not_raise_exception()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory);

      // Act & Assert
      Assert.DoesNotThrow(() => query.ToList());
    }

    [Test]
    public void FetchMany_returns_non_null_result()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory);

      // Act & Assert
      Assert.NotNull(query.ToList());
    }

    [Test]
    public void Fetch_does_not_raise_exception()
    {
      // Arrange
      var query = (from store in _query.Query<InventoryItem>()
                   select store)
        .Fetch(x => x.Product);

      // Act & Assert
      Assert.DoesNotThrow(() => query.FirstOrDefault());
    }

    [Test]
    public void Fetch_returns_non_null_result()
    {
      // Arrange
      var query = (from store in _query.Query<InventoryItem>()
                   select store)
        .Fetch(x => x.Product);

      // Act & Assert
      Assert.NotNull(query.FirstOrDefault());
    }

    [Test]
    public void FetchManyThenFetch_does_not_raise_exception()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory)
        .ThenFetch(x => x.Product);

      // Act & Assert
      Assert.DoesNotThrow(() => query.ToList());
    }

    [Test]
    public void FetchManyThenFetch_returns_non_null_result()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory)
        .ThenFetch(x => x.Product);

      // Act & Assert
      Assert.NotNull(query.ToList());
    }

    [Test]
    public void FetchManyThenFetchMany_does_not_raise_exception()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory)
        .ThenFetchMany(x => x.Batches);

      // Act & Assert
      Assert.DoesNotThrow(() => query.ToList());
    }

    [Test]
    public void FetchManyThenFetchMany_returns_non_null_result()
    {
      // Arrange
      var query = (from store in _query.Query<Store>()
                   select store)
        .FetchMany(x => x.Inventory)
        .ThenFetchMany(x => x.Batches);

      // Act & Assert
      Assert.NotNull(query.ToList());
    }

    [Test]
    public void ToFuture_does_not_raise_exception()
    {
      // Arrange
      var query1 = (from store in _query.Query<Store>()
                    select store)
        .ToFuture();
      var query2 = (from product in _query.Query<Product>()
                    select product)
        .ToFuture();

      // Act & Assert
      Assert.DoesNotThrow(() => query2.ToList(), "Query 2");
      Assert.DoesNotThrow(() => query1.ToList(), "Query 1");
    }

    [Test]
    public void ToFuture_returns_non_null_results()
    {
      // Arrange
      var query1 = (from store in _query.Query<Store>()
                    select store)
        .ToFuture();
      var query2 = (from product in _query.Query<Product>()
                    select product)
        .ToFuture();

      // Act & Assert
      Assert.AreEqual(9, query2.Count(), "Count of products");
      Assert.AreEqual(3, query1.Count(), "Count of stores");
    }

    [Test]
    public void ToFutureValue_does_not_raise_exception()
    {
      // Arrange
      var query1 = (from store in _query.Query<Store>()
                    from item in store.Inventory
                    where item != null
                    select store.Name)
        .ToFutureValue();
      var query2 = (from product in _query.Query<Product>()
                    select product.Price)
        .ToFutureValue();

      // Act & Assert
      Assert.DoesNotThrow(() => { var foo = query2.Value; }, "Query 2");
      Assert.DoesNotThrow(() => { var foo = query1.Value; }, "Query 1");
    }

    [Test]
    public void ToFutureValue_returns_non_null_results()
    {
      // Arrange
      var query1 = (from store in _query.Query<Store>()
                    from item in store.Inventory
                    where item != null
                    select store.Name)
        .ToFutureValue();
      var query2 = (from product in _query.Query<Product>()
                    select product.Price)
        .ToFutureValue();

      // Act & Assert
      Assert.NotNull(query2.Value, "First product price");
      Assert.NotNull(query1.Value, "Store name");
    }

    #endregion
  }
}

