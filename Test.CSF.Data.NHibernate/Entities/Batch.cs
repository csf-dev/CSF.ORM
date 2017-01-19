using System;

namespace Test.CSF.Data.NHibernate.Entities
{
  public class Batch : EntityBase
  {
    public virtual string Name
    {
      get;
      set;
    }

    public virtual InventoryItem Item
    {
      get;
      set;
    }
  }
}

