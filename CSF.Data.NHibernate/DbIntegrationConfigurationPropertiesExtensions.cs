using System;
using NHibernate.Cfg.Loquacious;
using CSF.Reflection;
using NHibernate.Driver;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Extension methods to the NHibernate database configuration interface.
  /// </summary>
  public static class DbIntegrationConfigurationPropertiesExtensions
  {
    #region fields

    private static object _syncRoot;

    #endregion

    #region extension methods

    /// <summary>
    /// Convenience method to select the 'correct' driver for SQLite databases.
    /// </summary>
    /// <remarks>
    /// The rationale for this method is that - when running on the Mono framework instead of the .NET framework - we
    /// will generally prefer to use Mono's builtin driver for SQLite, instead of using a locally-installed SQLite
    /// driver.  This method will use <see cref="Reflect.IsMono"/> and, if we are on Mono,
    /// substitute the default driver that NHibernate would select with Mono's builtin.  If we are not running on Mono
    /// then this method does nothing.
    /// </remarks>
    /// <returns>
    /// The correct driver for the platform.
    /// </returns>
    /// <param name='dbConfig'>
    /// An NHibernate database configuration element.
    /// </param>
    public static void SelectSQLiteDriver(this IDbIntegrationConfigurationProperties dbConfig)
    {
      SelectSQLiteDriver(dbConfig, null);
    }

    /// <summary>
    /// Convenience method to select the 'correct' driver for SQLite databases.
    /// </summary>
    /// <remarks>
    /// The rationale for this method is that - when running on the Mono framework instead of the .NET framework - we
    /// will generally prefer to use Mono's builtin driver for SQLite, instead of using a locally-installed SQLite
    /// driver.  This method will use <see cref="Reflect.IsMono"/> and, if we are on Mono,
    /// substitute the default driver that NHibernate would select with Mono's builtin.  If we are not running on Mono
    /// then this method does nothing.
    /// </remarks>
    /// <returns>
    /// The correct driver for the platform.
    /// </returns>
    /// <param name='dbConfig'>
    /// An NHibernate database configuration element.
    /// </param>
    /// <param name='forceMono'>
    /// A parameter indicating whether or not we are forcing the detection of Mono or not.
    /// </param>
    internal static void SelectSQLiteDriver(this IDbIntegrationConfigurationProperties dbConfig,
                                            bool? forceMono)
    {
      if(dbConfig == null)
      {
        throw new ArgumentNullException(nameof(dbConfig));
      }

      var mono = forceMono.HasValue? forceMono.Value : Reflect.IsMono();

      if(mono)
      {
        ConfigureMonoSqliteAssembly();
        dbConfig.Driver<MonoNHibernateSqlLiteDriver>();
      }
      else
      {
        dbConfig.Driver<SQLite20Driver>();
      }
    }

    private static void ConfigureMonoSqliteAssembly()
    {
      lock(_syncRoot)
      {
        if(MonoNHibernateSqlLiteDriver.MonoDataSqliteAssembly == null)
        {
          var finder = new MonoSqliteAssemblyFinder();
          var assembly = finder.Find();
          MonoNHibernateSqlLiteDriver.MonoDataSqliteAssembly = assembly;
        }
      }
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes the <see cref="CSF.Data.NHibernate.DbIntegrationConfigurationPropertiesExtensions"/> class.
    /// </summary>
    static DbIntegrationConfigurationPropertiesExtensions()
    {
      _syncRoot = new object();
    }

    #endregion
  }
}

