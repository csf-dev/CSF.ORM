using System;
using CSF.Entities;

namespace Test.CSF.Data.NHibernate.Entities
{
  public abstract class EntityBase : Entity<long>
  {
    public virtual long Identity
    {
      get {
        return base.IdentityValue;
      }
      set {
        base.IdentityValue = value;
      }
    }
  }
}

