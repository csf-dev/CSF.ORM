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

    #endregion
  }
}

