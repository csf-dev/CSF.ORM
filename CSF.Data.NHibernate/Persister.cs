using System;
using NHibernate;

namespace CSF.Data.NHibernate
{
  public class Persister
  {
    readonly ISession session;

    protected virtual ISession Session => session;

    public virtual T Save<T>(T objectToSave)
    {
      if(ReferenceEquals(objectToSave, null))
      {
        throw new ArgumentNullException(nameof(objectToSave));
      }

      return (T) Session.Save(objectToSave);
    }

    public virtual void Delete<T>(T objectToDelete)
    {
      if(ReferenceEquals(objectToDelete, null))
      {
        throw new ArgumentNullException(nameof(objectToDelete));
      }

      Session.Delete(objectToDelete);
    }

    public Persister(ISession session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));
      
      this.session = session;
    }
  }
}
