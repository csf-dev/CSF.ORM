using System;

namespace Test.CSF.ORM.NHibernate.Entities
{
  public class Sale : EntityBase
  {
    public virtual Store Store
    {
      get;
      set;
    }

    public virtual Customer Customer
    {
      get;
      set;
    }

    public virtual Product Product
    {
      get;
      set;
    }

    public virtual decimal PricePaid
    {
      get;
      set;
    }

    public virtual DateTime TimeOfSale
    {
      get;
      set;
    }

    public virtual int Quantity
    {
      get;
      set;
    }
  }
}

