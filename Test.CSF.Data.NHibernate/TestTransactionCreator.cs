using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using CSF.Data.NHibernate;

namespace Test.CSF.Data.NHibernate
{
  [TestFixture]
  public class TestTransactionCreator
  {
    [Test]
    public void BeginTransaction_creates_normal_transaction_when_session_transaction_is_null()
    {
      // Arrange
      var session = GetSession(transactionExists: false);
      var sut = new TransactionCreator(session);

      // Act
      var result = sut.BeginTransaction();

      // Assert
      Assert.NotNull(result, "Result nullability");
      Assert.AreEqual(typeof(TransactionWrapper), result.GetType(), "Result type");
    }

    [Test]
    public void BeginTransaction_creates_subordinate_transaction_when_session_transaction_is_active()
    {
      // Arrange
      var session = GetSession(transactionIsActive: true);
      var sut = new TransactionCreator(session);

      // Act
      var result = sut.BeginTransaction();

      // Assert
      Assert.NotNull(result, "Result nullability");
      Assert.AreEqual(typeof(SubordinateTransactionWrapper), result.GetType(), "Result type");
    }

    [Test]
    public void BeginTransaction_creates_normal_transaction_when_session_transaction_is_not_active()
    {
      // Arrange
      var session = GetSession(transactionIsActive: false);
      var sut = new TransactionCreator(session);

      // Act
      var result = sut.BeginTransaction();

      // Assert
      Assert.NotNull(result, "Result nullability");
      Assert.AreEqual(typeof(TransactionWrapper), result.GetType(), "Result type");
    }

    ISession GetSession(bool transactionExists = true, bool transactionIsActive = false)
    {
      var session = new Mock<ISession>();
      session.Setup(x => x.BeginTransaction()).Returns(Mock.Of<ITransaction>());
      if(transactionExists)
      {
        session.SetupGet(x => x.Transaction).Returns(Mock.Of<ITransaction>(t => t.IsActive == transactionIsActive));
      }
      else
      {
        session.SetupGet(x => x.Transaction).Returns((ITransaction) null);
      }
      return session.Object;
    }
  }
}
