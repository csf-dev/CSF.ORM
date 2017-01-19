using System;
using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.Data.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.Data.NHibernate.Mappings
{
  public class ProductMap : ClassMapping<Product>
  {
    public ProductMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Native);
      });

      Property(x => x.Name, m => {
        m.NotNullable(true);
      });

      Property(x => x.Price, m => {
        m.NotNullable(true);
      });
    }
  }
}

