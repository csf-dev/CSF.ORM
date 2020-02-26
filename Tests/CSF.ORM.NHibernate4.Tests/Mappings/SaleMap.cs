using NHibernate.Mapping.ByCode.Conformist;
using Test.CSF.ORM.NHibernate.Entities;
using NHibernate.Mapping.ByCode;

namespace Test.CSF.ORM.NHibernate.Mappings
{
  public class SaleMap : ClassMapping<Sale>
  {
    public SaleMap()
    {
      Id(x => x.Identity, m => {
        m.Generator(Generators.Assigned);
      });

      Property(x => x.PricePaid, m => {
        m.NotNullable(true);
      });

      Property(x => x.TimeOfSale, m => {
        m.NotNullable(true);
      });

      Property(x => x.Quantity, m => {
        m.NotNullable(true);
      });

      ManyToOne(x => x.Store, m => {
        m.NotNullable(true);
        m.Index("SaleHasStore");
        m.ForeignKey("SaleRequiresStore");
        m.Cascade(Cascade.Persist);
      });

      ManyToOne(x => x.Customer, m => {
        m.NotNullable(true);
        m.Index("SaleHasCustomer");
        m.ForeignKey("SaleRequiresCustomer");
        m.Cascade(Cascade.Persist);
      });

      ManyToOne(x => x.Product, m => {
        m.NotNullable(true);
        m.Index("SaleHasProduct");
        m.ForeignKey("SaleRequiresProduct");
        m.Cascade(Cascade.Persist);
      });
    }
  }
}

