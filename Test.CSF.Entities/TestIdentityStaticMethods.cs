using System;
using NUnit.Framework;
using CSF.Entities;
using Test.CSF.Entities.Stubs;

namespace Test.CSF.Entities
{
  [TestFixture]
  public class TestIdentityStaticMethods
  {
    #region tests

    [Test]
    public void Create_returns_appropriate_type()
    {
      // Act
      var result = Identity.Create(typeof(Person), typeof(int), 5);

      // Assert
      Assert.AreEqual(typeof(Person), result.EntityType, "Entity type");
      Assert.AreEqual(typeof(int), result.IdentityType, "Identity type");
      Assert.AreEqual(5, result.Value, "Identity value");
    }

    [Test]
    [Description("This method should not use the entity equality type")]
    public void Create_uses_passed_entity_type()
    {
      // Act
      var result = Identity.Create(typeof(Employee), typeof(int), 5);

      // Assert
      Assert.AreEqual(typeof(Employee), result.EntityType, "Entity type");
    }

    [Test]
    public void Equals_returns_true_for_reference_equality()
    {
      // Arrange
      var first = new Person() { Identity = 1 };
      var second = first;

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_returns_true_for_identity_equality()
    {
      // Arrange
      var first = new Person() { Identity = 5 };
      var second = new Person() { Identity = 5 };

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_returns_true_for_identity_equality_inherited()
    {
      // Arrange
      var first = new Person() { Identity = 5 };
      var second = new Employee() { Identity = 5 };

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_returns_true_for_identity_equality_across_class_hierarchies()
    {
      // Arrange
      var first = new Employee() { Identity = 5 };
      var second = new Customer() { Identity = 5 };

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_returns_false_for_incompatible_classes()
    {
      // Arrange
      var first = new Employee() { Identity = 5 };
      var second = new Animal() { Identity = 5 };

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void Equals_returns_false_for_identity_inequality()
    {
      // Arrange
      var first = new Person() { Identity = 5 };
      var second = new Person() { Identity = 10 };

      // Act
      var result = Identity.Equals(first, second);

      // Assert
      Assert.IsFalse(result);
    }

    #endregion
  }
}

