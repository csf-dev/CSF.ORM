using System;
using CSF.Entities;

namespace Test.CSF.Entities.Stubs
{
#pragma warning disable CS0618 // Type or member is obsolete
  [BaseType(typeof(Person))]
#pragma warning restore CS0618 // Type or member is obsolete
  public class Customer : Person
  {
  }
}

