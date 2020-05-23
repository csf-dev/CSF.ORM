using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CSF.PersistenceTester.Tests.NHibernate
{
    public class SampleEntityMap : ClassMapping<SampleEntity>
    {
        public SampleEntityMap()
        {
            Id(x => x.Identity);
            Property(x => x.StringProperty);
            ManyToOne(x => x.RelatedEntity, m =>
            {
                m.Column("RelatedEntityId");
                m.Cascade(Cascade.None);
            });
        }
    }
}
