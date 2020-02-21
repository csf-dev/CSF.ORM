using CSF.Entities;

namespace CSF.Entities.Tests.Stubs
{
  public class Person : Entity<int>
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

