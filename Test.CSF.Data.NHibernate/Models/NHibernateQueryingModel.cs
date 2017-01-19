using System;
using NHibernate;
using Test.CSF.Data.NHibernate.Entities;
using Ploeh.AutoFixture;
using System.Linq;

namespace Test.CSF.Data.NHibernate.Models
{
  public class NHibernateQueryingModel
  {
    #region fields

    private IFixture _autoFixture;

    #endregion

    #region methods

    public void CreateModel(ISession session)
    {
      var stores = _autoFixture
        .Build<Store>()
        .Without(x => x.Identity)
        .CreateMany(3)
        .ToArray();

      var inventories = _autoFixture
        .Build<InventoryItem>()
        .Without(x => x.Identity)
        .Without(x => x.Store)
        .Without(x => x.Product)
        .Do(x => {
          var batches = _autoFixture
            .Build<Batch>()
            .Without(b => b.Identity)
            .Without(b => b.Item)
            .Do(b => b.Item = x)
            .CreateMany(2);

          x.Batches.UnionWith(batches);

          x.Product = _autoFixture
            .Build<Product>()
            .Without(p => p.Identity)
            .Create();
          x.Store = stores[0];
        })
        .CreateMany(9);

      stores[0].Inventory.UnionWith(inventories);

      foreach(var store in stores)
      {
        session.Save(store);
      }
    }

    #endregion

    #region constructor

    public NHibernateQueryingModel()
    {
      _autoFixture = new Fixture();
    }

    #endregion
  }
}

