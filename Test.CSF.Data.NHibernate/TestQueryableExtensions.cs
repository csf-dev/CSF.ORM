using System;
using NUnit.Framework;
using Moq;
using CSF.Data.NHibernate;
using System.Linq;
using CSF.Data;
using Ploeh.AutoFixture;
using Test.CSF.Data.NHibernate.Models;
using Test.CSF.Data.NHibernate.Entities;

namespace Test.CSF.Data.NHibernate
{
  [TestFixture]
  public class TestQueryableExtensions
  {
    #region fields

    private IFixture _autoFixture;
    private IQuery _query;

    #endregion

    #region setup

    [SetUp]
    public void Setup()
    {
      _autoFixture = new Fixture();
      var query = new InMemoryQuery();

      query.Add(_autoFixture.CreateMany<string>(), x => x);
      query.Add(new Customer[0], x => x.Identity);

      var queryModel = new NHibernateQueryingModel();

      var stores = queryModel.CreateStores();
      var items = queryModel.CreateInventoryItems(stores[0]);

      query.Add(stores, x => x.Identity);
      query.Add(items, x => x.Identity);

      _query = query;
    }

    #endregion

    #region tests

    [Test]
    public void AnyCount_returns_true_for_results()
    {
      // Arrange

      // Act
      var result = _query.Query<string>().AnyCount();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void AnyCount_returns_false_for_empty_set()
    {
      // Arrange

      // Act
      var result = _query.Query<Customer>().AnyCount();

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
      var query2 = (from item in _query.Query<InventoryItem>()
                    select item)
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
      var query2 = (from item in _query.Query<InventoryItem>()
                    select item)
        .ToFuture();

      // Act & Assert
      Assert.AreEqual(9, query2.Count(), "Count of inventory items");
      Assert.AreEqual(3, query1.Count(), "Count of stores");
    }

    [Test]
    public void ToFutureValue_does_not_raise_exception()
    {
      // Arrange
      var query1 = (from store in _query.Query<Store>()
                    from item in store.Inventory
                    #pragma warning disable 253
                    where item != (object) null
                    #pragma warning restore 253
                    select store.Name)
        .ToFutureValue();
      var query2 = (from item in _query.Query<InventoryItem>()
                    select item.Quantity)
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
                    #pragma warning disable 253
                    where item != (object) null
                    #pragma warning restore 253
                    select store.Name)
        .ToFutureValue();
      var query2 = (from item in _query.Query<InventoryItem>()
                    select item.Quantity)
        .ToFutureValue();

      // Act & Assert
      Assert.NotNull(query2.Value, "First product price");
      Assert.NotNull(query1.Value, "Store name");
    }


    #endregion
  }
}

