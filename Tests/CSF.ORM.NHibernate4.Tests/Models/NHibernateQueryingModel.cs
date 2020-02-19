using NHibernate;
using Test.CSF.ORM.NHibernate.Entities;
using AutoFixture;
using System.Linq;

namespace Test.CSF.ORM.NHibernate.Models
{
  public class NHibernateQueryingModel
  {
    #region fields

    private IFixture _autoFixture;

    #endregion

    #region methods

    public void CreateModel(ISession session)
    {
      var stores = CreateStores();

      CreateInventoryItems(stores[0]);

      foreach(var store in stores)
      {
        session.Save(store);
      }
    }

    public Store[] CreateStores()
    {
      return _autoFixture
        .Build<Store>()
        .CreateMany(3)
        .ToArray();
    }

    public InventoryItem[] CreateInventoryItems(Store forStore)
    {
      var output = _autoFixture
        .Build<InventoryItem>()
        .Without(x => x.Store)
        .Without(x => x.Product)
        .Do(x => {
        var batches = _autoFixture
          .Build<Batch>()
          .Without(b => b.Item)
          .Do(b => b.Item = x)
          .CreateMany(2);

        x.Batches.UnionWith(batches);

        x.Product = _autoFixture
          .Build<Product>()
          .Create();
        x.Store = forStore;
      })
        .CreateMany(9)
        .ToArray();

      forStore.Inventory.UnionWith(output);

      return output;
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

