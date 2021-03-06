﻿using CSF.Entities;

namespace Test.CSF.ORM.NHibernate.Entities
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

