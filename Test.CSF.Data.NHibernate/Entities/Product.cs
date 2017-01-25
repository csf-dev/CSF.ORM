using System;

namespace Test.CSF.Data.NHibernate.Entities
{
  public class Product : EntityBase
  {
    public virtual string Name
    {
      get;
      set;
    }

    public virtual decimal Price
    {
      get;
      set;
    }
  }
}

