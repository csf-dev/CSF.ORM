using System;
using NUnit.Framework;
using CSF.Reflection;
using CSF;
using System.Reflection;

namespace Test.CSF.Reflection
{
  [TestFixture]
  public class TestEnumExtensions
  {
    #region tests
    
    [Test]
    public void TestGetUIText()
    {
      Assert.AreEqual("First", SampleEnum.One.GetUIText(), "Correct description");
    }
    
    [Test]
    public void TestGetUITextNull()
    {
      Assert.IsNull(SampleEnum.Three.GetUIText(), "Null description");
    }
    
    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetUITextInvalid()
    {
      ((SampleEnum) 5).GetUIText();
      Assert.Fail("Test should not reach this point");
    }
    
    [Test]
    public void TestGetFieldInfo()
    {
      FieldInfo field = SampleEnum.Three.GetFieldInfo();
      Assert.IsNotNull(field, "Field info not null");
      Assert.AreEqual(SampleEnum.Three.ToString(), field.Name, "Field hsa correct name");
    }

    [Test]
    public void TestIsDefinedValueTrue()
    {
      Assert.IsTrue(SampleEnum.Two.IsDefinedValue());
    }

    [Test]
    public void TestIsDefinedValueFalse()
    {
      Assert.IsFalse(((SampleEnum) 8).IsDefinedValue());
    }
    
    #endregion
    
    #region test enumeration
    
    enum SampleEnum : int
    {
      [UIText("First")]
      One   = 1,
      
      [UIText("Second")]
      Two   = 2,
      
      Three = 3
    }
    
    #endregion
  }
}

