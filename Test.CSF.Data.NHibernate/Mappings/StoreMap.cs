using System;
using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.Data.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.Data.NHibernate.Mappings
{
  public class StoreMap : ClassMapping<Store>
  {
    public StoreMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Native);
      });

      Property(x => x.Name, m => {
        m.NotNullable(true);
      });

      Set(x => x.Inventory, m => {
        m.Inverse(true);
        m.Access(Accessor.Property);
        m.Cascade(Cascade.DeleteOrphans | Cascade.Persist);
      },
          m => {
        m.OneToMany(x => {
          x.Class(typeof(InventoryItem));
        });
      });

      Set(x => x.Sales, m => {
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

