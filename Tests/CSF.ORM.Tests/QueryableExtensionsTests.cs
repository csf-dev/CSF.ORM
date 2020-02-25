﻿using System;
using NUnit.Framework;
using Moq;
using CSF.ORM;
using System.Linq;
using AutoFixture;
using Test.CSF.ORM.NHibernate.Models;
using Test.CSF.ORM.NHibernate.Entities;
using CSF.ORM.InMemory;

namespace Test.CSF.ORM.NHibernate
{
    [TestFixture, Description("Tests the QueryableExtensions, using an in-memory query API"), Parallelizable(ParallelScope.Children)]
    public class QueryableExtensionsTests
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

            QueryableExtensions.EagerFetchingProvider = new NoOpEagerFetcher();
            QueryableExtensions.LazyQueryingProvider = new NoOpLazyResultProvider();
            QueryableExtensions.AsyncQueryingProvider = new SynchronousAsyncQueryProvider();
        }

        #endregion

        #region tests

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
                          where item != null
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
                          where item != null
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

