namespace Test.CSF.ORM.NHibernate.Entities
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

