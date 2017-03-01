using System;
using System.Collections.Generic;
using NHibernate;

namespace CSF.Data.NHibernate
{
  public abstract class QueryObjectBase<T>
  {
    readonly ISession session;

    protected virtual ISession Session => session;

    public abstract IEnumerable<T> GetResults();

    public QueryObjectBase(ISession session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));
      
      this.session = session;
    }
  }
}
