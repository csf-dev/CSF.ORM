using System;
using NUnit.Framework;
using CSF.Entities;
using Test.CSF.Entities.Stubs;

namespace Test.CSF.Entities
{
  [TestFixture]
  public class StaticEntityTests
  {
    #region tests

    [Test]
    public void GetEqualityType_non_generic_returns_correct_type_for_base_entity()
    {
      // Act
      var type = Entity.GetEqualityType(typeof(Person));

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_non_generic_returns_correct_type_for_derived_entity()
    {
      // Act
      var type = Entity.GetEqualityType(typeof(Employee));

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_non_generic_returns_correct_type_for_other_entity()
    {
      // Act
      var type = Entity.GetEqualityType(typeof(Animal));

      // Assert
      Assert.AreEqual(typeof(Animal), type);
    }

    [Test]
    public void GetEqualityType_generic_returns_correct_type_for_base_entity()
    {
      // Act
      var type = Entity.GetEqualityType<Person>();

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_generic_returns_correct_type_for_derived_entity()
    {
      // Act
      var type = Entity.GetEqualityType<Employee>();

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_generic_returns_correct_type_for_other_entity()
    {
      // Act
      var type = Entity.GetEqualityType<Animal>();

      // Assert
      Assert.AreEqual(typeof(Animal), type);
    }

    [Test]
    public void GetEqualityType_instance_returns_correct_type_for_base_entity()
    {
      // Arrange
      var entity = new Person();

      // Act
      var type = Entity.GetEqualityType(entity);

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_instance_returns_correct_type_for_derived_entity()
    {
      // Arrange
      var entity = new Employee();

      // Act
      var type = Entity.GetEqualityType(entity);

      // Assert
      Assert.AreEqual(typeof(Person), type);
    }

    [Test]
    public void GetEqualityType_instance_returns_correct_type_for_other_entity()
    {
      // Arrange
      var entity = new Animal();

      // Act
      var type = Entity.GetEqualityType(entity);

      // Assert
      Assert.AreEqual(typeof(Animal), type);
    }

    //          First               Second              Expected result
    //          -----               ------              ---------------
    [TestCase(  typeof(Person),     typeof(Person),     true)]
    [TestCase(  typeof(Person),     typeof(Employee),   true)]
    [TestCase(  typeof(Person),     typeof(Customer),   true)]
    [TestCase(  typeof(Person),     typeof(Animal),     false)]
    [TestCase(  typeof(Employee),   typeof(Person),     true)]
    [TestCase(  typeof(Employee),   typeof(Employee),   true)]
    [TestCase(  typeof(Employee),   typeof(Customer),   true)]
    [TestCase(  typeof(Employee),   typeof(Animal),     false)]
    [TestCase(  typeof(Customer),   typeof(Person),     true)]
    [TestCase(  typeof(Customer),   typeof(Employee),   true)]
    [TestCase(  typeof(Customer),   typeof(Customer),   true)]
    [TestCase(  typeof(Customer),   typeof(Animal),     false)]
    [TestCase(  typeof(Animal),     typeof(Person),     false)]
    [TestCase(  typeof(Animal),     typeof(Employee),   false)]
    [TestCase(  typeof(Animal),     typeof(Customer),   false)]
    [TestCase(  typeof(Animal),     typeof(Animal),     true)]
    public void AreEqualityTypesSame_various_scenarios(Type first, Type second, bool expectedResult)
    {
      // Arrange


      // Act
      var result = Entity.AreEqualityTypesSame(first, second);

      // Assert
      Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetIdentity_returns_correct_identity_for_derived_type()
    {
      // Arrange
      var entity = new Employee() { Identity = 5 };

      // Act
      var result = entity.GetIdentity();

      // Assert
      Assert.AreEqual(typeof(Employee), result.EntityType, "Entity type");
      Assert.AreEqual(typeof(Int32), result.IdentityType, "Identity type");
      Assert.AreEqual(5, result.Value, "Identity value");
    }

    [Test]
    public void GetIdentity_returns_correct_identity_for_base_type()
    {
      // Arrange
      var entity = new Person() { Identity = 5 };

      // Act
      var result = entity.GetIdentity();

      // Assert
      Assert.AreEqual(typeof(Person), result.EntityType, "Entity type");
      Assert.AreEqual(typeof(Int32), result.IdentityType, "Identity type");
      Assert.AreEqual(5, result.Value, "Identity value");
    }

    #endregion
  }
}

