using System.Collections.Generic;

namespace Test.CSF.ORM.NHibernate.Entities
{
  public class Store : EntityBase
  {
    public virtual string Name
    {
      get;
      set;
    }

    public virtual ISet<InventoryItem> Inventory
    {
      get;
      protected set;
    }

    public virtual ISet<Sale> Sales
    {
      get;
      protected set;
    }

    public Store()
    {
      Inventory = new HashSet<InventoryItem>();
      Sales = new HashSet<Sale>();
    }
  }
}

