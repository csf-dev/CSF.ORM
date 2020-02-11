using System;
using CSF.Entities;

namespace Test.CSF.Entities.Stubs
{
  public class Animal : Entity<int>
  {
    public virtual int Identity
    {
      get { return IdentityValue; }
      set { IdentityValue = value; }
    }

    public virtual string Name
    {
      get;
      set;
    }
  }
}

