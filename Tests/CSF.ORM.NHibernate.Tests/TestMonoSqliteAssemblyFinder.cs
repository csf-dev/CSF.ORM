using System;
using NUnit.Framework;
using CSF.ORM.NHibernate;
using CSF.Reflection;

namespace Test.CSF.ORM.NHibernate
{
  [TestFixture]
  public class TestMonoSqliteAssemblyFinder
  {
    #region fields

    private MonoSqliteAssemblyFinder _sut;

    #endregion

    #region setup

    [SetUp]
    public void Setup()
    {
      _sut = new MonoSqliteAssemblyFinder();
    }

    #endregion

    #region tests

    [Test]
    public void Find_can_find_mono_data_sqlite_on_mono_systems()
    {
      // Arrange
      if(!new MonoRuntimeDetector().IsExecutingWithMono())
      {
        Assert.Inconclusive("Mono.Data.Sqlite is not available on non-mono systems, and so this test cannot be run.");
      }

      // Act
      var result = _sut.Find();

      // Assert
      Assert.NotNull(result);
    }

    #endregion
  }
}

