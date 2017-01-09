using System;
using CSF.Entities;

namespace Test.CSF.Entities.Stubs
{
  public class NonGenericEntityType : IEntity
  {
    #region properties

    public bool HasIdentity { get { throw new NotSupportedException(); } }

    public object IdentityValue { get { throw new NotSupportedException(); } }

    #endregion

    #region methods

    public Type GetIdentityType()
    {
      return typeof(string);
    }

    public bool IdentityEquals(IEntity other) { throw new NotSupportedException(); }

    #endregion
  }
}

