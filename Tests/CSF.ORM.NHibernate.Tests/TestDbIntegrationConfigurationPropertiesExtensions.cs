using System;
using NHibernate.Cfg.Loquacious;
using NUnit.Framework;
using CSF.ORM.NHibernate;
using Moq;
using NHibernate.Driver;

namespace Test.CSF.ORM.NHibernate
{
  [TestFixture]
  public class TestDbIntegrationConfigurationPropertiesExtensions
  {
    #region tests

    [Test]
    public void SelectSQLiteDriver_returns_mono_driver_when_mono_is_running()
    {
      // Arrange
      var conf = new Mock<IDbIntegrationConfigurationProperties>();

      // Act
      conf.Object.SelectSQLiteDriver(true);

      // Assert
      conf.Verify(x => x.Driver<MonoNHibernateSqlLiteDriver>(), Times.Once());
    }

    [Test]
    public void SelectSQLiteDriver_returns_builtin_driver_when_mono_is_not_running()
    {
      // Arrange
      var conf = new Mock<IDbIntegrationConfigurationProperties>();

      // Act
      conf.Object.SelectSQLiteDriver(false);

      // Assert
      conf.Verify(x => x.Driver<SQLite20Driver>(), Times.Once());
    }

    #endregion
  }
}

