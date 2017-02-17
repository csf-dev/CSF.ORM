using System;
using CSF.Entities;

namespace Test.CSF.Entities.Stubs
{
  /// <summary>
  /// This entity type mimicks an NHibernate proxy (in that it will throw an exception from all virtual methods except the primary key value property).
  /// </summary>
  public class ProhibitVirtualMethodsEntity : Entity<long>
  {
    public override bool Equals(object obj)
    {
      throw new NotSupportedException("This entity does not support any virtual method calls.");
    }

    public override int GetHashCode()
    {
      throw new NotSupportedException("This entity does not support any virtual method calls.");
    }

    protected override bool HasIdentity
    {
      get {
        throw new NotSupportedException("This entity does not support any virtual method calls.");
      }
    }

    public override bool IdentityEquals(IEntity other)
    {
      throw new NotSupportedException("This entity does not support any virtual method calls.");
    }

    public override string ToString()
    {
      throw new NotSupportedException("This entity does not support any virtual method calls.");
    }

    public override void SetIdentityValue (long identity)
    {
      throw new NotSupportedException("This entity does not support any virtual method calls.");
    }
  }
}

