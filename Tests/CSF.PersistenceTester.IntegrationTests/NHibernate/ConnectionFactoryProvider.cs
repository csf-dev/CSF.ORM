using System.Reflection;
using CSF.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using CSF.ORM;
using CSF.ORM.NHibernate;

namespace CSF.PersistenceTester.Tests.NHibernate
{
    public class ConnectionFactoryProvider
    {
        readonly IDetectsMono monoDetector;

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
                if (monoDetector.IsExecutingWithMono())
                {
                    MonoNHibernateSqlLiteDriver.MonoDataSqliteAssembly = Assembly.Load("Mono.Data.Sqlite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
                    db.Driver<MonoNHibernateSqlLiteDriver>();
                }
                else
                    db.Driver<SQLite20Driver>();

                db.Dialect<SQLiteDialect>();
                db.ConnectionString = "Data Source=CSF.PersistenceTester.IntegrationTests.db;Version=3;";
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
            });

            var mapping = GetMapping();

            config.AddMapping(mapping);

            return config;
        }

        HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public ConnectionFactoryProvider()
        {
            monoDetector = new MonoRuntimeDetector();
        }
    }
}
