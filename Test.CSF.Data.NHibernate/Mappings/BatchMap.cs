using System;
using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.Data.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.Data.NHibernate.Mappings
{
  public class BatchMap : ClassMapping<Batch>
  {
    public BatchMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Native);
      });

      Property(x => x.Name, m => {
        m.NotNullable(true);
      });

      ManyToOne(x => x.Item, m => {
        m.NotNullable(true);
        m.Index("BatchHasInventoryItem");
        m.ForeignKey("BatchRequiresInventoryItem");
        m.Cascade(Cascade.Persist);
      });
    }
  }
}

