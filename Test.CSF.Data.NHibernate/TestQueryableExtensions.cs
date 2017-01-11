using System;
using NUnit.Framework;
using Moq;
using CSF.Data.NHibernate;
using System.Linq;

namespace Test.CSF.Data.NHibernate
{
  [TestFixture]
  public class TestQueryableExtensions
  {
    #region tests

    [Test]
    public void AnyCount_returns_true_for_results()
    {
      // Arrange
      var query = new string[] { "foo" }.AsQueryable();

      // Act
      var result = query.AnyCount();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void AnyCount_returns_false_for_empty_set()
    {
      // Arrange
      var query = new string[0].AsQueryable();

      // Act
      var result = query.AnyCount();

      // Assert
      Assert.IsFalse(result);
    }

    #endregion
  }
}

