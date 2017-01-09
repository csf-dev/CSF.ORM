using System;
using NUnit.Framework;
using CSF.Entities;
using Test.CSF.Entities.Stubs;

namespace Test.CSF.Entities
{
  [TestFixture]
  public class TestIdentity
  {
    #region tests

    //          First val   Second type         Second val  Expected result
    //          ---------   -----------         ----------  ---------------
    [TestCase(  5,          typeof(Person),     5,          true)]
    [TestCase(  5,          typeof(Employee),   5,          true)]
    [TestCase(  5,          typeof(Animal),     5,          false)]
    [TestCase(  5,          typeof(Person),     10,         false)]
    [TestCase(  5,          typeof(Employee),   10,         false)]
    [TestCase(  5,          typeof(Animal),     10,         false)]
    public void Equals_object_various_scenarios(int firstVal, Type secondType, int secondVal, bool expected)
    {
      // Arrange
      var firstIdentity = new Identity<int,Person>(firstVal);
      var secondIdentity = Identity.Create(secondType, typeof(int), secondVal);

      // Act
      var result = firstIdentity.Equals((object) secondIdentity);

      // Assert
      Assert.AreEqual(expected, result);
    }

    //          First val   Second type         Second val  Expected result
    //          ---------   -----------         ----------  ---------------
    [TestCase(  5,          typeof(Person),     5,          true)]
    [TestCase(  5,          typeof(Employee),   5,          true)]
    [TestCase(  5,          typeof(Animal),     5,          false)]
    [TestCase(  5,          typeof(Person),     10,         false)]
    [TestCase(  5,          typeof(Employee),   10,         false)]
    [TestCase(  5,          typeof(Animal),     10,         false)]
    public void Equals_IIdentity_various_scenarios(int firstVal, Type secondType, int secondVal, bool expected)
    {
      // Arrange
      var firstIdentity = new Identity<int,Person>(firstVal);
      var secondIdentity = Identity.Create(secondType, typeof(int), secondVal);

      // Act
      var result = firstIdentity.Equals((IIdentity) secondIdentity);

      // Assert
      Assert.AreEqual(expected, result);
    }

    [Test]
    public void Cast_creates_correct_new_identity()
    {
      // Arrange
      var person = Identity.Create<Person>(5);

      // Act
      IIdentity<Customer> customer = person.Cast<Customer>();

      // Assert
      Assert.AreEqual(typeof(Customer), customer.EntityType, "Entity type as expected");
      Assert.AreEqual(5, customer.Value, "Identity value as expected");
    }

    [Test]
    public void Identity_interface_is_covariant()
    {
      // Act
      IIdentity<Person> person = Identity.Create<Customer>(5);

      // Assert
      Assert.AreEqual(typeof(Customer), person.EntityType, "Entity type as expected");
      Assert.AreEqual(5, person.Value, "Identity value as expected");
    }

    #endregion
  }
}

