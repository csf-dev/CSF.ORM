using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using CSF.Data.NHibernate;
using CSF.Entities;

namespace Test.CSF.Data.NHibernate
{
  [TestFixture]
  public class TestISessionExtensions
  {
    #region tests

    [Test]
    public void TestGet()
    {
      var session = new Mock<ISession>(MockBehavior.Strict);
      IIdentity<DummyEntity> identity = new Identity<DummyEntity,ulong>(5);
      var entity = new DummyEntity();

      session.Setup(x => x.Get<DummyEntity>(It.IsAny<object>())).Returns(entity);

      var result = session.Object.Get(identity);

      session.Verify(x => x.Get<DummyEntity>(It.IsAny<object>()), Times.Once());
      Assert.AreEqual(entity, result);
    }

    [Test]
    public void TestGetNull()
    {
      var session = new Mock<ISession>(MockBehavior.Strict);
      IIdentity<DummyEntity> identity = null;
      var entity = new DummyEntity();

      session.Setup(x => x.Get<DummyEntity>(It.IsAny<object>())).Returns(entity);

      var result = session.Object.Get(identity);

      session.Verify(x => x.Get<DummyEntity>(It.IsAny<object>()), Times.Never());
      Assert.IsNull(result);
    }

    [Test]
    public void TestLoad()
    {
      var session = new Mock<ISession>(MockBehavior.Strict);
      IIdentity<DummyEntity> identity = new Identity<DummyEntity,ulong>(5);
      var entity = new DummyEntity();

      session.Setup(x => x.Load<DummyEntity>(It.IsAny<object>())).Returns(entity);

      var result = session.Object.Load(identity);

      session.Verify(x => x.Load<DummyEntity>(It.IsAny<object>()), Times.Once());
      Assert.AreEqual(entity, result);
    }

    [Test]
    public void TestLoadNull()
    {
      var session = new Mock<ISession>(MockBehavior.Strict);
      IIdentity<DummyEntity> identity = null;
      var entity = new DummyEntity();

      session.Setup(x => x.Load<DummyEntity>(It.IsAny<object>())).Returns(entity);

      var result = session.Object.Load(identity);

      session.Verify(x => x.Load<DummyEntity>(It.IsAny<object>()), Times.Never());
      Assert.IsNull(result);
    }

    #endregion

    #region contained types

    class DummyEntity : Entity<DummyEntity,ulong> {}

    #endregion
  }
}

