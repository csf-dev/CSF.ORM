//
// InMemoryIdentityGeneratorTests.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2017 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using NUnit.Framework;
using CSF.Data.Entities;
using Ploeh.AutoFixture.NUnit3;
using Moq;
using Test.CSF.Stubs;

namespace Test.CSF
{
  [TestFixture]
  public class InMemoryIdentityGeneratorTests
  {
    [Test,AutoMoqData]
    public void GenerateIdentity_string_creates_string_formatted_as_a_guid(InMemoryIdentityGenerator sut)
    {
      // Act
      var result = sut.GenerateIdentity<string>();

      // Assert
      Assert.That(result, Does.Match(@"^[0-9a-f-]{36}$"));
    }

    [Test,AutoMoqData]
    public void GenerateIdentity_guid_creates_a_non_empty_guid(InMemoryIdentityGenerator sut)
    {
      // Act
      var result = sut.GenerateIdentity<Guid>();

      // Assert
      Assert.That(result, Is.Not.EqualTo(Guid.Empty));
    }

    [Test,AutoMoqData]
    public void GenerateIdentity_byte_uses_generator([Frozen] INumberGenerator generator,
                                                     InMemoryIdentityGenerator sut,
                                                     byte number)
    {
      // Arrange
      Mock.Get(generator).Setup(x => x.GetByte()).Returns(number);

      // Act
      var result = sut.GenerateIdentity<byte>();

      // Assert
      Assert.That(result, Is.EqualTo(number));
    }

    [Test,AutoMoqData]
    public void GenerateIdentity_int_uses_generator([Frozen] INumberGenerator generator,
                                                    InMemoryIdentityGenerator sut,
                                                    int number)
    {
      // Arrange
      Mock.Get(generator).Setup(x => x.GetInt()).Returns(number);

      // Act
      var result = sut.GenerateIdentity<int>();

      // Assert
      Assert.That(result, Is.EqualTo(number));
    }

    [Test,AutoMoqData]
    public void GenerateIdentity_long_uses_generator([Frozen] INumberGenerator generator,
                                                     InMemoryIdentityGenerator sut,
                                                     long number)
    {
      // Arrange
      Mock.Get(generator).Setup(x => x.GetLong()).Returns(number);

      // Act
      var result = sut.GenerateIdentity<long>();

      // Assert
      Assert.That(result, Is.EqualTo(number));
    }

    [Test,AutoMoqData]
    public void GenerateIdentity_for_entity_replaces_value_on_entity([Frozen] INumberGenerator generator,
                                                                     InMemoryIdentityGenerator sut,
                                                                     long number,
                                                                     Person person)
    {
      // Arrange
      Mock.Get(generator).Setup(x => x.GetLong()).Returns(number);

      // Act
      sut.GenerateIdentity(person);

      // Assert
      Assert.That(person.Identity, Is.EqualTo(number));
    }
  }
}
