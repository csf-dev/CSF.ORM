using System;
namespace CSF.Entities
{
  /// <summary>
  /// Use this to decorate classes which should not be considered for the purpose of identity equality.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class IgnoreForIdentityEqualityAttribute : Attribute
  {
  }
}
