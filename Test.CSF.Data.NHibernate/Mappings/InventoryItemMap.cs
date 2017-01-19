using System;
using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.Data.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.Data.NHibernate.Mappings
{
  public class InventoryItemMap : ClassMapping<InventoryItem>
  {
    public InventoryItemMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Native);
      });

      Property(x => x.Quantity, m => {
        m.NotNullable(true);
      });

      ManyToOne(x => x.Product, m => {
        m.NotNullable(true);
        m.Index("InventoryItemHasProduct");
        m.ForeignKey("InventoryItemRequiresProduct");
        m.Cascade(Cascade.Persist);
      });

      ManyToOne(x => x.Store, m => {
        m.NotNullable(true);
        m.Index("InventoryItemHasStore");
        m.ForeignKey("InventoryItemRequiresStore");
        m.Cascade(Cascade.Persist);
      });

      Set(x => x.Batches, m => {
        m.Inverse(true);
        m.Access(Accessor.Property);
        m.Cascade(Cascade.DeleteOrphans | Cascade.Persist);
      },
          m => {
        m.OneToMany(x => {
          x.Class(typeof(Batch));
        });
      });
    }
  }
}

