using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using CSF.ORM;
using CSF.ORM.NHibernate;

namespace CSF.PersistenceTester.Tests.NHibernate
{
    public class ConnectionFactoryProvider
    {
        public IGetsDataConnection GetConnectionFactory()
        {
            var config = GetConfiguration();
            var sessionFactory = config.BuildSessionFactory();
            return new SessionFactoryAdapter(sessionFactory);
        }

        Configuration GetConfiguration()
        {
            var config = new Configuration();

            config.DataBaseIntegration(db => {
                db.Driver<CSF.NHibernate.MonoSafeSQLite20Driver>();
                db.Dialect<SQLiteDialect>();
                db.ConnectionString = "Data Source=CSF.PersistenceTester.IntegrationTests.db;Version=3;";
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
            });
            config.AddMapping(GetMapping());

            return config;
        }

        HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}
