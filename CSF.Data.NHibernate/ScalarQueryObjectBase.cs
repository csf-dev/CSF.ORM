using System;
using NHibernate;

namespace CSF.Data.NHibernate
{
  public abstract class ScalarQueryObjectBase<T>
  {
    readonly ISession session;

    protected virtual ISession Session => session;

    public abstract T GetResult();

    public ScalarQueryObjectBase(ISession session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));

      this.session = session;
    }
  }
}
