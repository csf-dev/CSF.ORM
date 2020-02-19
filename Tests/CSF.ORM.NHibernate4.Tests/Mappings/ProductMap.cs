using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.ORM.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.ORM.NHibernate.Mappings
{
  public class ProductMap : ClassMapping<Product>
  {
    public ProductMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Assigned);
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

