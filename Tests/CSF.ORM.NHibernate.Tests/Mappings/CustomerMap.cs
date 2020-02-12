using System;
using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.ORM.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.ORM.NHibernate.Mappings
{
  public class CustomerMap : ClassMapping<Customer>
  {
    public CustomerMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Assigned);
      });

      Property(x => x.Name, m => {
        m.NotNullable(true);
      });

      Set(x => x.Purchases,
          m => {
        m.Inverse(true);
        m.Access(Accessor.Property);
        m.Cascade(Cascade.DeleteOrphans | Cascade.Persist);
      },
          m => {
        m.OneToMany(x => {
          x.Class(typeof(Sale));
        });
      });
    }
  }
}

