using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using System.Linq;
using Test.CSF.ORM.NHibernate.Mappings;
using CSF.ORM;
using NHibernate.Tool.hbm2ddl;
using System.Data;
using System.IO;
using System.Text;
using NHibernate.Dialect;
using env = NHibernate.Cfg.Environment;

namespace Test.CSF.ORM.NHibernate.Config
{
  public class NHibernateProvider
  {
    #region fields

    private static readonly Type NamespaceMarker = typeof(IMappingNamespace);
    private readonly log4net.ILog _logger;

    #endregion

    #region methods

    public ISessionFactory CreateSessionFactory(Configuration config)
    {
      if(config == null)
      {
        throw new ArgumentNullException(nameof(config));
      }

      return config.BuildSessionFactory();
    }

    public Configuration CreateConfiguration()
    {
      var config = new Configuration();

      config.DataBaseIntegration(x => {
        x.ConnectionString = "Data Source=:memory:;Version=3;New=True;";
        x.Dialect<SQLiteDialect>();
      });

      config.SetProperty(env.ReleaseConnections, "on_close");

      var mapper = new ModelMapper();

      var mappingTypes = (from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                          where
                          type.IsClass
                          && !type.IsAbstract
                          && type.Namespace.StartsWith(NamespaceMarker.Namespace)
                          select type)
        .ToArray();

      mapper.AddMappings(mappingTypes);

      var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();

      config.SetInterceptor(new LoggingInterceptor());

      config.AddMapping(mappings);

      return config;
    }

    public void ExportSchema(Configuration config, IDbConnection connection)
    {
      var exporter = new SchemaExport(config);
      var sql = new StringBuilder();

      using(var writer = new StringWriter(sql))
      {
        exporter.Execute(false, true, false, connection, writer);
      }

      _logger.Info(sql);
    }

    #endregion

    #region constructor

    public NHibernateProvider()
    {
      _logger = log4net.LogManager.GetLogger(this.GetType());
    }

    #endregion
  }
}

