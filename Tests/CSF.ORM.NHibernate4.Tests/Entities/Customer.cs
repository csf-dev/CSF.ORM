using System.Collections.Generic;

namespace Test.CSF.ORM.NHibernate.Entities
{
  public class Customer : EntityBase
  {
    public virtual string Name
    {
      get;
      set;
    }

    public virtual ISet<Sale> Purchases
    {
      get;
      protected set;
    }

    public Customer()
    {
      Purchases = new HashSet<Sale>();
    }
  }
}

