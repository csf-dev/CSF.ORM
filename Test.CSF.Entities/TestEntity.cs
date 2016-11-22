using System;
using NUnit.Framework;
using Test.CSF.Entities.Stubs;
using CSF.Entities;

namespace Test.CSF.Entities
{
  [TestFixture]
  public class TestEntity
  {
    #region tests

    [Test]
    public void GetHashCode_returns_identity_hash_code()
    {
      // Arrange
      var identity = 10;
      var entity = new Person() { Identity = identity };

      // Act
      var result = entity.GetHashCode();

      // Assert
      Assert.AreEqual(identity.GetHashCode(), result);
    }

    [Test]
    public void GetHashCode_does_not_change_after_resetting_identity()
    {
      // Arrange
      var entity = new Person() { Identity = 5 };

      // Act
      var result1 = entity.GetHashCode();
      entity.Identity = 10;
      var result2 = entity.GetHashCode();

      // Assert
      Assert.AreEqual(result1, result2);
    }

    [Test]
    public void GetHashCode_does_not_change_after_setting_identity()
    {
      // Arrange
      var entity = new Person();

      // Act
      var result1 = entity.GetHashCode();
      entity.Identity = 10;
      var result2 = entity.GetHashCode();

      // Assert
      Assert.AreEqual(result1, result2);
    }

    [Test]
    public void ToString_returns_identity()
    {
      // Arrange
      var entity = new Person() { Identity = 10 };

      // Act
      var result = entity.ToString();

      // Assert
      Assert.AreEqual("[Person#10]", result);
    }

    [Test]
    public void ToString_uses_actual_entity_type()
    {
      // Arrange
      var entity = new Employee() { Identity = 10 };

      // Act
      var result = entity.ToString();

      // Assert
      Assert.AreEqual("[Employee#10]", result);
    }

    [Test]
    public void ToString_returns_no_identity()
    {
      // Arrange
      var entity = new Person();

      // Act
      var result = entity.ToString();

      // Assert
      Assert.AreEqual("[Person#(no identity)]", result);
    }

    [Test]
    public void Equals_returns_true_for_reference_equality()
    {
      // Arrange
      var entity = new Person();

      // Act
      var result = entity.Equals(entity);

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_returns_false_for_identity_equality()
    {
      // Arrange
      var entity1 = new Person() {Identity = 5};
      var entity2 = new Person() {Identity = 5};

      // Act
      var result = entity1.Equals(entity2);

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void GetIdentityType_returns_correct_value()
    {
      // Arrange
      IEntity entity = new Person();

      // Act
      var result = entity.GetIdentityType();

      // Assert
      Assert.AreEqual(typeof(int), result);
    }

    #endregion
  }
}

