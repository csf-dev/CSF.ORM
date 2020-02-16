using System;
using NUnit.Framework;
using Moq;
using nh = NHibernate;

namespace CSF.ORM.NHibernate.Tests
{
    [TestFixture]
    public class TestTransactionCreatorAdapter
    {
        [Test]
        public void BeginTransaction_creates_normal_transaction_when_session_transaction_is_null()
        {
            // Arrange
            var session = GetSession(transactionExists: false);
            var sut = new TransactionCreatorAdapter(session);

            // Act
            var result = sut.BeginTransaction();

            // Assert
            Assert.NotNull(result, "Result nullability");
            Assert.AreEqual(typeof(TransactionAdapter), result.GetType(), "Result type");
        }

        [Test]
        public void BeginTransaction_creates_subordinate_transaction_when_session_transaction_is_active()
        {
            // Arrange
            var session = GetSession(transactionIsActive: true);
            var sut = new TransactionCreatorAdapter(session);

            // Act
            var result = sut.BeginTransaction();

            // Assert
            Assert.NotNull(result, "Result nullability");
            Assert.AreEqual(typeof(TransactionAdapter), result.GetType(), "Result type");
        }

        [Test]
        public void BeginTransaction_creates_normal_transaction_when_session_transaction_is_not_active()
        {
            // Arrange
            var session = GetSession(transactionIsActive: false);
            var sut = new TransactionCreatorAdapter(session);

            // Act
            var result = sut.BeginTransaction();

            // Assert
            Assert.NotNull(result, "Result nullability");
            Assert.AreEqual(typeof(TransactionAdapter), result.GetType(), "Result type");
        }

        nh.ISession GetSession(bool transactionExists = true, bool transactionIsActive = false)
        {
            var session = new Mock<nh.ISession>();
            session.Setup(x => x.BeginTransaction()).Returns(Mock.Of<nh.ITransaction>());
            if (transactionExists)
            {
                session.SetupGet(x => x.Transaction).Returns(Mock.Of<nh.ITransaction>(t => t.IsActive == transactionIsActive));
            }
            else
            {
                session.SetupGet(x => x.Transaction).Returns((nh.ITransaction)null);
            }
            return session.Object;
        }
    }
}
