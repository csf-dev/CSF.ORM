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
    #region constants

    private const ulong IDENTITY = 56;

    #endregion

    #region fields

    private DummyEntity _entity;
    private IIdentity<DummyEntity> _identity;
    private Mock<ISession> _sut;

    #endregion

    #region setup

    [SetUp]
    public void Setup()
    {
      _sut = new Mock<ISession>(MockBehavior.Strict);

      _entity = new DummyEntity() {
        Identity = IDENTITY
      };
      _identity = _entity.GetIdentity();
    }

    #endregion

    #region tests

    [Test]
    public void TestGet()
    {
      // Arrange
      _sut.Setup(x => x.Get<DummyEntity>(IDENTITY)).Returns(_entity);

      // Act
      var result = _sut.Object.Get(_identity);

      // Assert
      _sut.Verify(x => x.Get<DummyEntity>(IDENTITY), Times.Once());
      Assert.AreEqual(_entity, result);
    }

    [Test]
    public void TestGetNull()
    {
      // Arrange
      IIdentity<DummyEntity> identity = null;

      _sut.Setup(x => x.Get<DummyEntity>(null)).Throws<ArgumentNullException>();

      // Act
      var result = _sut.Object.Get(identity);

      // Assert
      _sut.Verify(x => x.Get<DummyEntity>(null), Times.Never());
      Assert.IsNull(result);
    }

    [Test]
    public void TestLoad()
    {
      // Arrange
      _sut.Setup(x => x.Load<DummyEntity>(IDENTITY)).Returns(_entity);

      // Act
      var result = _sut.Object.Load(_identity);

      // Assert
      _sut.Verify(x => x.Load<DummyEntity>(IDENTITY), Times.Once());
      Assert.AreEqual(_entity, result);
    }

    [Test]
    public void TestLoadNull()
    {
      // Arrange
      IIdentity<DummyEntity> identity = null;

      _sut.Setup(x => x.Load<DummyEntity>(null)).Throws<ArgumentNullException>();

      // Act
      var result = _sut.Object.Load(identity);

      // Assert
      _sut.Verify(x => x.Load<DummyEntity>(null), Times.Never());
      Assert.IsNull(result);
    }

    #endregion

    #region contained types

    class DummyEntity : Entity<ulong> {}

    #endregion
  }
}

