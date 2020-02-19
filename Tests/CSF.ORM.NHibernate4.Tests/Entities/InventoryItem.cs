using System.Collections.Generic;

namespace Test.CSF.ORM.NHibernate.Entities
{
  public class InventoryItem : EntityBase
  {
    public virtual Product Product
    {
      get;
      set;
    }

    public virtual Store Store
    {
      get;
      set;
    }

    public virtual int Quantity
    {
      get;
      set;
    }

    public virtual ISet<Batch> Batches
    {
      get;
      protected set;
    }

    public InventoryItem()
    {
      Batches = new HashSet<Batch>();
    }
  }
}

